using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestaurantApp.Application.Exceptions;
using RestaurantApp.Application.Logging;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorLogger _errorLogger;

        public ExceptionHandlingMiddleware(RequestDelegate next, IErrorLogger logger)
        {
            _next = next;
            _errorLogger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Call the next delegate/middleware in the pipeline.
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 422;

                var errors = ex.Errors.Select(x => new
                {
                    x.ErrorMessage,
                    x.PropertyName
                });

                await context.Response.WriteAsJsonAsync(errors);
            }
            catch (UnauthorizedUseCaseExecutionException ex)
            {
                context.Response.StatusCode = 401;
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = 401;
            }
            catch (EntityNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message
                });
            }
            catch (System.Exception ex)
            {
                Guid errorId = Guid.NewGuid();
                AppError error = new AppError
                {
                    Exception = ex,
                    ErrorId = errorId,
                    Username = "test"
                };
                _errorLogger.Log(error);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var responseBody = new
                {
                    message = $"There was an error, please contact support with this error code: {errorId}."
                };

                await context.Response.WriteAsJsonAsync(responseBody);
            }
            }
        }
}
