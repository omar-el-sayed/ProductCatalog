using System.ComponentModel;

namespace ProductCatalog.DAL.Models
{
    public class BaseObject
    {
        public int Id { get; set; }

        [DefaultValue(typeof(DateTime), "")]
        public DateTime CreatedOn { get; set; }
    }
}
