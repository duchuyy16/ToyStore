using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
