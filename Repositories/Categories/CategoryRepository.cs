﻿
using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    public Task<Category?> GetCategoryWithProductsAsync(int id)
    {
        return context.Categories
            .Include(c => c.Products).FirstOrDefaultAsync(x => x.Id == id);
    }
    public IQueryable<Category> GetCategoryByProductsAsync()
    {
       return context.Categories.Include(c => c.Products).AsQueryable();
    }
   
}
