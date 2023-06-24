using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.BL.DTOs
{
    public record ForgetPasswordDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = string.Empty;
    }
}
