using Services.Base;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRole : IBaseRepository<Role>
    {
        List<Role> GetAll();
        Role GetById(int id);
        Role Find(int id);
        bool IsIdExist(int id);
    }
}
