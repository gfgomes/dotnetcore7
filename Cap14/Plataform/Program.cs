//using Platform;
using Platform.Services;

var builder = WebApplication.CreateBuilder(args);

//IConfiguration config = builder.Configuration;

//builder.Services.AddScoped<IResponseFormatter>(serviceProvider => { 
//    string? typeName = config["services:IResponseFormatter"];
//    return (IResponseFormatter)ActivatorUtilities
//        .CreateInstance(serviceProvider, typeName == null
//            ? typeof(GuidService) : Type.GetType(typeName, true)!); 
//});
//builder.Services.AddScoped<ITimeStamper, DefaultTimeStamper>();

builder.Services.AddScoped<IResponseFormatter, TextResponseFormatter>();
builder.Services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
builder.Services.AddScoped<IResponseFormatter, GuidService>();

var app = builder.Build();

//app.UseMiddleware<WeatherMiddleware>();

//app.MapEndpoint<WeatherEndpoint>("endpoint/class");

//app.MapGet("endpoint/function", 
//    async (HttpContext context, IResponseFormatter formatter) => { 
//        await formatter.Format(context, 
//            "Endpoint Function: It is sunny in LA");
//});

app.MapGet("single", async context => {
    IResponseFormatter formatter = context.RequestServices
        .GetRequiredService<IResponseFormatter>();
    await formatter.Format(context, "Single service");
});

app.MapGet("/", async context => {
    IResponseFormatter formatter = context.RequestServices
        .GetServices<IResponseFormatter>().First(f => f.RichOutput);
    await formatter.Format(context, "Multiple services");
});

app.Run();