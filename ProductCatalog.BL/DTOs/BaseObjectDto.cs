namespace ProductCatalog.BL.DTOs
{
    public record BaseObjectDto
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
