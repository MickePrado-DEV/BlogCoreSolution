using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogCore.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        [Area("Client")]
        public IActionResult Index()
        {
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
