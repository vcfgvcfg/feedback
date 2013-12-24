using System.Web;
using Glimpse.AspNet.Extensions;
using Glimpse.Core.Extensibility;
using LiteReg.Core.Utility;

namespace LiteReg.Web.Infrastructure.Glimpse
{
    public class GlimpseSecurityPolicy : IRuntimePolicy
    {
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            var httpContext = policyContext.GetHttpContext();
            return IsGlimpseAllowed(httpContext) ? RuntimePolicy.On : RuntimePolicy.Off;
        }

        public static bool IsGlimpseAllowed(HttpContextBase httpContext)
        {
            // bypass check for local requests
            if (httpContext.Request.IsLocal) return true;

            // ensure remote access token is valid
            var remoteAccessCookie = httpContext.Request.Cookies[GlimpseUtility.REMOTE_ACCESS_TOKEN_COOKIE_NAME];
            bool allowRemoteAccess = remoteAccessCookie != null &&
                GlimpseUtility.IsValidRemoteAccessToken(remoteAccessCookie.Value);
            return allowRemoteAccess;
        }

        public RuntimeEvent ExecuteOn
        {
            get { return RuntimeEvent.EndRequest; }
        }
    }
}