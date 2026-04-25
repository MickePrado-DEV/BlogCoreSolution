using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.DataAccess.Data.Repository
{
    public class WorkContainer : IWorkContainer
    {

        private readonly ApplicationDbContext _db;

        public WorkContainer(ApplicationDbContext db)
        {
            _db = db;
            CategoryRepository = new CategoryRepository(_db);
        }
       

        public ICategoryRepository CategoryRepository { get; private set; }
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
