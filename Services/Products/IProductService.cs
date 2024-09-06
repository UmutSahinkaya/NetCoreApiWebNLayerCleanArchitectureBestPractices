using App.Repositories.Products;

namespace App.Services.Products;

public interface IProductService
{
    Task<ServiceResult<List<Product>>> GetTopPriceProductsAsync(int count);
}
