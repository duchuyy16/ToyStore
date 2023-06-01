using Services.Base;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProduct : IBaseRepository<Product>
    {
        List<Product> GetBestSellers(int count);
        List<Product> GetAll();
        List<Product> GetProductsByCategory(int categoryId);
        Product GetById(int id);
        List<Product> Search(string keyword);
        Product Find(int id);
        bool IsIdExist(int id);
        int CountProducts();
    }
}
