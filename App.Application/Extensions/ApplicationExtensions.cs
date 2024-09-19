using App.Application.Features.Categories;
using App.Application.Features.Products;
using App.Persistence.Filters;
using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Persistence.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped(typeof(NotFoundFilter<,>));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //services.AddExceptionHandler<CriticalExceptionHandler>();
            //services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
