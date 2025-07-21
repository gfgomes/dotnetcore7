using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);


//[builder.Services.]: configura todos os servi�os necess�rios para a aplica��o usando inje��o de depend�ncia

//.AddControllersWithViews(): adiciona suporte para controladores e visualiza��es, permitindo que a aplica��o use o padr�o MVC (Model-View-Controller).


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts => {
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
});

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

//C�digo padr�o da cria��o do projeto que foi comantado
//app.MapGet("/", () => "Hello World!");

//permite suporte para servir conte�do est�tico da pasta wwwroot
app.UseStaticFiles();
app.UseSession();


app.MapControllerRoute("catpage",
    "{category}/Page{productPage:int}",
    new { Controller = "Home", action = "Index" });

app.MapControllerRoute("page", "Page{productPage:int}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("category", "{category}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("pagination",
    "Products/Page{productPage}",
    new { Controller = "Home", action = "Index", productPage = 1 });

//Fornece o recurso de roteamento para endpoints, permitindo que a aplica��o defina rotas para controladores(classes) e a��es(metodos).
app.MapDefaultControllerRoute();
app.MapRazorPages();

SeedData.EnsurePopulated(app);

app.Run();
