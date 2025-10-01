using Microsoft.EntityFrameworkCore;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


//Cria uma scope e obtem o servi�o necess�rio para popular o banco de dados
// (essa abordagem � necess�ria porque o DataContext � registrado como scoped por padr�o)
// como scoped, ele � criado uma vez por requisi��o, e n�o uma vez por aplica��o
// essa abordagem faz o papel que um request faria, mas sem a necessidade de um request
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();

SeedData.SeedDatabase(context);

app.Run();
