using CleanApp.API.Filters;

namespace CleanApp.API.Extensions
{
    public static class ControllerExtension
    {
        public static IServiceCollection AddControllersWithFiltersExt(this IServiceCollection services)
        {
            services.AddScoped(typeof(NotFoundFilter<,>));
            services.AddControllers(opt =>
            {
                opt.Filters.Add<FluentValidationFilter>();
                opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            return services;
        }
    }
}
