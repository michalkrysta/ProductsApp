using System;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace ProductsApp.Api.Framework
{
    public class LogAttribute : ActionFilterAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Logger.Trace($"Action Method {context.ActionDescriptor.DisplayName} executing at {DateTime.Now.Date}", "Web API Logs");
        }
    }
}