using App.Services.Products;

namespace App.Services.Categories.Dtos;
public record CategoryWithProductsDto(int Id, string Name, List<ProductDto> Products);
