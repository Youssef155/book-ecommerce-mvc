using BookEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEcommerce.DataAccess.Repository.Interfaces
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        void Update(Company company);
    }
}
