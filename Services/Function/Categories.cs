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
    public class Categories : BaseRepository<Category>, ICategory
    {
        public Categories(ToyStoreContext context) : base(context)
        {
        }

        public Category Find(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                throw new Exception("Category not found");
            }

            return category;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
            
        }

        public Category GetById(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                throw new Exception("Category not found");
            }

            return category;           
        }

        public bool IsIdExist(int id)
        {
            return _context.Categories.Any(c => c.CategoryId == id);
        }
    }
}
