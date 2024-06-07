
using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface IProductService
    {
        Product? GetById(int id);
        List<Product> GetAll();
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        Product GetProductDetails(string url);
        List<Product> GetProductsByCategory(string name, int pageSize, int page = 1);
        int GetCountByCategory(string category);
        List<Product> GetHomePageProducts();
        List<Product> GetSearchResults(string searchText);

    }
}