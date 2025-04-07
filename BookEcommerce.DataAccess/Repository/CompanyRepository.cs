using BookEcommerce.DataAccess.Data;
using BookEcommerce.DataAccess.Repository.Interfaces;
using BookEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEcommerce.DataAccess.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public void Update(Company company)
        {
            _context.Update(company);
        }
    }
}
