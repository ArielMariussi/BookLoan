using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLivros.Services
{
    public class ResetDemoDataService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ResetDemoDataService> _logger;

        // ⏰ How often to reset (30 minutes)
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

        public ResetDemoDataService(
            IServiceProvider serviceProvider,
            ILogger<ResetDemoDataService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("🔄 ResetDemoDataService started. Reset every {Hours}h.", _interval.TotalHours);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Wait for the interval BEFORE resetting
                    // (on first run, wait so it doesn't reset right after startup)
                    await Task.Delay(_interval, stoppingToken);

                    await ResetDemoDataAsync();
                }
                catch (OperationCanceledException)
                {
                    // App is shutting down, nothing wrong here
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Error resetting demo data.");
                }
            }

            _logger.LogInformation("🛑 ResetDemoDataService stopped.");
        }

        private async Task ResetDemoDataAsync()
        {
            _logger.LogInformation("🔄 Starting demo data reset...");

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // 1. Find demo user
            var demoUser = await userManager.FindByEmailAsync(SeedData.DemoEmail);

            if (demoUser == null)
            {
                _logger.LogWarning("⚠️ Demo account not found. Skipping reset.");
                return;
            }

            // 2. Delete ALL demo loans
            var demoLoans = await context.Loans
                .Where(l => l.UserId == demoUser.Id)
                .ToListAsync();

            if (demoLoans.Any())
            {
                context.Loans.RemoveRange(demoLoans);
                await context.SaveChangesAsync();
                _logger.LogInformation("🗑️ {Count} demo loans deleted.", demoLoans.Count);
            }

            // 3. Recreate nice demo loans
            var newDemoLoans = CreateDemoLoansList(demoUser.Id);
            await context.Loans.AddRangeAsync(newDemoLoans);
            await context.SaveChangesAsync();

            _logger.LogInformation("✅ {Count} demo loans recreated successfully!", newDemoLoans.Count);
        }

        private static List<Loan> CreateDemoLoansList(string demoUserId)
        {
            return new List<Loan>
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
        }
    }
}
