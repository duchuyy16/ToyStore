using Services.Base;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IStatus : IBaseRepository<Status>
    {
        List<Status> GetAll();
        Status GetById(int id);
        Status Find(int id);
        bool IsIdExist(int id);
    }
}
