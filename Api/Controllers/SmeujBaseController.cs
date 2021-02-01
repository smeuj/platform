using Microsoft.AspNetCore.Mvc;
using Nouwan.Smeuj.Framework;

namespace Nouwan.Smeuj.Api.Controllers
{
    public class SmeujBaseController : ControllerBase
    {
        public ObjectResult ReturnResult<T>(Result<T> result)
        {
            return result.ResultType switch
            {
                ResultType.Ok => Ok(result),
                ResultType.NotFound => NotFound(result),
                ResultType.Unprocessable => Conflict(result),
                _ => BadRequest("Could not parse your request")
            };
        }
    }
}
