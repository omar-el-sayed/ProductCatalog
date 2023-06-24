using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.BL.DTOs
{
    public record CategoryDto : BaseObjectDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<ProductDto> Products { get; set; }
    }
}
