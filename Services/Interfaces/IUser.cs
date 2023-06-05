using Services.Base;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUser : IBaseRepository<AspNetUser>
    {
        List<AspNetUser> GetAll();
        AspNetUser GetById(string id);
        bool IsIdExist(string id);
        int CountUsers();
    }
}
