using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IProduct _iProduct;
        private readonly ICategory _iCategory;

        public StatisticsController(IProduct iProduct, ICategory iCategory)
        {
            _iProduct = iProduct;
            _iCategory = iCategory;
        }
        [HttpPost("ProductStatistics")]
        public List<StatisticModel> ProductStatistics()
        {
            var categories = _iCategory.GetAll();
            var products = _iProduct.GetAll();
            var statistics = new List<StatisticModel>();
            foreach (var category in categories)
            {
                var categoryId = category.CategoryId;
                var soluong = products.Where(x => new List<int> { x.CategoryId }.Contains(categoryId)).Count();
                statistics.Add(new StatisticModel
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    ProductQuantity = soluong,
                });
            }
            return statistics;
        }
    }
}
