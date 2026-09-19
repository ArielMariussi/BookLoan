using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLivros.Data
{
    public static class SeedData
    {
        public const string DemoEmail = "demo@demo.com";
        public const string DemoPassword = "Demo@123";

        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();


            var demoUser = await CreateDemoAccountAsync(userManager);

            if (demoUser == null)
            {
                Console.WriteLine("⚠️ Could not create/find the demo account. Skipping loan seeding.");
                return;
            }


            await CreateDemoLoansAsync(context, demoUser.Id);
        }

        private static async Task<IdentityUser?> CreateDemoAccountAsync(UserManager<IdentityUser> userManager)
        {
            var demoUser = await userManager.FindByEmailAsync(DemoEmail);

            if (demoUser == null)
            {
                demoUser = new IdentityUser
                {
                    UserName = DemoEmail,
                    Email = DemoEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(demoUser, DemoPassword);

                if (result.Succeeded)
                {
                    Console.WriteLine("✅ Demo account created successfully!");
                    return demoUser;
                }
                else
                {
                    Console.WriteLine("❌ Error creating demo account:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"   - {error.Description}");
                    }
                    return null;
                }
            }

            Console.WriteLine("ℹ️ Demo account already exists in the database.");
            return demoUser;
        }

        private static async Task CreateDemoLoansAsync(ApplicationDbContext context, string demoUserId)
        {

            var alreadyHasLoans = await context.Loans
                .AnyAsync(l => l.UserId == demoUserId);

            if (alreadyHasLoans)
            {
                Console.WriteLine("ℹ️ Demo account already has loans registered.");
                return;
            }


            var demoLoans = new List<Loan>
            {
                new Loan
                {
                    Borrower = "Maria Silva",
                    Lender = "Biblioteca Central",
                    BookTitle = "Dom Casmurro - Machado de Assis",
                    LastUpdatedAt = DateTime.UtcNow.AddDays(-2),
                    UserId = demoUserId
                },
                new Loan
                {
                    Borrower = "João Pedro",
                    Lender = "Ana Costa",
                    BookTitle = "1984 - George Orwell",
                    LastUpdatedAt = DateTime.UtcNow.AddDays(-5),
                    UserId = demoUserId
                },
                new Loan
                {
                    Borrower = "Carla Mendes",
                    Lender = "Biblioteca Central",
                    BookTitle = "O Hobbit - J.R.R. Tolkien",
                    LastUpdatedAt = DateTime.UtcNow.AddDays(-1),
                    UserId = demoUserId
                },
                new Loan
                {
                    Borrower = "Roberto Lima",
                    Lender = "Patrícia Souza",
                    BookTitle = "Sapiens - Yuval Noah Harari",
                    LastUpdatedAt = DateTime.UtcNow.AddDays(-7),
                    UserId = demoUserId
                },
                new Loan
                {
                    Borrower = "Fernanda Alves",
                    Lender = "Biblioteca Central",
                    BookTitle = "Cem Anos de Solidão - Gabriel García Márquez",
                    LastUpdatedAt = DateTime.UtcNow.AddDays(-3),
                    UserId = demoUserId
                },
                new Loan
                {
                    Borrower = "Lucas Oliveira",
                    Lender = "Marina Reis",
                    BookTitle = "O Pequeno Príncipe - Antoine de Saint-Exupéry",
                    LastUpdatedAt = DateTime.UtcNow.AddDays(-10),
                    UserId = demoUserId
                },
                new Loan
                {
                    Borrower = "Beatriz Santos",
                    Lender = "Biblioteca Central",
                    BookTitle = "Senhor dos Anéis - J.R.R. Tolkien",
                    LastUpdatedAt = DateTime.UtcNow.AddDays(-4),
                    UserId = demoUserId
                },
                new Loan
                {
                    Borrower = "Gabriel Rocha",
                    Lender = "Camila Ferreira",
                    BookTitle = "A Revolução dos Bichos - George Orwell",
                    LastUpdatedAt = DateTime.UtcNow.AddDays(-6),
                    UserId = demoUserId
                }
            };

            await context.Loans.AddRangeAsync(demoLoans);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ {demoLoans.Count} demo loans created successfully!");
        }
    }
}
