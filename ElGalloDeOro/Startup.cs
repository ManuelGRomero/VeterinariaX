using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElGalloDeOro.Startup))]
namespace ElGalloDeOro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
