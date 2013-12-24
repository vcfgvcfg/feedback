using System;
using System.Web;
using System.Web.Mvc;

namespace LiteReg.Web.Infrastructure
{
    public class CookieManager
    {
        private static readonly string trakingCookie = "Guid: {0} User AUS ID: {1} User Agency ID: {1}";
        public static void CreateCookieTracking(Controller controller)
        {
            if (controller.Request.Cookies[Consts.TrackingCookie] == null)
            {
                HttpCookie cookie = new HttpCookie(Consts.TrackingCookie);
                cookie.Value = Guid.NewGuid().ToString();
                cookie.Expires = DateTime.Now.AddYears(10);//want to use DateTime.MaxValue, but this property seems not supported by some browsers
                controller.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
        }

        public static void CreateSignInOrNotCookie(Controller controller)
        {
            if (controller.Request.Cookies[Consts.CheckUserSignInCookie] == null)
            {
                HttpCookie cookie = new HttpCookie(Consts.CheckUserSignInCookie);
                cookie.Value = "true";
                cookie.Expires = DateTime.Now + System.Web.Security.FormsAuthentication.Timeout;
                controller.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
        }

        public static void ClearSignInCookie(Controller controller)
        {
            if ( controller.Request.Cookies[Consts.CheckUserSignInCookie]!= null)
            {
                var cookie = controller.Request.Cookies[Consts.CheckUserSignInCookie];
                cookie.Expires = DateTime.Now.AddDays(-1);
                controller.Request.Cookies.Remove(Consts.CheckUserSignInCookie);
                controller.ControllerContext.HttpContext.Response.AppendCookie(cookie);
            }
         
        }

        public static void UpdateTrackingCookie(Controller controller, string userAUSID, string userAgencyID)
        {
            var cookie = controller.Request.Cookies[Consts.TrackingCookie];
            if (cookie == null)
            {
                CreateCookieTracking(controller);
                cookie = controller.Request.Cookies[Consts.TrackingCookie];
            }
            if (cookie.Value.Contains("User AUS ID"))//update userid and useragencyID in case differet user logins on same machine.
            {
                cookie.Value = cookie.Value.Remove(cookie.Value.IndexOf("User AUS ID"));
                cookie.Value = cookie.Value.Replace("Guid:", string.Empty);
                cookie.Value = cookie.Value.Replace(" ", string.Empty);
            }

            cookie.Value = string.Format(trakingCookie, cookie.Value, userAUSID, userAgencyID);
            controller.ControllerContext.HttpContext.Response.Cookies.Set(cookie);
        }

        public static void SetFromFBSync(Controller controller)
        {
            HttpCookie c = new HttpCookie("FromSyncFB", "true");
            c.Expires = DateTime.Now.AddSeconds(30);
            controller.Response.Cookies.Add(c);
        }

        public static bool GetExpireFromFBSync(Controller controller)
        {
            if (controller.Request.Cookies["FromSyncFB"] != null && controller.Request.Cookies["FromSyncFB"].Value == "true")
            {
                var c = new HttpCookie("FromSyncFB");
                c.Expires = DateTime.Now.AddDays(-1);
                controller.Response.Cookies.Add(c);
                return true;
            }
            return false;
        }

        public static bool GetPrivacyPolicyClosed(HttpRequestBase request)
        {
            var cookie = request.Cookies["privacy_message_closed"];

            return cookie != null && cookie.Value == "true";
        }
    }
}