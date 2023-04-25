using GungeonApp.WebApp;
using GungeonApp.WebApp.Services;
using GungeonApp.WebApp.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IGungeonService, GungeonService>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});
builder.Services.AddScoped<IRefreshService, RefreshService>();
builder.Services.AddSingleton<InventoryState>();

await builder.Build().RunAsync();
