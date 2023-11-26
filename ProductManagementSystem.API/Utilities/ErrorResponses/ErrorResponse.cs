using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ProductManagementSystem.API.Utilities.ErrorResponses
{
    public static class ErrorResponse
    {
        public static async void InternalServerError(HttpContext context, Guid traceId)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problemDetails = ProblemDetailsFactory.InternalServerError(traceId);
            var json = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }

        public static IActionResult InternalServerError()
        {
            ProblemDetails problemDetails = ProblemDetailsFactory.InternalServerError();

            return new ObjectResult(problemDetails);
        }

        public static IActionResult BadRequest(IDictionary<string, string[]> valdiationErrors)
        {
            ProblemDetails problemDetails = ProblemDetailsFactory.BadRequest(valdiationErrors);

            return new BadRequestObjectResult(problemDetails);
        }

        public static IActionResult BadRequest(string message)
        {
            ProblemDetails problemDetails = ProblemDetailsFactory.BadRequest(message);

            return new BadRequestObjectResult(problemDetails);
        }

        public static IActionResult NotFound(string message)
        {
            ProblemDetails problemDetails = ProblemDetailsFactory.NotFound(message);

            return new BadRequestObjectResult(problemDetails);
        }
    }
}
