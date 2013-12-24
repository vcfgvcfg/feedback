using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Feedback.Web.Startup))]
namespace Feedback.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
