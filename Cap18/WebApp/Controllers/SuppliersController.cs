using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private DataContext context;

        public SuppliersController(DataContext ctx)
        {
            context = ctx;
        }

        [HttpGet("{id}")]
        public async Task<Supplier?> GetSupplier(long id)
        {
            // Include para carregar os produtos do fornecedor
            Supplier supplier = await context.Suppliers
                .Include(s => s.Products)
                .FirstAsync(s => s.SupplierId == id);

            // Quebrando referências circulares em dados relacionados de modo bruto, quando não se usa  opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            // na configuração do serializador JSON em Program.cs
            if (supplier.Products != null)
            {
                foreach (Product p in supplier.Products)
                {
                    p.Supplier = null;
                }
            }
            return supplier;
        }
    }
}