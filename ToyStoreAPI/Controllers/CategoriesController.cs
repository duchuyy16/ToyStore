using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory _iCategory;
        public CategoriesController(ICategory iCategory)
        {
            _iCategory = iCategory;
        }

        [HttpPost("GetAllCategories")]
        public List<CategoryModel> GetAllCategories()
        {
            var categories = _iCategory.GetAll();

            List<CategoryModel> lstCategory = new List<CategoryModel>();

            foreach (var item in categories)
            {
                CategoryModel categoryModel = new CategoryModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                };
                lstCategory.Add(categoryModel);
            }

            return lstCategory;
        }

        [HttpPost("GetCategoryById/{id}")]
        public CategoryModel GetCategoryById(int id)
        {
            var category = _iCategory.GetById(id);

            CategoryModel categoryModel = new CategoryModel
            {
                CategoryId=category.CategoryId,
                CategoryName=category.CategoryName,
            };

            return categoryModel;
        }

        [HttpPost("FindCategoryById/{id}")]
        public CategoryModel FindCategoryById(int id)
        {
            var category = _iCategory.Find(id);

            CategoryModel categoryModel = new CategoryModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };

            return categoryModel;
        }

        [HttpPost("ExistsById/{id}")]
        public bool ExistsById(int id)
        {
            return _iCategory.IsIdExist(id);
        }

        [HttpPost("AddCategory")]
        public CategoryModel AddCategory(CategoryModel categoryModel)
        {
            var newCategory = new Category
            {
                CategoryName = categoryModel.CategoryName
            };

            var addedCategory = _iCategory.Add(newCategory);

            if (addedCategory == null) return new CategoryModel();
   
            return new CategoryModel { CategoryName= addedCategory.CategoryName };
        }

        [HttpPost("UpdateCategory")]
        public bool UpdateCategory(CategoryModel categoryModel)
        {
            var category = new Category
            {
                CategoryId= categoryModel.CategoryId,
                CategoryName = categoryModel.CategoryName
            };
            var updateResult = _iCategory.Update(category);
            return updateResult;
        }

        [HttpPost("DeleteCategory")]
        public bool DeleteCategory(CategoryModel categoryModel)
        {
            var category = _iCategory.GetById(categoryModel.CategoryId);
            if (category == null) return false;
            var deleteResult = _iCategory.Delete(category);
            return deleteResult;
        }
    }
}
