using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MudBlazor;
using MudBlazor.Services;
using RDS.Core.Interfaces;
using RDS.Core.Libraries.Services;
using RDS.Core.Models;
using RDS.Core.Requests;
using RDS.Core.Utils;
using RDS.Core.Validators.CategoryValidators;
using RDS.Core.Validators.ServiceOfferedValidators;
using RDS.Core.Validators.SkillSwapRequestValidators;
using RDS.Infrastructure.Repositories;
using RDS.Infrastructure.Services;
using RDS.Persistence;
using RDS.Server.Components.Account;
using RDS.Server.Identity;
using RDS.Server.Libraries;
using RDS.Server.Libraries.Filters;
using RDS.Server.Services;
using Resend;

namespace RDS.Server.Common.Server;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        ConfigurationServer.ConnectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        ConfigurationServer.SmtpResend.Host =
            builder.Configuration.GetSection("SmtpResend").GetValue<string>("Host") ?? string.Empty;
        ConfigurationServer.SmtpResend.Port = builder.Configuration.GetSection("SmtpResend").GetValue<int>("Port");
        ConfigurationServer.SmtpResend.UserName =
            builder.Configuration.GetSection("SmtpResend").GetValue<string>("UserName") ?? string.Empty;
        ConfigurationServer.SmtpResend.ResendApiKey =
            builder.Configuration.GetSection("SmtpResend").GetValue<string>("ResendApiKey") ?? string.Empty;
        ConfigurationServer.SmtpResend.FromEmail =
            builder.Configuration.GetSection("SmtpResend").GetValue<string>("FromEmail") ?? string.Empty;
    }

    public static void AddServiceContainer(this WebApplicationBuilder builder)
    {
        // Add MudBlazor services
        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 2000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });

        builder.Services.AddControllersWithViews();
        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents()
            //.AddInteractiveWebAssemblyComponents()
            .AddAuthenticationStateSerialization();

        builder.Services.AddSingleton<ICurrencyParser, CurrencyParser>();

        builder.Services.AddMemoryCache();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
        builder.Services.AddControllers(options => { options.Filters.Add<ValidationFilter>(); });
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        // Registra a fábrica de DbContext para ser usada nos repositórios (evita problemas de concorrência).
        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseSqlServer(ConfigurationServer.ConnectionString));

        // Para compatibilidade com Identity, mantemos o DbContext scoped também
        builder.Services.AddScoped<ApplicationDbContext>(provider =>
        {
            var factory = provider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
            return factory.CreateDbContext();
        });

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Configuración de Identity
        builder.Services
            .AddIdentity<ApplicationUser, IdentityRole<long>>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<CustomIdentityErrorDescriber>();
}

    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        // Configuración base de autenticación - solo una vez
        builder.Services.AddAuthentication() /*(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })*/
            // Proveedores de autenticación externos
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("OAuth:Google");
                options.ClientId = googleAuthNSection["ClientId"]!;
                options.ClientSecret = googleAuthNSection["ClientSecret"]!;
            })
            .AddFacebook(options =>
            {
                IConfigurationSection fbAuthNSection = builder.Configuration.GetSection("OAuth:FaceBook");
                options.ClientId = fbAuthNSection["ClientId"]!;
                options.ClientSecret = fbAuthNSection["ClientSecret"]!;
            })
            .AddMicrosoftAccount(options =>
            {
                IConfigurationSection msAuthNSection = builder.Configuration.GetSection("OAuth:Microsoft");
                options.ClientId = msAuthNSection["ClientId"]!;
                options.ClientSecret = msAuthNSection["ClientSecret"]!;
            });
        //.AddIdentityCookies();

        // Configuración de cookies para API
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                if (context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                }

                context.Response.Redirect(context.RedirectUri);
                return Task.CompletedTask;
            };

            options.Events.OnRedirectToAccessDenied = context =>
            {
                if (context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                }

                context.Response.Redirect(context.RedirectUri);
                return Task.CompletedTask;
            };
        });

        var url = builder.Configuration.GetSection("ApiSettings")["BaseAddress"]!;
        builder.Services.AddHttpClient("LocalAPI",
            client => { client.BaseAddress = new Uri(url); });

        builder.Services.AddAuthorization();
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => options.AddPolicy(
            ConfigurationServer.CorsPolicyName,
            policy => policy
                .WithOrigins([
                    ConfigurationServer.BackendUrl,
                    ConfigurationServer.FrontendUrl
                ])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
        ));
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(options =>
        {
            // Definir explicitamente a versão OpenAPI correta
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SkillSwap API",
                Version = "v1",
                Description = "API para gestão de serviços e profissionais do SkillSwap",
                Contact = new OpenApiContact
                {
                    Name = "Equipe SkillSwap",
                    Email = "contato@skillswap.com",
                    Url = new Uri("https://skillswap.com")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            options.CustomSchemaIds(type => type.FullName);

            // Función auxiliar para obtener la ruta del archivo XML para un ensamblado
            string GetXmlCommentsPath(Assembly assembly)
            {
                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                return xmlPath;
            }

            // Incluye el XML del ensamblado actual (donde está AddDocumentation, si tiene comentarios relevantes)
            var executingAssembly = Assembly.GetExecutingAssembly();
            var executingXmlPath = GetXmlCommentsPath(executingAssembly);
            if (File.Exists(executingXmlPath)) // Buena práctica: verificar si el archivo existe
            {
                options.IncludeXmlComments(executingXmlPath);
            }

            // Incluye el XML del ensamblado que contiene los Controllers (ej: RDS)
            // Usa un tipo conocido de ese ensamblado, como CategoryController
            /*var controllersAssembly = typeof(CategoryController).Assembly;
            var controllersXmlPath = GetXmlCommentsPath(controllersAssembly);
            if (File.Exists(controllersXmlPath))
            {
                options.IncludeXmlComments(controllersXmlPath);
            }*/

            // Incluye el XML del ensamblado que contiene los DTOs/Requests (ej: RDS.Core)
            // Usa un tipo conocido de ese ensamblado, como FilterParams
            var coreAssembly = typeof(FilterParams).Assembly;
            var coreXmlPath = GetXmlCommentsPath(coreAssembly);
            if (File.Exists(coreXmlPath))
            {
                options.IncludeXmlComments(coreXmlPath);
            }

            // 🟰 Añade esto para cargar el XML de comentarios
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            options.IncludeXmlComments(Path.Combine(xmlPath));
        });
    }

    public static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IServiceOfferedRepository, ServiceOfferedRepository>();
        builder.Services.AddScoped<IProfessionalProfileRepository, ProfessionalProfileRepository>();
        builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
        builder.Services.AddScoped<ISkillSwapRepository, SkillSwapRepository>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        #region MailServices

        //builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        builder.Services.AddSingleton<EmailQueueService>();
        builder.Services.AddHostedService<EmailBackgroundSender>();
        builder.Services.AddTransient<IEmailSendService, EmailSendService>();
        builder.Services.AddScoped<IEmailSender<ApplicationUser>, EmailSender>();
        builder.Services.AddOptions();
        builder.Services.AddHttpClient<ResendClient>();
        builder.Services.Configure<ResendClientOptions>(options =>
        {
            options.ApiToken = builder.Configuration.GetSection("ResendConfiguration")["ResendApiKey"]!;
        });
        builder.Services.AddScoped<IResend, ResendClient>();

        #endregion

        builder.Services.AddScoped<LayoutStateService>();
        builder.Services.AddScoped<ThemeState>();
        builder.Services.AddScoped<ThemeService>();
        builder.Services.AddScoped<IUserContextService, UserContextService>();
        builder.Services.AddScoped<UserTimeZoneService>();
        builder.Services.AddSingleton<CultureHelper>();

        // Serviço de notificações em tempo real (Singleton para compartilhar eventos entre todas as conexões)
        builder.Services.AddSingleton<ISkillSwapNotificationService, SkillSwapNotificationService>();

        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IServiceOfferedService, ServiceOfferedService>();
        builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();
        builder.Services.AddScoped<IProfessionalProfileService, ProfessionalProfileService>();
        builder.Services.AddScoped<IReviewService, ReviewService>();
        builder.Services.AddScoped<IMessageService, MessageService>();
        builder.Services.AddScoped<ISkillSwapService, SkillSwapService>();

        builder.Services.AddValidatorsFromAssemblyContaining<CategoryBaseDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CategoryCreateDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CategoryUpdateDtoValidator>();

        builder.Services.AddValidatorsFromAssemblyContaining<ServiceOfferedDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<ServiceOfferedBaseDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<ServiceOfferedCreateDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<ServiceOfferedUpdateDtoValidator>();

        builder.Services.AddValidatorsFromAssemblyContaining<SkillSwapRequesCreateDtoValidator>();
    }
}