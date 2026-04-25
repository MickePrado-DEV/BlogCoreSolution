using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var objToDb = _db.Categories.FirstOrDefault(c=> c.Id == category.Id);


            objToDb.Name = category.Name;
            objToDb.Order = category.Order;


        }
    }
}
