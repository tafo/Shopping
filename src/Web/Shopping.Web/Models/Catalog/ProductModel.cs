namespace Shopping.Web.Models.Catalog;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<string> CategoryList { get; set; } = [];
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
}

public record GetProductResponse(IEnumerable<ProductModel> Products);

public record GetProductByCategoryResponse(IEnumerable<ProductModel> ProductList);

public record GetProductByIdResponse(ProductModel Product);