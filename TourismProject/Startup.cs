using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TourismProject.Startup))]
namespace TourismProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
