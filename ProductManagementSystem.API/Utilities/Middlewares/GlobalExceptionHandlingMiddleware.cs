using ProductManagementSystem.API.Utilities.ErrorResponses;

namespace ProductManagementSystem.API.Utilities.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Guid traceId = Guid.NewGuid();

                _logger.LogError(ex, "Exception message: {@message}, TraceId: {@traceId}", ex.Message, traceId);

                ErrorResponse.InternalServerError(context, traceId);
            }
        }

    }
}
