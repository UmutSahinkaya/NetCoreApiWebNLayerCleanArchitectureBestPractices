using App.Services.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

public class CategoriesController(ICategoryService categoryService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCategories() => CreateActionResult(await categoryService.GetCategoriesAsync());
    [HttpGet("{id}:int")]
    public async Task<IActionResult> GetCategoryById(int id) => CreateActionResult(await categoryService.GetCategoryByIdAsync(id));
    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetCategoryWithProducts(int id) => CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));

    [HttpGet("products")]
    public async Task<IActionResult> GetCategoryWithProducts() => CreateActionResult(await categoryService.GetCategoryWithProductsAsync());

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest createCategoryDto) => CreateActionResult(await categoryService.CreateCategoryAsync(createCategoryDto));
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequest updateCategoryDto) => CreateActionResult(await categoryService.UpdateCategoryAsync(id, updateCategoryDto));
}
