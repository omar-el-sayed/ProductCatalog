using Microsoft.AspNetCore.Identity;

namespace ProductCatalog.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
