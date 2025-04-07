using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookEcommerce.DataAccess.Repository.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string[] includes = null);
        T Get(Expression<Func<T, bool>> filter, string[] includes = null, bool tracked = false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(T entities);
    }
}
