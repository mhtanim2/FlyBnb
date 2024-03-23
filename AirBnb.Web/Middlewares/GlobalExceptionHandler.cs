using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AirBnb.Web.Middlewares
{
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var errorId = Guid.NewGuid();
            _logger.LogError(
                exception, $"Exception occurred: {errorId}", exception.Message);

            httpContext.Response.Redirect("/Home/Error");
            return true;
        }
    }
}
