using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace hafta1WebApi.Filters
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        private readonly Stopwatch _stopwatch;
        public CustomActionFilter()
        {
            _stopwatch = Stopwatch.StartNew();       
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string name = context.HttpContext.User.Identity.Name;
            Console.WriteLine(name);
            base.OnActionExecuted(context);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _stopwatch.Stop();
            var elapsed = _stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Runtime: "+ elapsed);
            context.HttpContext.Response.Headers.Add("X-Response-Time-ms", elapsed.ToString());
            base.OnResultExecuting(context);
        }
    }
}
