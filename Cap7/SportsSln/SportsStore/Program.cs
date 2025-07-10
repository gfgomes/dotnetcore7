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

var app = builder.Build();

//Código padrão da criação do projeto que foi comantado
//app.MapGet("/", () => "Hello World!");

//permite suporte para servir conteúdo estático da pasta wwwroot
app.UseStaticFiles();

app.MapControllerRoute("pagination",
    "Products/Page{productPage}",
    new { Controller = "Home", action = "Index" });

//Fornece o recurso de roteamento para endpoints, permitindo que a aplicação defina rotas para controladores(classes) e ações(metodos).
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
