using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.entity
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<OrderItem> OrderItems { get; set; } // Add this line to define the relationship

    }
}