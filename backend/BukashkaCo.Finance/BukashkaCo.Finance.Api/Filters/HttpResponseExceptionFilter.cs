using System;
using BukashkaCo.Finance.Api.ResourceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BukashkaCo.Finance.Api.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                var test = new {Error = exception.Error};
                context.Result = new ObjectResult(test)
                {
                    StatusCode = exception.StatusCode
                };
                context.ExceptionHandled = true;
            }
        }
    }
}