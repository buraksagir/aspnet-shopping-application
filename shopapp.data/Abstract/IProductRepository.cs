using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductDetails(string url);
        List<Product> GetHomePageProducts();
        List<Product> GetProductsByCategory(string name, int pageSize, int page = 1);
        int GetCountByCategory(string category);
        List<Product> GetSearchResults(string searchText);


    }
}