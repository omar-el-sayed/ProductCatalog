using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.BL.DTOs
{
    public record ProductDto : BaseObjectDto
    {
        [Required, MaxLength(100, ErrorMessage = "Max Length of Name field is 100")]
        public string Name { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "The Price field must be greater than or equal to {1}.")]
        public double Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The Duration field must be greater than or equal to {1}.")]
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryDto? Category { get; set; }
    }
}
