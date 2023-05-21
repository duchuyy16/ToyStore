using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _iProduct;
        public ProductsController(IProduct iProduct)
        {
            _iProduct = iProduct;
        }

        [HttpPost("GetAllProductsBestSellers")]
        public List<ProductModel> GetAllProductsBestSellers()
        {
            var products = _iProduct.GetBestSellers(10);

            List<ProductModel> lstProduct = new List<ProductModel>();

            foreach (var item in products)
            {
                ProductModel productModel = new ProductModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    Discount = item.Discount,
                    Image = item.Image,
                    ModelYear = item.ModelYear,
                    Description = item.Description,
                    Category = item.Category.Adapt<CategoryModel>()
                    //Category = new CategoryModel()
                    //{
                    //    CategoryId = item.Category.CategoryId,
                    //    CategoryName = item.Category.CategoryName
                    //}
                };
                lstProduct.Add(productModel);
            }

            return lstProduct;
        }

        [HttpPost("GetAllProducts")]
        public List<ProductModel> GetAllProducts()
        {
            var products = _iProduct.GetAll();

            List<ProductModel> lstProduct = new List<ProductModel>();

            foreach (var item in products)
            {
                ProductModel productModel = new ProductModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    Discount = item.Discount,
                    Image = item.Image,
                    ModelYear = item.ModelYear,
                    Description = item.Description,
                    Category = item.Category.Adapt<CategoryModel>()
                };
                lstProduct.Add(productModel);
            }

            return lstProduct;
        }

        [HttpPost("GetProductsByCategoryId/{categoryId}")]
        public List<ProductModel> GetProductsByCategoryId(int categoryId)
        {
            var products = _iProduct.GetProductsByCategory(categoryId);

            List<ProductModel> lstProduct = new List<ProductModel>();

            foreach (var item in products)
            {
                ProductModel productModel = new ProductModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    Discount = item.Discount,
                    Image = item.Image,
                    ModelYear = item.ModelYear,
                    Description = item.Description,
                    Category = item.Category.Adapt<CategoryModel>()
                };
                lstProduct.Add(productModel);
            }

            return lstProduct;
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost("SearchByProductName/{keyword}")]
        public List<ProductModel> SearchByProductName(string keyword)
        {
            var products = _iProduct.Search(keyword);

            List<ProductModel> lstProduct = new List<ProductModel>();

            foreach (var item in products)
            {
                ProductModel productModel = new ProductModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    Discount = item.Discount,
                    Image = item.Image,
                    ModelYear = item.ModelYear,
                    Description = item.Description,
                    Category = item.Category.Adapt<CategoryModel>()
                };
                lstProduct.Add(productModel);
            }

            return lstProduct;
        }

        //[Authorize]
        [HttpPost("GetProductById/{id}")]
        public ProductModel GetProductById(int id)
        {
            var product = _iProduct.GetById(id);

            ProductModel productModel = new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Discount = product.Discount,
                Image = product.Image,
                ModelYear = product.ModelYear,
                Description = product.Description,
                Category = product.Category.Adapt<CategoryModel>()
            };

            return productModel;
        }

        [HttpPost("FindProductById/{id}")]
        public ProductModel FindProductById(int id)
        {
            var product = _iProduct.Find(id);

            ProductModel productModel = new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Discount = product.Discount,
                Image = product.Image,
                ModelYear = product.ModelYear,
                Description = product.Description,
                Category = product.Category.Adapt<CategoryModel>()
            };

            return productModel;
        }

        [HttpPost("ExistsById/{id}")]
        public bool ExistsById(int id)
        {
            return _iProduct.IsIdExist(id);
        }

        [HttpPost("AddProduct")]
        public ProductModel AddProduct(ProductModel productModel)
        {
            var newProduct = new Product
            {
                ProductName = productModel.ProductName,
                Price = productModel.Price,
                CategoryId = productModel.CategoryId,
                Discount = productModel.Discount,
                Image = productModel.Image,
                ModelYear = productModel.ModelYear,
                Description = productModel.Description
            };
            var addedProduct = _iProduct.Add(newProduct);

            if (addedProduct == null)
            {
                return new ProductModel();
            }

            return new ProductModel
            {
                ProductName = addedProduct.ProductName,
                Price = addedProduct.Price,
                CategoryId = addedProduct.CategoryId,
                Discount = addedProduct.Discount,
                Image = addedProduct.Image,
                ModelYear = addedProduct.ModelYear,
                Description = addedProduct.Description               
            };
        }

        [HttpPost("UpdateProduct")]
        public bool UpdateProduct(ProductModel productModel)
        {
            var product = new Product
            {
                ProductId = productModel.ProductId,
                ProductName = productModel.ProductName,
                Price = productModel.Price,
                CategoryId = productModel.CategoryId,
                Discount = productModel.Discount,
                Image = productModel.Image,
                ModelYear = productModel.ModelYear,
                Description = productModel.Description
            };
            var updateResult = _iProduct.Update(product);
            return updateResult;
        }

        [HttpPost("DeleteProduct")]
        public bool DeleteProduct(ProductModel productModel)
        {
            var product = _iProduct.GetById(productModel.ProductId);
            if (product == null) return false;
            var deleteResult = _iProduct.Delete(product);
            return deleteResult;
        }
    }
}
