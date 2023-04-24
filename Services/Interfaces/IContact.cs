using Services.Base;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IContact : IBaseRepository<Contact>
    {
        List<Contact> GetAll();
        Contact GetById(int id);
        Contact Find(int id);
        bool IsIdExist(int id);
    }
}
