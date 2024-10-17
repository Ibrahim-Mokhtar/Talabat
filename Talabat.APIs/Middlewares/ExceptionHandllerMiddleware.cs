using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Text.Json;
using Talabat.API.Controllers.Errors;
using Talabat.Core.Application.Common.Exceptions;

namespace Talabat.APIs.Middlewares
{
    public class ExceptionHandllerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandllerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandllerMiddleware(RequestDelegate next,
                                                 ILogger<ExceptionHandllerMiddleware> logger,
                                                 IWebHostEnvironment env
                                                 )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Logic Executed with the Request

                await _next(httpContext);

                // Logic Executed with the Response


            }
            catch (Exception ex)
            {
                #region Logging
                if (_env.IsDevelopment())
                {
                    // Development Mode

                    _logger.LogError(ex, ex.Message);
                }

                else
                {
                    // Production
                    /// Log Exception Details in Database | File (Text,Json)
                }
                #endregion
                await HandleExceptionAsync(httpContext, ex);

            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ApiRespons response;
            switch (ex)
            {
                case NotFoundException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiRespons(404, ex.Message);
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case BadRequestException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiRespons(404, ex.Message);
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                default:
                    response = _env.IsDevelopment() ?
                          response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                          response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
            }
        }
    }
}
