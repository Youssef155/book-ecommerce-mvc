﻿using BookEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEcommerce.DataAccess.Repository.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        void Update(Product product);
    }
}
