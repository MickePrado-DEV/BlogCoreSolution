using BlogCore.Models;
using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IWorkContainer _workContainer;
        public HomeController(IWorkContainer workContainer)
        {
            
            _workContainer = workContainer;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                SlidersList = _workContainer.SliderRepository.GetAll(),
                ArticleList = _workContainer.ArticleRepository.GetAll(),

            };
            ViewData["IsHome"] = true;

            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var articleByBd = _workContainer.ArticleRepository.Get(id);
            return View(articleByBd);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
