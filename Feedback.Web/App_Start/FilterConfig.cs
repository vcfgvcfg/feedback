﻿using Feedback.Web.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace Feedback.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandledErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
