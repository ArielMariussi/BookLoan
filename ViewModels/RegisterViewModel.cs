using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email is required!")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;


    [Required(ErrorMessage = "Password is required!")]
    [StringLength(100, ErrorMessage = "Password must be at least {2} characters long", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
