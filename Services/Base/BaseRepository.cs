using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ToyStoreContext _context;
        public BaseRepository(ToyStoreContext context)
        {
            _context = context;
        }
        public T? Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Update(T entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

    }
}
