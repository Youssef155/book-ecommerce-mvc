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
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var oldProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if(oldProduct != null)
            {
                oldProduct.ISBN = product.ISBN;
                oldProduct.ListPrice = product.ListPrice;
                oldProduct.Price = product.Price;
                oldProduct.Price50 = product.Price50;
                oldProduct.Price100 = product.Price100;
                oldProduct.Author = product.Author;
                oldProduct.Description = product.Description;
                oldProduct.CategoryId = product.CategoryId;
                oldProduct.Title = product.Title;
                if (product.ImgUrl != null)
                    oldProduct.ImgUrl = product.ImgUrl;
            }

            _context.SaveChanges();
        }
    }
}
