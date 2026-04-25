using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogCoreSolution.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DbContext _context;

        internal DbSet<T> _dbSet;


        public Repository(DbContext Context)
        {
            _context = Context;
            this._dbSet = Context.Set<T>();
        }


        public void Add(T entity)
        {
           _dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string? includeProperties = null)
        {

            // se crea una consulta IQueryable a partir del DbSet del contexto
            IQueryable<T> query = _dbSet;
            // aplicar el filtro si se proporciona
            if(filter != null)
            {
                query = query.Where(filter);
            }
           
            //se incluyen propiedades de navegacion si se proporciona

            if(includeProperties != null)
            {
                foreach (var property in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            // se aplica ordenamiento

            if(orderBy != null)
            {
                // se ejecuta la funcion de ordenamiento y se convierte la consulta en una lista
                return orderBy(query).ToList();
            }
            // si no se proporciona ordenamiento, simplemente se convierte la consulta en lista
            return query.ToList();
        }

        public T GetFirsOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            // se crea una consulta IQueryable a partir del DbSet del contexto
            IQueryable<T> query = _dbSet;

            // aplicar el filtro si se proporciona
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //se incluyen propiedades de navegacion si se proporciona

            if (includeProperties != null)
            {
                foreach (var property in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            // se aplica ordenamiento

           
            // si no se proporciona ordenamiento, simplemente se convierte la consulta en lista
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
             _dbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            T entitityToRemove = _dbSet.Find(id);
        }
    }
}
