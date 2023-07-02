using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Data;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUser _iUsers;
        private readonly ICategory _iCategory;
        private readonly IProduct _iProduct;
        private readonly IOrder _iOrder;
        public HomeController(IUser iUsers, ICategory iCategory, IProduct iProduct, IOrder iOrder)
        {
            _iUsers = iUsers;
            _iCategory = iCategory;
            _iProduct = iProduct;
            _iOrder = iOrder;
        }

        //[HttpPost("Count")]
        //public IActionResult Count()
        //{
        //    int countOrder = _iOrder.CountOrders();
        //    int countProduct = _iProduct.CountProducts();
        //    int countUser = _iUsers.CountUsers();
        //    int countCategory = _iCategory.CountCategories();
        //    return Ok();
        //}

        [HttpPost("Count")]
        public IActionResult Count()
        {
            int countOrder = _iOrder.CountOrders();
            int countProduct = _iProduct.CountProducts();
            int countUser = _iUsers.CountUsers();
            int countCategory = _iCategory.CountCategories();

            var result = new Count
            {
                OrderCount = countOrder,
                ProductCount = countProduct,
                UserCount = countUser,
                CategoryCount = countCategory
            };

            return Ok(result);
        }
    }
}
