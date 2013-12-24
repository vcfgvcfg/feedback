using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteReg.Web.Infrastructure
{
    public class Consts
    {
        public const string TrackingCookie = "trackingCookie";
        public const string CheckUserSignInCookie = "SignInCookie";
        public static readonly string[] BarColors =
        { 
            "chart-orange", "chart-green" , "chart-purple", "chart-blue" ,
            "chart-red", "chart-teal" ,"chart-pink", "chart-lavender"
        };

        public const string UPCOMMINGEVENTS = "Upcoming Events";
        public const string PASTEVENTS = "Past Events";
    }
    public class Utility
    {
        public static string Truncate(string str, int length)
        {
            if (str != null && str.Length <= length)
            {
                return str;
            }
            return str.Substring(0, length - 3);

        }
       
        public static string TruncateWithDotted(string str, int length)
        {

            if (str != null && str.Length <= length)
            {
                return str;
            }
            return str.Substring(0, length - 3) + "...";


        }

        /// <summary>
        /// Truncate on space closest to length max
        /// </summary>
        /// <param name="str">Text to be truncated</param>
        /// <param name="length">Max length of string</param>
        /// <returns></returns>
        public static string TruncateOnWordWithDotted(string str, int length)
        {
            if (!string.IsNullOrEmpty(str) && str.Length <= length) return str;

            str = str.Substring(0, length - 3);
            var index = str.LastIndexOf(" ", System.StringComparison.Ordinal);
            if (index <= 0) return str + "...";
            return str.Substring(0, index) + "...";
        }

        /// <summary>
        /// truncate long title with tooltip
        /// </summary>
        /// <param name="text">test too be trucated</param>
        /// <param name="length">standar length,if show tooltip</param>
        /// <returns></returns>
        public static MvcHtmlString TruncateWithTooltip(string text, int length)
        {
            var value = string.Empty;
            if (!string.IsNullOrEmpty(text) && text.Length > length)
                value = string.Format("rel = 'tooltip' title='{0}'", text);
            return MvcHtmlString.Create(value);
        }
        /// <summary>
        /// truncate long title with tooltip
        /// </summary>
        /// <param name="text">test too be trucated</param>
        /// <param name="length">standar length,if show tooltip</param>
        /// <returns></returns>
        public static MvcHtmlString TruncateWithColorDotted(string text, int length)
        {

            if (!string.IsNullOrEmpty(text) && text.Length > length)
                text = string.Format("{0}<span class=\"dotted\">...</span>", Truncate(text, length));
            return MvcHtmlString.Create(text);
        }
    }

}