using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IWorkContainer _workContainer;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public SlidersController(IWorkContainer workContainer, IWebHostEnvironment hostingEnvironment)
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
        

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id != null)
            {
               var _slider = _workContainer.SliderRepository.Get(id.GetValueOrDefault());
                return View(_slider);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {

            if (ModelState.IsValid)
            {
                string mainRoot = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var sliderToBd = _workContainer.SliderRepository.Get(slider.Id);

                if (files.Count > 0)
                {
                    //Nuevo imagen para el slider
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(mainRoot, @"images\sliders");
                    Directory.CreateDirectory(uploads);
                    var extension = Path.GetExtension(files[0].FileName);

                    var rootImage = Path.Combine(mainRoot, sliderToBd.UrlImage.TrimStart('\\', '/'));

                    if (System.IO.File.Exists(rootImage))
                    {
                        System.IO.File.Delete(rootImage);
                    }

                    //Nuevamente subimos el slider
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                   slider.UrlImage = @"\images\sliders\" + fileName + extension;
                }
                else
                {
                    //Aquí sería cuando la imagen ya existe y se conserva
                    slider.UrlImage = sliderToBd.UrlImage;
                }

                _workContainer.SliderRepository.Update(slider);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }

            
            return View();
        }

        #region Call Api
        [HttpGet]
        public IActionResult getAll()
        {
            return Json(new { data = _workContainer.SliderRepository.GetAll() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
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
                if ( archives.Count > 0)
                {
                    //Nuevo slider
                    string fileMain = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(mainPath, @"images\sliders");
                    Directory.CreateDirectory(uploads);
                    var extension = Path.GetExtension(archives[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileMain + extension), FileMode.Create))
                    {
                        archives[0].CopyTo(fileStreams);
                    }

                    slider.UrlImage = @"\images\sliders\" + fileMain + extension;

                    _workContainer.SliderRepository.Add(slider);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }

          
            return View();
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _workContainer.SliderRepository.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando slider" });
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

            _workContainer.SliderRepository.Remove(objFromDb);
            _workContainer.Save();
            return Json(new { success = true, message = "Slider Borrado Correctamente" });

        }
        //}

        #endregion
    }
}
