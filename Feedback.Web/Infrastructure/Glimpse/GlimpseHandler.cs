using System.Web;
using Glimpse.AspNet;

namespace LiteReg.Web.Infrastructure.Glimpse
{
    public class GlimpseHandler : HttpHandler, IHttpHandler
    {
        public new void ProcessRequest(HttpContext context)
        {
            if (GlimpseSecurityPolicy.IsGlimpseAllowed(new HttpContextWrapper(context)))
                base.ProcessRequest(context);
            else
                throw new HttpException(404, "Not Found");
        }
    }
}