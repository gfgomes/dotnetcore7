using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebApp.Models;

namespace WebApp.Components
{
    public class CitySummary : ViewComponent
    {
        private CitiesData data;

        public CitySummary(CitiesData cdata)
        {
            data = cdata;
        }

        // Exemplo retornando uma View com um ViewModel (Views\Shared\Components\CitySummary\Default.cshtml)
        //public IViewComponentResult Invoke()
        //{
        //    return View(new CityViewModel
        //    {
        //        Cities = data.Cities.Count(),
        //        Population = data.Cities.Sum(c => c.Population)
        //    });
        //}

        //Exemplo retornando fragmento de html que será codificada para ficar segura e não ser rederizada como html(This is a &lt;h3&gt;&lt;i&gt;string&lt;/i&gt;&lt;/h3&gt;)
        //Além do método Content, o método Invoke pode retornar um string, que será 
        //automaticamente convertido em ContentViewComponentResult.
        //public IViewComponentResult Invoke()
        //{
        //    return Content("This is a <h3><i>string</i></h3>");
        //}

        //Exemplo retornando fragmento de html que não será codificada, ou seja, será renderizada como html (This is a <h3><i>string</i></h3>)
        //Deve ter cuidado ao usar HtmlString para evitar vulnerabilidades XSS.
        //public IViewComponentResult Invoke()
        //{
        //    return new HtmlContentViewComponentResult(
        //        new HtmlString("This is a <h3><i>string</i></h3>"));
        //}

        //public string Invoke()
        //{
        //    if (RouteData.Values["controller"] != null)
        //    {
        //        return "Controller Request";
        //    }
        //    else
        //    {
        //        return "Razor Page Request";
        //    }
        //}
        //public IViewComponentResult Invoke(string themeName)
        //{
        //    ViewBag.Theme = themeName;
        //    return View(new CityViewModel
        //    {
        //        Cities = data.Cities.Count(),
        //        Population = data.Cities.Sum(c => c.Population)
        //    });
        //}

        public IViewComponentResult Invoke(string themeName = "success")
        {
            ViewBag.Theme = themeName;
            return View(new CityViewModel
            {
                Cities = data.Cities.Count(),
                Population = data.Cities.Sum(c => c.Population)
            });
        }

    }
}
