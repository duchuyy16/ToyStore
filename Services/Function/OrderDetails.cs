using System;
using Services.Interfaces;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Base;
using Microsoft.EntityFrameworkCore;

namespace Services.Function
{
    public class OrderDetails : BaseRepository<OrderDetail>, IOrderDetail
    {
        public OrderDetails(ToyStoreContext context) : base(context)
        {
        }

        public OrderDetail Find(int id)
        {
            var orderDetail = _context.OrderDetails.Find(id);

            if (orderDetail == null)
            {
                throw new Exception("OrderDetail not found");
            }

            return orderDetail;
        }


        public List<OrderDetail> GetAll()
        {
            return _context.OrderDetails.Include(o => o.Order).Include(o => o.Product).ToList();
        }

        public OrderDetail GetById(int id)
        {
            var orderDetail = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product).SingleOrDefault(s => s.OrderDetailId == id);

            if (orderDetail == null)
            {
                throw new Exception("OrderDetail not found");
            }

            return orderDetail;
        }

        public bool IsIdExist(int id)
        {
            return _context.OrderDetails.Any(p => p.OrderDetailId == id);
        }
    }
}
