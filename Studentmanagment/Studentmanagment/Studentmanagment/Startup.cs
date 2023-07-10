using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Studentmanagment.Startup))]
namespace Studentmanagment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
