using Microsoft.AspNetCore.Identity;
using RDS.Persistence.Data;
using RDS.Server.Components;
using RDS.Server.Components.Account;
using ApplicationDbContext = RDS.Persistence.ApplicationDbContext;
using ApplicationUser = RDS.Core.Models.ApplicationUser;

namespace RDS.Server.Common.Server;

public static class AppExtension
{
    public static void LoadConfiguration(this WebApplication app)
    {
        var smtp = new ConfigurationServer.SmtpConfiguration();
        app.Configuration.GetSection("SmtpConfiguration").Bind(smtp);
        ConfigurationServer.Smtp = smtp;

        var smtpResend = new ConfigurationServer.ResendConfiguration();
        app.Configuration.GetSection("ResendConfiguration").Bind(smtpResend);
        ConfigurationServer.SmtpResend = smtpResend;
    }
    public static void ConfigureEnvironment(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "SkillSwap API v1");
                options.DocumentTitle = "Documentação SkillSwap";
                options.RoutePrefix = "swagger";
            });

            app.MapSwagger();//.RequireAuthorization(); // Protege a rota do swagger (se quiser)
            app.UseWebAssemblyDebugging();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }
    }

    public static void ConfigureComponents(this WebApplication app)
    {
        app.UseHttpsRedirection();
        //app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();
        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        app.MapControllers();
        app.MapAdditionalIdentityEndpoints();
    }

    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        // Cria um escopo para resolver serviços "scoped" de forma segura durante a inicialização.
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<long>>>();

        // Lê a configuração (appsettings.json / appsettings.Development.json / variáveis de ambiente)
        var shouldSeed = app.Configuration.GetValue<bool>("SeedDatabase");
        await DatabaseSeeder.SeedAsync(dbContext, userManager, roleManager, shouldSeed);
    }
}