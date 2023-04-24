using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    public interface IBaseRepository<T> where T : class
    {
        T? Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
