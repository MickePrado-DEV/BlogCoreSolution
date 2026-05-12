using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogCore.Areas.Admin.Controllers
{
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
            ClaimsIdentity? claimsIdentity = (ClaimsIdentity)User.Identity;
            Claim? currentUser = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier);

            return View(_workContainer.UserRepository.GetAll(currentUser!.Value));
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
