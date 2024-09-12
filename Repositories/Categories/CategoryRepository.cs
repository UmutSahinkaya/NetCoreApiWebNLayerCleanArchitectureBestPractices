
namespace App.Repositories.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    public Task<Category> GetCategoryWithProductsAsync(int id)
    {
        throw new NotImplementedException();
    }
}
