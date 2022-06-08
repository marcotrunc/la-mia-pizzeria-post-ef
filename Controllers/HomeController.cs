using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*List<Dictionary<string, object>> Menu = new List<Dictionary<string, object>>();

            Menu.Add(new Dictionary<string, object>());
            Menu[0].Add("name", "margherita");
            Menu[0].Add("description", "pizza delle pizze");
            Menu[0].Add("foto", "John");
            Menu[0].Add("price", 5.99);

            Menu[1].Add("name", "margherita");
            Menu[1].Add("description", "pizza delle pizze");
            Menu[1].Add("foto", "John");
            Menu[1].Add("price", 5.99);

            Menu[2].Add("name", "margherita");
            Menu[2].Add("description", "pizza delle pizze");
            Menu[2].Add("foto", "John");
            Menu[2].Add("price", 5.99);

            ViewData["Menu"] = Menu;*/

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}