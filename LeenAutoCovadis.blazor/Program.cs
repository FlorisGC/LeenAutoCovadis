using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LeenAutoCovadis.shared.Clients;
using LeenAutoCovadis.blazor.Handler;
using Microsoft.AspNetCore.Components.Authorization;
using LeenAutoCovadis.blazor.Services;
using LeenAutoCovadis.shared.Interfaces;

namespace LeenAutoCovadis.blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped<UserHttpClient>();
            builder.Services.AddHttpClient(nameof(UserHttpClient)).AddHttpMessageHandler<AuthorizationMessageHandler>();
            builder.Services.AddScoped<LocalStorageService>();
            builder.Services.AddScoped<ICurrentUserContext, CurrentUserContext>();

            builder.Services.AddScoped<AuthorizationMessageHandler>();
            builder.Services.AddScoped<AuthHttpClient>();
            builder.Services.AddScoped<LeenAutoCovadisAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<LeenAutoCovadisAuthenticationStateProvider>());
            builder.Services.AddAuthorizationCore();


            builder.Services.AddScoped<CarHttpClient>();

            await builder.Build().RunAsync();
        }
    }
}