using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _iOrder;
        public OrdersController(IOrder iOrder)
        {
            _iOrder = iOrder;
        }

        [HttpPost("GetAllOrders")]
        public List<OrderModel> GetAllOrders()
        {
            var orders = _iOrder.GetAll();

            List<OrderModel> lstOrder = new List<OrderModel>();

            foreach (var item in orders)
            {
                OrderModel orderModel = new OrderModel
                {   
                    OrderId=item.OrderId,
                    UserId=item.UserId,
                    OrderDate=item.OrderDate,
                    EstimatedDeliveryDate=item.EstimatedDeliveryDate,
                    StatusId=item.StatusId
                };
                lstOrder.Add(orderModel);
            }

            return lstOrder;
        }

        [HttpPost("GetOrderById/{id}")]
        public OrderModel GetOrderById(int id)
        {
            var order = _iOrder.GetById(id);

            OrderModel categoryModel = new OrderModel
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                StatusId = order.StatusId
            };

            return categoryModel;
        }

        [HttpPost("FindOrderById/{id}")]
        public OrderModel FindOrderById(int id)
        {
            var order = _iOrder.Find(id);

            OrderModel categoryModel = new OrderModel
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                StatusId = order.StatusId
            };

            return categoryModel;
        }

        [HttpPost("ExistsById/{id}")]
        public bool ExistsById(int id)
        {
            return _iOrder.IsIdExist(id);
        }

        [HttpPost("AddOrder")]
        public OrderModel AddOrder(OrderModel orderModel)
        {
            var newOrder = new Order
            {
                UserId = orderModel.UserId,
                OrderDate = orderModel.OrderDate,
                EstimatedDeliveryDate = orderModel.EstimatedDeliveryDate,
                StatusId = orderModel.StatusId
            };

            var addedOrder = _iOrder.Add(newOrder);

            if (addedOrder == null) return new OrderModel();

            return new OrderModel {
                UserId = addedOrder.UserId,
                OrderDate = addedOrder.OrderDate,
                EstimatedDeliveryDate = addedOrder.EstimatedDeliveryDate,
                StatusId = addedOrder.StatusId
            };
        }

        [HttpPost("UpdateOrder")]
        public bool UpdateOrder(OrderModel orderModel)
        {
            var order = new Order
            {
                OrderId=orderModel.OrderId,    
                UserId = orderModel.UserId,
                OrderDate = orderModel.OrderDate,
                EstimatedDeliveryDate = orderModel.EstimatedDeliveryDate,
                StatusId = orderModel.StatusId
            };
            var updateResult = _iOrder.Update(order);
            return updateResult;
        }

        [HttpPost("DeleteOrder")]
        public bool DeleteOrder(OrderModel orderModel)
        {
            var order = _iOrder.GetById(orderModel.OrderId);
            if (order == null) return false;
            var deleteResult = _iOrder.Delete(order);
            return deleteResult;
        }
    }
}
