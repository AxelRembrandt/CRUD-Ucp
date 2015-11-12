using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ucp.Startup))]
namespace Ucp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
