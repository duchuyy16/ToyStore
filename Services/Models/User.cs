using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ShippingAddress { get; set; } = null!;
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
