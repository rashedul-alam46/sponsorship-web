using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sponsorship;
using Sponsorship.Services;
using SponsorshipWeb;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


// Bind ApiSettings from appsettings.json
var apiSettings = new ApiSettings();
builder.Configuration.GetSection("ApiSettings").Bind(apiSettings);
builder.Services.AddSingleton(apiSettings);

// Register services for dependency injection
builder.Services.AddScoped<SponsorshipService>();
builder.Services.AddScoped<AccountAuthService>();
builder.Services.AddScoped<SponsorshipTypeService>();
builder.Services.AddScoped<DropdownService>();

await builder.Build().RunAsync();
