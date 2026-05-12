using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IWorkContainer _workContainer;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticlesController(IWorkContainer workContainer, IWebHostEnvironment hostingEnvironment)
        {
            _workContainer = workContainer;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ArticleVM articleVM = new ArticleVM()
            {
                Article = new Article(),
                ListOfCategories = _workContainer.CategoryRepository.GetListOfCategories()
            };

            return View(articleVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticleVM artiVM = new ArticleVM()
            {
                Article = new Article(),
                ListOfCategories = _workContainer.CategoryRepository.GetListOfCategories()
            };

            if (id != null)
            {
                artiVM.Article = _workContainer.ArticleRepository.Get(id.GetValueOrDefault());
            }

            return View(artiVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleVM artiVM)
        {

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesdeBd = _workContainer.ArticleRepository.Get(artiVM.Article.Id);

                if (archivos.Count > 0)
                {
                    //Nuevo imagen para el artículo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"images\articles");
                    Directory.CreateDirectory(subidas);
                    var extension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeBd.UrlImage.TrimStart('\\', '/'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.Article.UrlImage = @"\images\articles\" + nombreArchivo + extension;
                }
                else
                {
                    //Aquí sería cuando la imagen ya existe y se conserva
                    artiVM.Article.UrlImage = articuloDesdeBd.UrlImage;
                }

                _workContainer.ArticleRepository.Update(artiVM.Article);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }

            artiVM.ListOfCategories = _workContainer.CategoryRepository.GetListOfCategories();
            return View(artiVM);
        }

        #region Call Api
        [HttpGet]
        public IActionResult getAll()
        {
            return Json(new { data = _workContainer.ArticleRepository.GetAll(includeProperties: "Category") });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleVM artiVM)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                System.Diagnostics.Debug.WriteLine("ERROR LIST =>",error.ErrorMessage);
            }
            if (ModelState.IsValid)
            {
                string mainPath = _hostingEnvironment.WebRootPath;
                var archives = HttpContext.Request.Form.Files;
                if (artiVM.Article.Id == 0 && archives.Count > 0)
                {
                    //Nuevo articulo
                    string fileMain = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(mainPath, @"images\articles");
                    Directory.CreateDirectory(uploads);
                    var extension = Path.GetExtension(archives[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileMain + extension), FileMode.Create))
                    {
                        archives[0].CopyTo(fileStreams);
                    }

                    artiVM.Article.UrlImage = @"\images\articles\" + fileMain + extension;

                    _workContainer.ArticleRepository.Add(artiVM.Article);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }

            artiVM.ListOfCategories = _workContainer.CategoryRepository.GetListOfCategories();
            return View(artiVM);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _workContainer.ArticleRepository.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando artículo" });
            }

            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(objFromDb.UrlImage))
            {
                var rutaImagen = Path.Combine(rutaDirectorioPrincipal, objFromDb.UrlImage.TrimStart('\\', '/'));
                if (System.IO.File.Exists(rutaImagen))
                {
                    System.IO.File.Delete(rutaImagen);
                }
            }

            _workContainer.ArticleRepository.Remove(objFromDb);
            _workContainer.Save();
            return Json(new { success = true, message = "Artículo Borrado Correctamente" });

        }
        //}

        #endregion
    }
}
