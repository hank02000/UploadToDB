using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Upload.Startup))]
namespace MVC_Upload
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
