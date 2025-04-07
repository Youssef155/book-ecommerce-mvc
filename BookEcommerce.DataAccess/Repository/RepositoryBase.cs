using BookEcommerce.DataAccess.Data;
using BookEcommerce.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookEcommerce.DataAccess.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string[] includes = null, bool tracked = false)
        {
            IQueryable<T> query;

            if (tracked)
                query = dbSet;
            else
                query = dbSet.AsNoTracking();

            query = query.Where(filter);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            IQueryable<T> query = dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(T entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
