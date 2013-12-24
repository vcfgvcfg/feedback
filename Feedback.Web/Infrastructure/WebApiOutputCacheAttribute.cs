using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Threading;

namespace LiteReg.Web.Infrastructure
{

    // TODO: Remove class if this caching attribute is not used elswhere (currently not being used) [MT]

    /// <summary>
    /// Output caching attribute for ASP >NET Web Api Controllers
    /// </summary>
    public class WebApiOutputCacheAttribute : ActionFilterAttribute
    {
        // Cache length in seconds
        private int _Timespan;

        // Client cache length in seconds
        private int _ClientTimeSpan;

        // Cache for anonymous users only?
        private bool _AnonymousOnly;

        // Cache key
        private string _Cachekey;

        // Cache repository
        private static readonly ObjectCache WebApiCache = MemoryCache.Default;

        private bool _isCacheable(HttpActionContext ac)
        {
            if (_Timespan > 0 && _ClientTimeSpan > 0)
            {
                if (_AnonymousOnly)
                    if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                        return false;
                if (ac.Request.Method == HttpMethod.Get) return true;
            }
            else
            {
                throw new InvalidOperationException("Wrong Arguments");
            }

            return false;
        }

        private CacheControlHeaderValue setClientCache()
        {

            var cachecontrol = new CacheControlHeaderValue();
            cachecontrol.MaxAge = TimeSpan.FromSeconds(_ClientTimeSpan);
            cachecontrol.MustRevalidate = true;
            return cachecontrol;
        }

        public WebApiOutputCacheAttribute(int timespan, int clientTimeSpan, bool anonymousOnly)
        {
            _Timespan = timespan;
            _ClientTimeSpan = clientTimeSpan;
            _AnonymousOnly = anonymousOnly;
        }

        public override void OnActionExecuting(HttpActionContext ac)
        {
            if (ac != null)
            {
                if (_isCacheable(ac))
                {
                    _Cachekey = string.Join(":", new string[] { ac.Request.RequestUri.AbsolutePath, ac.Request.Headers.Accept.FirstOrDefault().ToString() });

                    if (WebApiCache.Contains(_Cachekey))
                    {
                        var val = (string)WebApiCache.Get(_Cachekey);

                        if (val != null)
                        {
                            var contenttype = (MediaTypeHeaderValue)WebApiCache.Get(_Cachekey + ":response-ct");
                            if (contenttype == null)
                                contenttype = new MediaTypeHeaderValue(_Cachekey.Split(':')[1]);

                            ac.Response = ac.Request.CreateResponse();
                            ac.Response.Content = new StringContent(val);

                            ac.Response.Content.Headers.ContentType = contenttype;
                            ac.Response.Headers.CacheControl = setClientCache();
                            return;
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("actionContext");
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (!(WebApiCache.Contains(_Cachekey)))
            {
                var body = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
                WebApiCache.Add(_Cachekey, body, DateTime.Now.AddSeconds(_Timespan));
                WebApiCache.Add(_Cachekey + ":response-ct", actionExecutedContext.Response.Content.Headers.ContentType, DateTime.Now.AddSeconds(_Timespan));
            }

            if (_isCacheable(actionExecutedContext.ActionContext))
                actionExecutedContext.ActionContext.Response.Headers.CacheControl = setClientCache();
        }
    }
}
