using Services.Base;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategory : IBaseRepository<Category>
    {
        List<Category> GetAll();
        Category GetById(int id);
        Category Find(int id);
        bool IsIdExist(int id);
    }
}
