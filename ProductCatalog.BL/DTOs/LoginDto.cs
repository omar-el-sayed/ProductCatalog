using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.BL.DTOs
{
    public record LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Min Length is 6")]
        public string Password { get; set; } = string.Empty;

        public bool Remember { get; set; }
    }
}
