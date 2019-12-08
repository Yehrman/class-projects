using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeSheetDbConn.Startup))]
namespace TimeSheetDbConn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
