using WebApp.Models;

namespace WebApp
{
	public class TestMiddleware
	{
		private RequestDelegate nextDelegate;

		public TestMiddleware(RequestDelegate next)
		{
			nextDelegate = next ?? throw new ArgumentNullException(nameof(next));
		}

		public async Task Invoke(HttpContext context, DataContext dataContext)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (dataContext == null)
			{
				throw new ArgumentNullException(nameof(dataContext));
			}

			if (context.Request.Path == "/test")
			{
				await context.Response.WriteAsync($"There are "
					+ dataContext.Products.Count() + " products\n").ConfigureAwait(false);

				await context.Response.WriteAsync("There are "
					+ dataContext.Categories.Count() + " categories\n").ConfigureAwait(false);

				await context.Response.WriteAsync($"There are "
					+ dataContext.Suppliers.Count() + " suppliers\n").ConfigureAwait(false);
			}
			else
			{
				await nextDelegate(context).ConfigureAwait(false);
			}
		}
	}
}
