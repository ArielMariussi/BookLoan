using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.Models;

public class Loan
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Enter the borrower's name!")]
    public string Borrower { get; set; }

    [Required(ErrorMessage = "Enter the lender's name!")]
    public string Lender { get; set; }

    [Required(ErrorMessage = "Enter the book title!")]
    public string BookTitle { get; set; }
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public string UserId { get; set; } = string.Empty;
}
