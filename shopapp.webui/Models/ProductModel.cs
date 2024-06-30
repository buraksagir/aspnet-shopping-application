using System.ComponentModel.DataAnnotations;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        
        [Display(Name = "Product Name:", Prompt = "Enter product name")]
        public string Name { get; set; }
        [Display(Name = "Price:", Prompt = "Enter the price of product")]
        public double? Price { get; set; }
        [Display(Name = "Product Description:", Prompt = "Enter product description")]
        public string Description { get; set; }
        [Display(Name = "Product Image URL:", Prompt = "Enter image url:")]
        public string ImageUrl { get; set; }
        [Display(Name = "Product URL:", Prompt = "Enter product url:")]
        public string Url { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}