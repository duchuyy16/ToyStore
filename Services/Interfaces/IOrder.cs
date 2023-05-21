using Services.Base;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrder : IBaseRepository<Order>
    {
        List<Order> GetAll();
        Order GetById(int id);
        Order Find(int id);
        int FindMaxId();
        bool IsIdExist(int id);
    }
}
