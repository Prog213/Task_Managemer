using System.ComponentModel.DataAnnotations;

namespace API.Models.Dtos;

public class RegisterDto
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [RegularExpression("^(?=.*[!@#$%^&*(),.?]).*$", 
        ErrorMessage = "Password must contain at least one special character.")]
    public string Password { get; set; } = null!;
}
