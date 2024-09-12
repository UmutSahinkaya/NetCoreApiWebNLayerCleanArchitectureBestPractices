using App.Repositories;
using App.Repositories.Products;
using App.Services.ExceptionHandlers;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork,IValidator<CreateProductRequest> createProductRequestValidator,IMapper mapper) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);
        var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto
        };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();

        #region manuel mapping
        //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        #endregion

        var productsAsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int page, int pageSize)
    {
        int skip = (page - 1) * pageSize;
        var products = await productRepository.GetAll().Skip(skip).Take(pageSize).ToListAsync();
        #region manuel mapping
        //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        #endregion
        var productsAsDto = mapper.Map<List<ProductDto>>(products);

        
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
        }
        #region manuel mapping
        //var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);
        #endregion

        var productAsDto = mapper.Map<ProductDto>(product);
        return ServiceResult<ProductDto>.Success(productAsDto)!;
    }
    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        //throw new CriticalException("kritik seviyede bir hata meydana geldi");
        //throw new Exception("Db hatası");

        // Async manuel service business check
        var anyProduct = await productRepository.Where(p => p.Name == request.Name).AnyAsync();
        if (anyProduct)
        {
            return ServiceResult<CreateProductResponse>.Fail("Ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);
        }

        #region Async manuel fluent validation business check
        //var validationResult = await createProductRequestValidator.ValidateAsync(request);

        //if (!validationResult.IsValid)
        //{
        //    return ServiceResult<CreateProductResponse>.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        //}
        #endregion

        var product = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),$"api/products/{product.Id}");
    }
    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {
        //Fast Fail
        //Guard Clauses (Önce olumsuzları yaz)


        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        var isProductNameExist = await productRepository.Where(p => p.Name == request.Name && p.Id!=id).AnyAsync();
        if (isProductNameExist)
        {
            return ServiceResult.Fail("Ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult>  UpdateStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.productId);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }
        product.Stock = request.quantity;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }


    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }
        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
