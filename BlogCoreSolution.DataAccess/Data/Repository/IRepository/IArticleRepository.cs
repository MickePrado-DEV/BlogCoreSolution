using BlogCoreSolution.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.DataAccess.Data.Repository.IRepository
{
    public interface IArticleRepository: IRepository<Article>
    {
        void Update(Article article);

        //metodo para el buscador

        IQueryable<Article> AsQueryable();
    }
}
