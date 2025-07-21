using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);


//[builder.Services.]: configura todos os serviços necessários para a aplicação usando injeção de dependência

//.AddControllersWithViews(): adiciona suporte para controladores e visualizações, permitindo que a aplicação use o padrão MVC (Model-View-Controller).


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

//Código padrão da criação do projeto que foi comantado
//app.MapGet("/", () => "Hello World!");

//permite suporte para servir conteúdo estático da pasta wwwroot
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

//Fornece o recurso de roteamento para endpoints, permitindo que a aplicação defina rotas para controladores(classes) e ações(metodos).
app.MapDefaultControllerRoute();
app.MapRazorPages();

SeedData.EnsurePopulated(app);

app.Run();
