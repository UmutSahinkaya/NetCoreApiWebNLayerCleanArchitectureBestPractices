using App.Repositories.Products;
using FluentValidation;

namespace App.Services.Products.Update;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
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
    }
}
