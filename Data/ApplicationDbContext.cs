using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLivros.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext(options)

    {


    public DbSet<Loan> Loans { get; set; }


    }
}
