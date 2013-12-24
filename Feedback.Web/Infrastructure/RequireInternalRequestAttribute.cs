using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteReg.Web.Infrastructure
{
    /// <summary>
    /// Ensures the request is coming form an internal server and does not
    /// allow requests from external Internet clients.
    /// </summary>
    public class RequireInternalRequestAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
            if (filterContext.HttpContext.Request.Url == null)
                throw new ArgumentNullException("Request.Url");

            string host = filterContext.HttpContext.Request.UserHostAddress;
            if (!host.StartsWith("10.") && !host.StartsWith("127."))
                throw new InvalidOperationException("External requests not allowed.");
        }
    }
}
