var builder = WebApplication.CreateBuilder(args);
var app = builder.Build(); 


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error.xhtml");
    app.UseStaticFiles();
}
app.Run(context =>
{
    throw new Exception("Something has gone wrong");
});

app.Run();