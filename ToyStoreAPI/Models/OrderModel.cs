using Services.Models;

namespace ToyStoreAPI.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public int StatusId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }
        public StatusModel Status { get; set; } = null!;

        //public List<OrderDetailModel> OrderDetails { get; set; } = null!;
    }
}
