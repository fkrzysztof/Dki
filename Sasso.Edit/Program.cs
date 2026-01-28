using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sasso.Edit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}



//using System.Globalization;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Localization;


//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//builder.Services.AddControllersWithViews()
//    .AddViewLocalization()
//    .AddDataAnnotationsLocalization();

//var app = builder.Build();

//var supportedCultures = new[]
//{
//    new CultureInfo("pl-PL"),
//    new CultureInfo("en-US"),
//    new CultureInfo("uk-UA")
//};

//var localizationOptions = new RequestLocalizationOptions
//{
//    DefaultRequestCulture = new RequestCulture("pl-PL"),
//    SupportedCultures = supportedCultures,
//    SupportedUICultures = supportedCultures
//};

//localizationOptions.RequestCultureProviders = new IRequestCultureProvider[]
//{
//    new CookieRequestCultureProvider(),
//    new AcceptLanguageHeaderRequestCultureProvider()
//};

//app.UseRequestLocalization(localizationOptions);

//app.UseStaticFiles();
//app.UseRouting();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
