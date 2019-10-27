using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using ProductsApp.Infrastructure.Exceptions;

namespace ProductsApp.Api.Framework
{
    public class ExceptionHandlerMiddleware
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
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
            var errorCode = "Error";
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();
            switch (exception)
            {
                case ServiceException e when exceptionType == typeof(ServiceException):
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;
                case Exception _ when exceptionType == typeof(Exception):
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new {Code = errorCode, exception.Message};
            var payload = JsonConvert.SerializeObject(response);

            Logger.Error($"Code: {response.Code}, Message: {response.Message}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;
            return context.Response.WriteAsync(payload);
        }
    }
}