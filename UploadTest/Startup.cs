using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UploadTest.Startup))]
namespace UploadTest
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
