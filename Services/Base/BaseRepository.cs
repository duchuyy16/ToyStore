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
            try
            {
                var ent = _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return ent.Entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                _context.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
