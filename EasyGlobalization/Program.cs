using EasyGlobalization.Data;
using EasyGlobalization.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("ru"),
        new CultureInfo("de"),
        new CultureInfo("uz")
    };  

    options.DefaultRequestCulture = new RequestCulture("ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LocalizationContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("LocalizationConString")));

builder.Services.AddTransient<IStringLocalizer, EFStringLocalizer>();
builder.Services.AddSingleton<IStringLocalizerFactory>(new EFStringLocalizerFactory(builder.Configuration.GetConnectionString("LocalizationConString")));

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
                    .AddDataAnnotationsLocalization(options =>
                    {
                        options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(null);
                    })
                   .AddViewLocalization();// добавляем локализацию представлений;
var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseRequestLocalization();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Resources}/{action=Index}/{id?}");
});


app.Run();
