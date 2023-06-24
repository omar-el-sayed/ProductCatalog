using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.DAL.Models
{
    public class Category : BaseObject
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; }
    }
}
