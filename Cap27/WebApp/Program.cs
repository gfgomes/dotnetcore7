using Microsoft.EntityFrameworkCore;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration[
        "ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<CitiesData>();

var app = builder.Build();

app.UseStaticFiles();
//app.MapControllers();
//app.MapDefaultControllerRoute();
app.MapControllerRoute("forms",
    "controllers/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var serviceProviderDatacontext = scope.ServiceProvider.GetRequiredService<DataContext>();
    SeedData.SeedDatabase(serviceProviderDatacontext);
}

app.Run();
