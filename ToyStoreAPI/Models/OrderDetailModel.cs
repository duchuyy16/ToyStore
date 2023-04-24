using Services.Models;

namespace ToyStoreAPI.Models
{
    public class OrderDetailModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
