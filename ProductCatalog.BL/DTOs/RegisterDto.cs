﻿using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.BL.DTOs
{
    public record RegisterDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage ="Min Length is 6")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Min Length is 6")]
        [Compare("Password", ErrorMessage = "Password not matched")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
