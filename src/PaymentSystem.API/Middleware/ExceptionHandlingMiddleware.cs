using PaymentsSystem.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace PaymentMicroservice.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, message) = exception switch
            {
                CustomerNotFoundException ex
                    => (HttpStatusCode.NotFound, ex.Message),

                InvalidOperationException ex
                    => (HttpStatusCode.BadRequest, ex.Message),

                FluentValidation.ValidationException ex
                    => (HttpStatusCode.UnprocessableEntity,
                        string.Join(", ", ex.Errors.Select(e => e.ErrorMessage))),

                _ => (HttpStatusCode.InternalServerError,
                        "Ocurrió un error interno. Intenta de nuevo más tarde.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                status = (int)statusCode,
                message,
                timestamp = DateTime.UtcNow
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
