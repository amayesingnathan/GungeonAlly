using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using GungeonAlly.WebApp.Services;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("EtGDb");
if (connString == null)
    throw new ConfigurationErrorsException("Missing connection string.");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IGungeonService>(new GungeonService(connString));
builder.Services.AddSingleton<InventoryState>();
builder.Services.AddSingleton<PageHistory>();
builder.Services.AddScoped<IRefreshService, RefreshService>();
builder.Services.AddScoped<PageManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
