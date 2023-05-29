using Services.Models;

namespace ToyStoreAPI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int? ModelYear { get; set; }
        public decimal? Discount { get; set; }
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int CategoryId { get; set; }
        public  CategoryModel? Category { get; set; }
    }
}
