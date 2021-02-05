using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MVCPhoneServiceWeb.Areas.Identity.IdentityHostingStartup))]
namespace MVCPhoneServiceWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}