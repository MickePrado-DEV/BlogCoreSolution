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
            var sliders = _workContainer.SliderRepository.GetAll();
            var activeSliders = sliders.Where(s => s.Status).ToList();
            HomeVM homeVM = new HomeVM()
            {
                SlidersList = activeSliders,
                ArticleList = _workContainer.ArticleRepository.GetAll(),

            };
            ViewBag.IsHome = true;

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
