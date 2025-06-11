
namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {

        public ViewResult Index()
        {
            Product?[] products = Product.GetProducts();
            #pragma warning disable CS8602
            return View(new string[] { products[0].Name });
        }
    }
}