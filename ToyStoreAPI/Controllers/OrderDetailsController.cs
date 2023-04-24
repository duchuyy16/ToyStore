using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetail _iOrderDetail;
        public OrderDetailsController(IOrderDetail iOrderDetail)
        {
            _iOrderDetail = iOrderDetail;
        }

        [HttpPost("GetAllOrderDetails")]
        public List<OrderDetailModel> GetAllOrderDetails()
        {
            var orderDetails = _iOrderDetail.GetAll();

            List<OrderDetailModel> lstOrderDetail = new List<OrderDetailModel>();

            foreach (var item in orderDetails)
            {
                OrderDetailModel orderDetailModel = new OrderDetailModel
                {
                    OrderDetailId = item.OrderDetailId,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Discount=item.Discount
                };
                lstOrderDetail.Add(orderDetailModel);
            }

            return lstOrderDetail;
        }

        [HttpPost("GetOrderDetailById/{id}")]
        public OrderDetailModel GetOrderDetailById(int id)
        {
            var orderDetail = _iOrderDetail.GetById(id);

            OrderDetailModel orderDetailModel = new OrderDetailModel
            {
                OrderDetailId = orderDetail.OrderDetailId,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                Price = orderDetail.Price,
                Discount = orderDetail.Discount
            };

            return orderDetailModel;
        }

        [HttpPost("FindOrderDetailById/{id}")]
        public OrderDetailModel FindOrderDetailById(int id)
        {
            var orderDetail = _iOrderDetail.Find(id);

            OrderDetailModel orderDetailModel = new OrderDetailModel
            {
                OrderDetailId = orderDetail.OrderDetailId,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                Price = orderDetail.Price,
                Discount = orderDetail.Discount
            };

            return orderDetailModel;
        }

        [HttpPost("ExistsById/{id}")]
        public bool ExistsById(int id)
        {
            return _iOrderDetail.IsIdExist(id);
        }

        [HttpPost("AddOrderDetail")]
        public OrderDetailModel AddOrderDetail(OrderDetailModel orderDetailModel)
        {
            var newOrderDetail = new OrderDetail
            {
                OrderId = orderDetailModel.OrderId,
                ProductId = orderDetailModel.ProductId,
                Quantity = orderDetailModel.Quantity,
                Price = orderDetailModel.Price,
                Discount = orderDetailModel.Discount
            };

            var addedOrderDetail = _iOrderDetail.Add(newOrderDetail);

            if (addedOrderDetail == null) return new OrderDetailModel();

            return new OrderDetailModel
            {
                OrderId = addedOrderDetail.OrderId,
                ProductId = addedOrderDetail.ProductId,
                Quantity = addedOrderDetail.Quantity,
                Price = addedOrderDetail.Price,
                Discount = addedOrderDetail.Discount
            };
        }

        [HttpPost("UpdateOrderDetail")]
        public bool UpdateOrderDetail(OrderDetailModel orderDetailModel)
        {
            var orderDetail = new OrderDetail
            {
                OrderDetailId=orderDetailModel.OrderDetailId,
                OrderId = orderDetailModel.OrderId,
                ProductId = orderDetailModel.ProductId,
                Quantity = orderDetailModel.Quantity,
                Price = orderDetailModel.Price,
                Discount = orderDetailModel.Discount
            };
            var updateResult = _iOrderDetail.Update(orderDetail);
            return updateResult;
        }

        [HttpPost("DeleteOrderDetail")]
        public bool DeleteOrderDetail(OrderDetailModel orderDetailModel)
        {
            var orderDetail = _iOrderDetail.GetById(orderDetailModel.OrderDetailId);
            if (orderDetail == null) return false;
            var deleteResult = _iOrderDetail.Delete(orderDetail);
            return deleteResult;
        }
    }
}
