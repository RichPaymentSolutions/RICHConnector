using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RICH_Connector.API.Filter
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is Exception ex)
            {

                context.Result = new ObjectResult(new
                {
                    status = false,
                    error = ex.Message
                })
                {
                    StatusCode = 500
                };

                context.ExceptionHandled = true;
            }

            context.HttpContext.Response.Headers.Add("Content-Type", "application/json");
        }
    }
}
