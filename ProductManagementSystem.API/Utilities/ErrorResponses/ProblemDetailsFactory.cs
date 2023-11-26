using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ProductManagementSystem.API.Utilities.ErrorResponses
{
    public static class ProblemDetailsFactory
    {
        public static ProblemDetails InternalServerError(Guid traceId)
        {
            var problem = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Server error",
                Detail = "An internal server error has occured"
            };
            problem.Extensions.Add("trace_id", traceId.ToString());

            return problem;
        }

        public static ProblemDetails InternalServerError()
        {
            var problem = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Server error",
                Detail = "Something went wrong while processing your request"
            };
            problem.Extensions.Add("trace_id", Guid.NewGuid().ToString());

            return problem;
        }

        public static ProblemDetails BadRequest(IDictionary<string, string[]> validationErrors)
        {
            var problem = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad Request",
                Title = "Bad Request",
                Detail = "Validation(s) failed for request",
            };
            problem.Extensions.Add("errors", validationErrors);

            return problem;
        }

        public static ProblemDetails BadRequest(string message)
        {
            var problem = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad Request",
                Title = "Bad Request",
                Detail = message
            };

            return problem;
        }

        public static ProblemDetails NotFound(string message)
        {
            var problem = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.NotFound,
                Type = "Not Found",
                Title = "Not Found",
                Detail = message
            };

            return problem;
        }
    }
}
