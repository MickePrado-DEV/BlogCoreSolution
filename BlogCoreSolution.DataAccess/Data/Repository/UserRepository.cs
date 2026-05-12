using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogCoreSolution.DataAccess.Data.Repository
{
    public class UserRepository :  IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        public UserRepository( 
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager
            ) 
        {
            _userManager = userManager;
            _db = db;
        }

        public void BlockUser(string userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
                _db.SaveChanges();
            }
        }

        public IEnumerable<ApplicationUser> GetAll(string currentUser)
        {
            return [.. _db.Users.Where(u => u.Id != currentUser)];
        }

        public ApplicationUser getUser(string userId)
        { 
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            return user!;
        }

        public void UnBlockUser(string userId)
        {

            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now;
                _db.SaveChanges();
            }
        }
    }
}
