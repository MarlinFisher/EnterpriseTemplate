using EnterpriseTemplate;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace EnterpriseTemplate
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}