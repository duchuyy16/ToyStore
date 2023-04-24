namespace ToyStoreAPI.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ShippingAddress { get; set; } = null!;
        public int? RoleId { get; set; }
        public RoleModel? Role { get; set; }
        public List<OrderModel> Orders { get; set; } = null!;
    }
}
