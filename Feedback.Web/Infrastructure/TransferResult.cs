using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LiteReg.Web.Infrastructure
{
    /// <summary>
    /// This is to keep the orginal referer that is useful for search engine analysis. 
    /// </summary>
    public class TransferResult : RedirectResult
    {
        public TransferResult(string url):base(url)
        {
        }

        public TransferResult(object routeValues)
            : base(GetRouteURL(routeValues))
        { 
        
        }

        private static string GetRouteURL(object routeValues)
        {
            UrlHelper url = new UrlHelper(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()), RouteTable.Routes);
            return url.RouteUrl(routeValues);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var httpContext = HttpContext.Current;
            httpContext.Server.TransferRequest(Url, true);          
        }
    }
}