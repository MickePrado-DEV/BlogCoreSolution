using BlogCore.Models;
using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
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

        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            var articles = _workContainer.ArticleRepository.AsQueryable();
            var paginatedEntries = articles.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var sliders = _workContainer.SliderRepository.GetAll();
            var activeSliders = sliders.Where(s => s.Status).ToList();
            HomeVM homeVM = new HomeVM()
            {
                SlidersList = activeSliders,
                ArticleList = paginatedEntries,
                PageIndex = page,
                TotalPages = (int)Math.Ceiling(articles.Count() / (double)pageSize)
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

        [HttpGet]
        public IActionResult SearchResult(string searchString, int page = 1, int pageSize = 3)
        {   
            var articles = _workContainer.ArticleRepository.AsQueryable();

            //filtrar por titulo si hay un termino de busqueda

            if (!string.IsNullOrEmpty(searchString)) 
            { 
                articles = articles.Where(e => e.Name.Contains(searchString));
            }

            var paginatedEntries = articles.Skip((page - 1) * pageSize).Take(pageSize);

            //crear el modelo para la vista
            var model = new PaginatedList<Article>(paginatedEntries.ToList(), articles.Count(), page, pageSize, searchString);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
