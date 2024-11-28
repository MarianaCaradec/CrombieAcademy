using BibliotecaAPIWeb.Utilities;

namespace BibliotecaAPIWeb.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly FileLogger _logger;

        public ErrorLoggingMiddleware(RequestDelegate next, FileLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error procesando la solicitud:" +
                    $"{context.Request.Method} {context.Request.Path}", ex);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Ocurrió un error interno en el servidor");
            }
        }
    }
}
