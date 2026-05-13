using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
   
    public class UsersController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public UsersController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
            {
                return Challenge();
            }

            return View(_workContainer.UserRepository.GetAll(currentUserId));
        }
        [HttpGet]
        public IActionResult Block(string id)
        {
            if(id == null)
            {
                return NotFound();

            }

            _workContainer.UserRepository.BlockUser(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult UnBlock(string id)
        {
            if(id == null)
            {
                return NotFound();

            }

            _workContainer.UserRepository.UnBlockUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
