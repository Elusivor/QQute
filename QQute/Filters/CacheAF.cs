using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QQute.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CacheAF : ActionFilterAttribute
    {
        private bool _allowCache;

        public CacheAF(bool DoYouWantCache = false)
        {
            _allowCache = DoYouWantCache;
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (_allowCache)
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    Private = true,
                    Public = false,
                    NoStore = false,
                    MaxAge = TimeSpan.FromSeconds(10)
                };

                actionExecutedContext.Response.Headers.Date = DateTime.UtcNow;
                actionExecutedContext.Response.Content.Headers.Expires = DateTime.UtcNow.AddSeconds(10);
            }

            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TimerAF : ActionFilterAttribute
    {
        const string _header = "QQ_API_Timer";
        private string _property;
        private string _timerName;

        public TimerAF(string timerName = "")
        {
            _timerName = timerName;
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            _property = $"{_timerName}_{actionContext.ActionDescriptor.ActionName}";
            actionContext.Request.Properties.Add(_property, Stopwatch.StartNew());

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var timer = actionExecutedContext.Request.Properties[_property];
            actionExecutedContext.Response.Headers.Add(_header, $"{((Stopwatch)timer).ElapsedMilliseconds.ToString()} ms");

            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}