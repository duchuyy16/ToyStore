using Microsoft.EntityFrameworkCore;
using Services.Base;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Function
{
    public class Products : BaseRepository<Product>, IProduct
    {
        public Products(ToyStoreContext context) : base(context)
        {
        }

        public List<Product> GetBestSellers(int count)
        {
            return _context.Products
                    .OrderByDescending(p => p.OrderDetails.Sum(od => od.Quantity))
                    .Take(count)
                    .ToList();
        }

        public List<Product> Search(string name)
        {
            return _context.Products.Where(m => m.ProductName.Contains(name)).Include(p => p.Category).ToList();
        }

        public Product Find(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return product;
        }

        public List<Product> GetAll()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public Product GetById(int id)
        {
            var product = _context.Products.Include(p => p.Category).SingleOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return product;
        }

        public bool IsIdExist(int id)
        {
            return _context.Products.Any(p => p.ProductId == id);
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(c => c.CategoryId == categoryId).Include(p => p.Category).ToList();
        }
    }
}
