using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QQute.Filters
{
    public class CacheAF : ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }



        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                Private = true,
                Public = false,
                NoStore = false,
                MaxAge = TimeSpan.FromSeconds(10)
            };

            //Pragma
            actionExecutedContext.Response.Headers.Date = DateTime.UtcNow;
            actionExecutedContext.Response.Content.Headers.Expires = DateTime.UtcNow.AddSeconds(10);




            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }


    }
}