namespace Catalog.API.Domain
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public List<string> CategoryList { get; set; } = [];
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
