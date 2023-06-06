using BlazorWasmAtuhSample;
using BlazorWasmAtuhSample.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region Authentication
//เพิ่ม service เพื่อใช้งาน Authentication
builder.Services.AddScoped<AuthenticationStateProvider, AppAuthenticationState>();
builder.Services.AddAuthorizationCore();
#endregion

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
