using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.DataAccess.Data.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _db;
        public ArticleRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public void Update(Article article)
        {
            var objToDb = _db.Articles.FirstOrDefault(s=> s.Id == article.Id);
            objToDb.Name = article.Name;
            objToDb.Description = article.Description;
            objToDb.UrlImage = article.UrlImage;
            objToDb.CategoryId = article.CategoryId;


        }
    }
}
