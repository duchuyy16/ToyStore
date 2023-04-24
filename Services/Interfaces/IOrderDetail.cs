using Services.Base;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderDetail : IBaseRepository<OrderDetail>
    {
        List<OrderDetail> GetAll();
        OrderDetail GetById(int id);
        OrderDetail Find(int id);
        bool IsIdExist(int id);
    }
}
