using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Automata_BugTracker.Startup))]
namespace Automata_BugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
