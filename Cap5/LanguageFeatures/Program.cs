var builder = WebApplication.CreateBuilder(args);

// Configura��o para abilir a estrutura MVC Framework
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configura��o para abilir a estrutura MVC Framework
//app.MapGet("/", () => "Hello World!");


// Configura��o para abilir a estrutura MVC Framework
app.MapDefaultControllerRoute();

app.Run();
