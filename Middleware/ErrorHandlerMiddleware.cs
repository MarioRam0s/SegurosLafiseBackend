using System.Net;
using System.Text.Json;

namespace SegurosLafiseBackend.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message = exception.Message;

            // Determinar código HTTP según el tipo de excepción
            switch (exception)
            {
                case KeyNotFoundException: // por ejemplo, 404
                    status = HttpStatusCode.NotFound;
                    break;
                case ArgumentException: // 400
                case InvalidOperationException:
                    status = HttpStatusCode.BadRequest;
                    break;
                default: // 500
                    status = HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(result);
        }
    }
}
