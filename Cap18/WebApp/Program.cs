using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			_ = builder.Services.AddDbContext<DataContext>(opts =>
			{
				_ = opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
				_ = opts.EnableSensitiveDataLogging(true);
			});

			var app = builder.Build();

			_ = app.UseMiddleware<TestMiddleware>();

			_ = app.MapGet("/", () => "Hello World!");

			// Cria uma scope e obtem o serviço necessário para popular o banco de dados  
			// (essa abordagem é necessária porque o DataContext é registrado como scoped por padrão)  
			// como scoped, ele é criado uma vez por requisição, e não uma vez por aplicação  
			// essa abordagem faz o papel que um request faria, mas sem a necessidade de um request  
			using (var scope = app.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<DataContext>();
				SeedData.SeedDatabase(context);
			}

			await app.RunAsync().ConfigureAwait(false);
		}
	}
}
