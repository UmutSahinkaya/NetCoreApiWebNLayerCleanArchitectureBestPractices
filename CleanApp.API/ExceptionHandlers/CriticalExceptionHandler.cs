using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CleanApp.API.ExceptionHandlers;

public class CriticalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //business logic
        if (exception is CriticalException)
        {
            Console.WriteLine("Hata ile ilgili sms gönderildi.");
        }
        return ValueTask.FromResult(false);
    }
}
