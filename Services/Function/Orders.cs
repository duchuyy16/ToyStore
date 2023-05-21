using Microsoft.EntityFrameworkCore;
using Services.Base;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Function
{
    public class Orders : BaseRepository<Order>, IOrder
    {
        public Orders(ToyStoreContext context) : base(context)
        {
        }

        public Order Find(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            return order;
        }

        public int FindMaxId()
        {
            return _context.Orders.Max(o => o.OrderId);
        }

        public List<Order> GetAll()
        {
            return _context.Orders.Include(o => o.Status).ToList();
        }

        public Order GetById(int id)
        {
            var order = _context.Orders.Include(o => o.Status).SingleOrDefault(s => s.OrderId == id);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            return order;
        }

        public bool IsIdExist(int id)
        {
            return _context.Orders.Any(p => p.OrderId == id);
        }
    }
}
