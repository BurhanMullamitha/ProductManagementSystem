using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.API.Utilities.ErrorResponses;
using ProductManagementSystem.Dal.Core;

namespace ProductManagementSystem.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return ErrorResponse.NotFound(string.Empty);
            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.IsSuccess && result.Value == null)
                return ErrorResponse.NotFound(string.Empty);
            return ErrorResponse.BadRequest(result.Error);
        }
    }
}
