var builder = WebApplication.CreateBuilder(args);

// Configuração para abilir a estrutura MVC Framework
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configuração para abilir a estrutura MVC Framework
//app.MapGet("/", () => "Hello World!");


// Configuração para abilir a estrutura MVC Framework
app.MapDefaultControllerRoute();

app.Run();
