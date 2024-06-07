using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.data.Concrete.EfCore;
using shopapp.entity;

namespace shopapp.data.Concrete
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product, ShopContext>, IProductRepository
    {
        public List<Product> GetPopularProducts()
        {
            using (var context = new ShopContext())
            {
                return context.Products.ToList();
            }
        }

        public List<Product> GetProductsByCategory(string name, int pageSize, int page = 1)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products
                .Where(i => i.IsApproved)
                .AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    products = products
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.ProductCategories.Any(a => a.Category.Url == name.ToLower()));
                }
                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }


        public Product GetProductDetails(string url)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                .Where(i => i.Url == url)
                .Include(i => i.ProductCategories)
                .ThenInclude(i => i.Category)
                .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products
                .Where(i => i.IsApproved)
                .AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.ProductCategories.Any(a => a.Category.Url == category.ToLower()));
                }
                return products.Count();
            }
        }

        public List<Product> GetHomePageProducts()
        {
            using (var context = new ShopContext())
            {
                return context.Products.Where(i => i.IsApproved && i.IsHome).ToList();
            }
        }

        public List<Product> GetSearchResults(string searchText)
        {
            using (var context = new ShopContext())
            {
                var products = context
                .Products
                .Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchText.ToLower()) || i.Description.ToLower().Contains(searchText.ToLower())))
                .AsQueryable();

                return products.ToList();
            }
        }

    }
}