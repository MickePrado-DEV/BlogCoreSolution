using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.DataAccess.Data.Repository
{
    public class WorkContainer : IWorkContainer
    {

        private readonly ApplicationDbContext _db;

        public WorkContainer(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            CategoryRepository = new CategoryRepository(_db);
            ArticleRepository = new ArticleRepository(_db);
            SliderRepository = new SliderRepository(_db);
            UserRepository = new UserRepository(_db, userManager);
        }
       

        public ICategoryRepository CategoryRepository { get; private set; }
        public IArticleRepository ArticleRepository { get; private set; }
        public ISliderRepository SliderRepository { get; private set; }
      
        public IUserRepository UserRepository { get; private set; }
        public void Dispose()
        {
           _db.Dispose();

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
