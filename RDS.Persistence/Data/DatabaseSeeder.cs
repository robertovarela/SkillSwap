using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RDS.Core.Models;

namespace RDS.Persistence.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole<long>> roleManager,
        bool shouldSeed)
    {
        // A decisão de executar o seeder é controlada pelo caller (IConfiguration / variável de ambiente).
        // Por padrão, configure "SeedDatabase": false no appsettings ou deixe a variável de ambiente apropriada.
        if (!shouldSeed)
            return;

        // 1. Criar Roles
        string[] roles = { "administrador", "profissional", "cliente" };
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<long>
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpperInvariant()
                });
            }
        }

        // 2. Gerar usuários se tiver menos que o determinado
        var predefinedEmails = new List<string>
        {
            "roberto.rn@gmail.com", "Dean.gomesc@gmail.com", "Oliveiracarol1266@gmail.com",
            "pedroluciandro675@gmail.com", "emyleagra2@gmail.com", "rodriguesaltanir@gmail.com"
        };
        await GenerateUsersWithRolesAsync(context, userManager, predefinedEmails.Count, predefinedEmails, "administrador");
        //await GenerateUsersWithRolesAsync(context, userManager, 4);

        // 3. Categorias
        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new() { Name = "Design Gráfico", Description = "Identidade visual, logos, banners e mais" },
                new() { Name = "Desenvolvimento Web", Description = "Sites, sistemas e landing pages" },
                new() { Name = "Redação & Copywriting", Description = "Textos profissionais para blogs, e-mails etc" },
                new() { Name = "Marketing Digital", Description = "Redes sociais, campanhas, anúncios online" },
                new() { Name = "Arquitetura", Description = "Plantas, fachadas, renders e consultoria" },
                new() { Name = "Fotografia", Description = "Fotos profissionais, edição e tratamento de imagens" },
                new() { Name = "Vídeo & Animação", Description = "Produção e edição de vídeos, motion graphics" },
                new() { Name = "Tradução", Description = "Traduções técnicas e literárias em diversos idiomas" },
                new() { Name = "Consultoria Financeira", Description = "Planejamento financeiro, análise de investimentos" },
                new() { Name = "Aulas & Tutoria", Description = "Aulas particulares, cursos online, mentoria" },
                new() { Name = "Design de Interiores", Description = "Projetos residenciais e comerciais, decoração" },
                new() { Name = "Ilustração", Description = "Arte digital, desenhos, quadrinhos e caricaturas" },
                new() { Name = "Música & Áudio", Description = "Produção musical, jingles, locução e podcast" },
                new() { Name = "Assessoria Jurídica", Description = "Consultoria legal, contratos e documentos" },
                new() { Name = "Consultoria de RH", Description = "Recrutamento, seleção e gestão de pessoas" }
            };
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        // 4. ProfessionalProfiles
        if (await context.ProfessionalProfiles.CountAsync() < 7)
        {
            // Busca usuários com role "profissional" ou "administrador"
            var users = (from user in context.Users
                join ur in context.UserRoles on user.Id equals ur.UserId
                join role in context.Roles on ur.RoleId equals role.Id
                where (role.Name == "profissional" || role.Name == "administrador") && !context.ProfessionalProfiles.Any(p => p.UserId == user.Id)
                select new { user.Id, user.Email }).ToList();

            var faker = new Faker("pt_BR");
            var profiles = new List<ProfessionalProfile>();

            foreach (var user in users)
            {
                var emailName = user.Email?.Split('@')[0] ?? faker.Internet.UserName();
                emailName = char.ToUpper(emailName[0]) + emailName[1..];

                profiles.Add(new ProfessionalProfile
                {
                    UserId = user.Id,
                    ProfessionalName = emailName,
                    Bio = faker.Lorem.Paragraph(),
                    Expertise = faker.Name.JobTitle(),
                    IsPremium = faker.Random.Bool(0.15f),
                    AcademicRegistry = faker.Random.AlphaNumeric(15),
                    Cpf = Cpf.Parse(GenerateCpf()),
                    BirthDate = DateOnly.FromDateTime(faker.Date.Past(50, DateTime.Now.AddYears(-18))),
                    TeachingInstitution = faker.Company.CompanyName() + " University",
                    SkillDolarBalance = new decimal(200.00),
                });
            }

            await context.ProfessionalProfiles.AddRangeAsync(profiles);
            await context.SaveChangesAsync();
        }

        // 5. Services Offered
        if (await context.ServiceOffered.CountAsync() < 12)
        {
            var professionalIds = context.ProfessionalProfiles.Select(p => p.Id).ToList();
            var categoryIds = context.Categories.Select(c => c.Id).ToList();
            var faker = new Faker("pt_BR");
            var services = new List<ServiceOffered>();

            for (var i = 0; i < 12; i++)
            {
                services.Add(new ServiceOffered
                {
                    ProfessionalProfileId = faker.PickRandom(professionalIds),
                    CategoryId = faker.PickRandom(categoryIds),
                    Title = faker.Name.JobTitle(),
                    Description = faker.Lorem.Sentences(3),
                    Price = faker.Random.Decimal(30, 500),
                    CreatedAt = DateTimeOffset.UtcNow.AddDays(-faker.Random.Int(1, 180)),
                });
            }
            await context.ServiceOffered.AddRangeAsync(services);
            await context.SaveChangesAsync();
        }
    }

    private static async Task GenerateUsersWithRolesAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        int numberOfUsers,
        List<string>? userEmails = null,
        string roleName = "profissional")
    {
        var faker = new Faker("pt_BR");
        for (var i = 0; i < numberOfUsers; i++)
        {
            var email = userEmails != null && i < userEmails.Count ? userEmails[i] : faker.Internet.Email();
            if (await userManager.FindByEmailAsync(email) != null) continue;

            var user = new ApplicationUser
            {
                UserName = email, Email = email, EmailConfirmed = true,
                PhoneNumber = faker.Phone.PhoneNumber()
            };
            var result = await userManager.CreateAsync(user, "Senha123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
    
    private static string GenerateCpf()
    {
        var random = new Random();
        var n = new int[11];
        for (var i = 0; i < 9; i++)
            n[i] = random.Next(0, 9);

        var d1 = n[8] * 2 + n[7] * 3 + n[6] * 4 + n[5] * 5 + n[4] * 6 + n[3] * 7 + n[2] * 8 + n[1] * 9 + n[0] * 10;
        d1 = 11 - (d1 % 11);
        if (d1 >= 10) d1 = 0;
        n[9] = d1;

        var d2 = n[9] * 2 + n[8] * 3 + n[7] * 4 + n[6] * 5 + n[5] * 6 + n[4] * 7 + n[3] * 8 + n[2] * 9 + n[1] * 10 + n[0] * 11;
        d2 = 11 - (d2 % 11);
        if (d2 >= 10) d2 = 0;
        n[10] = d2;

        return string.Join("", n);
    }
}
