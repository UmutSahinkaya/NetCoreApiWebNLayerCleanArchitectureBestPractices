using CleanApp.API.ExceptionHandlers;

namespace CleanApp.API.Extensions;

public static class ExceptionHandlersExtension
{
    public static IServiceCollection AddExceptionHandlerExt(this IServiceCollection services)
    {
        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
    
}
