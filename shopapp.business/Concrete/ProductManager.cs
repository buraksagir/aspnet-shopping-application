using Microsoft.VisualBasic;
using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.data.Concrete;
using shopapp.entity;

namespace shopapp.business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void Create(Product entity)
        {
            _productRepository.Create(entity);
        }
        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product? GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productRepository.GetCountByCategory(category);
        }

        public List<Product> GetHomePageProducts()
        {
            return _productRepository.GetHomePageProducts();
        }

        public Product GetProductDetails(string url)
        {
            return _productRepository.GetProductDetails(url);
        }


        public List<Product> GetProductsByCategory(string name, int pageSize, int page = 1)
        {
            return _productRepository.GetProductsByCategory(name, page, pageSize);

        }

        public List<Product> GetSearchResults(string searchText)
        {
            return _productRepository.GetSearchResults(searchText);
        }


        
    }
}