using App.Repositories.Products;
using FluentValidation;

namespace App.Services.Products.Create;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository _productRepository;
    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        // Name Validation
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün ismi gereklidir.")
            .Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.");
        //.MustAsync(MustUniqueProductNameAsync).WithMessage("Ürün ismi veritabanında bulunmaktadır.");
        //.Must(MustUniqueProductName).WithMessage("Ürün ismi veritabanında bulunmaktadır.");

        // Price Validation
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

        //Stock Incliusive Validation
        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasında olmalıdır.");
        _productRepository = productRepository;
    }
    #region 2 Way Async Validation
    //private async Task<bool> MustUniqueProductNameAsync(string name, CancellationToken arg2)
    //{
    //    return !await _productRepository.Where(x => x.Name == name).AnyAsync(arg2);
    //}
    #endregion

    #region One Way Sync Validation
    //private bool MustUniqueProductName(string name)
    //{
    //    //false => bir hata var // true => Bir hata yok. 
    //    return !_productRepository.Where(x => x.Name == name).Any();
    //}
    #endregion
}
