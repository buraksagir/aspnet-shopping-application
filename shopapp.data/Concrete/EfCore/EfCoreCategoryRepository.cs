using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category, ShopContext>, ICategoryRepository
    {
        public Category GetByIdWithProducts(int categoryId)
        {
            using (var context = new ShopContext())
            {
                return context.Categories
                    .Where(i=> i.CategoryId == categoryId)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i=> i.Product)
                    .FirstOrDefault();
            }
        }

        public void DeleteFromCategory(int productId, int categoryId)
        {
            using (var context = new ShopContext())
            {
                var cmd = "delete from productcategory where ProductId=@p0 and CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd, productId, categoryId);
            }
        }

        public void AddCategoryToProduct(int productId, int categoryId)
        {
            using (var context = new ShopContext())
            {
                var cmd = "insert into productcategory (ProductId, CategoryId) values (@p0,@p1)";
                context.Database.ExecuteSqlRaw(cmd, productId, categoryId);
            }
        }
    }
}