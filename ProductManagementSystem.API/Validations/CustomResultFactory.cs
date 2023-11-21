using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using ProductManagementSystem.API.Utilities.ErrorResponses;

namespace ProductManagementSystem.API.Validations;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        return ErrorResponse.BadRequest(validationProblemDetails.Errors);
    }
}
