using Microsoft.EntityFrameworkCore;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


//Cria uma scope e obtem o serviço necessário para popular o banco de dados
// (essa abordagem é necessária porque o DataContext é registrado como scoped por padrão)
// como scoped, ele é criado uma vez por requisição, e não uma vez por aplicação
// essa abordagem faz o papel que um request faria, mas sem a necessidade de um request
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();

SeedData.SeedDatabase(context);

app.Run();
