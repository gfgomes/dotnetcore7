using Microsoft.EntityFrameworkCore;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts => {
    opts.UseSqlServer(builder.Configuration[
        "ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();
app.MapControllerRoute("Default",
    "{controller=Home}/{action=Index}/{id?}"); // mesmo padrão que definir app.MapDefaultControllerRoute();


using(var scope = app.Services.CreateScope())
{
    var serviceProviderDatacontext = scope.ServiceProvider.GetRequiredService<DataContext>();
    SeedData.SeedDatabase(serviceProviderDatacontext);
}
//var context = app.Services.CreateScope().ServiceProvider
//    .GetRequiredService<DataContext>();
//SeedData.SeedDatabase(context);

app.Run();
