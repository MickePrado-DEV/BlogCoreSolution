using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
      private readonly IWorkContainer _workContainer;

        public CategoriesController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category categoria)
        {
            if (ModelState.IsValid)
            {
                //Logica para guardar en BD
                _workContainer.CategoryRepository.Add(categoria);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category categoria = new Category();
            categoria = _workContainer.CategoryRepository.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category categoria)
        {
            if (ModelState.IsValid)
            {
                //Logica para guardar en BD
                _workContainer.CategoryRepository.Update(categoria);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }


        #region Call Api
        [HttpGet]
        public IActionResult getAll()
        {
            return Json(new { data = _workContainer.CategoryRepository.GetAll() });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _workContainer.CategoryRepository.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando categoría" });
            }

            _workContainer.CategoryRepository.Remove(objFromDb);
            _workContainer.Save();
            return Json(new { success = true, message = "Categoría Borrada Correctamente" });

        }

        #endregion
    }
}
