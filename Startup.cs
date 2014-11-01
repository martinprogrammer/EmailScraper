using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmailScraper.Startup))]
namespace EmailScraper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
