using App.Application;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.Created => Created(result.UrlAsCreated, result),
                HttpStatusCode.NoContent => NoContent(),
                _ => new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() }
            };
        }
        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() },
                _ => new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() }
            };
        }
    }
}
