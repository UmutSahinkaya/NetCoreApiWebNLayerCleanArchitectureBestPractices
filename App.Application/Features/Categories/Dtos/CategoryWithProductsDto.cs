using App.Application.Features.Products.Dtos;

namespace App.Application.Features.Categories.Dtos;
public record CategoryWithProductsDto(int Id, string Name, List<ProductDto> Products);
