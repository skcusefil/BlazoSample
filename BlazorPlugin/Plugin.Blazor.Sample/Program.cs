using Microsoft.Extensions.FileProviders;
using Plugin.Blazor.Sample.Data;
using Plugin.Shared;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddPluginServices();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//plugin
app.UsePluginMiddleware();


app.UseHttpsRedirection();

app.UseStaticFiles();

//var root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

//app.UseFileServer(new FileServerOptions
//{
//    FileProvider = new PhysicalFileProvider(
//           Path.Combine(root, "Plugins")),
//    RequestPath = "/Plugins",
//    EnableDirectoryBrowsing = true
//});



app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
