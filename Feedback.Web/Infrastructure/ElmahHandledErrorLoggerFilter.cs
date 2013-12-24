using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Feedback.Web.Infrastructure
{
    public class ElmahHandledErrorLoggerFilter : System.Web.Mvc.IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Log only handled exceptions, because all other will be caught by ELMAH anyway.
            if (context.ExceptionHandled)
            {
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
            }
        }
    }
}