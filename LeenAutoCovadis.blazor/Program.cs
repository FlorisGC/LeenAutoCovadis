using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LeenAutoCovadis.shared.Clients;

namespace LeenAutoCovadis.blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<UserHttpClient>();

            await builder.Build().RunAsync();
        }
    }
}