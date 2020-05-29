using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BookStore.WebService.Startup))]

namespace BookStore.WebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureIoc();
            ConfigureAuth(app);
        }
    }
}
