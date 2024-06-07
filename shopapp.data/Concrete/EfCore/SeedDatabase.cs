using Microsoft.EntityFrameworkCore;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopContext();

            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }
                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(ProductCategories);
                }

                if (context.Users.Count() == 0)
                {
                    context.Users.AddRange(Users);
                }
            }
            context.SaveChanges();
        }
        private static Category[] Categories = {
            new Category(){Name = "Telefon", Url = "telefon"},
            new Category(){Name = "Bilgisayar", Url = "bilgisayar"},
            new Category(){Name = "Elektronik", Url = "elektronik"},
            new Category(){Name = "Beyaz Eşya", Url = "beyaz-esya"},

        };

        private static User[] Users =
        {
            new User() { Username = "burak", Password = "burak123", Name = "Burak Taha", Surname = "Sağır", Id = 0 }
        };
        private static Product[] Products = {
            new Product(){Name = "Samsung Galaxy S23",Price = 23000,Description="128 GB Siyah",ImageUrl="1.jpg",Url="samsung-s23",IsApproved= true,IsHome = true},
            new Product(){Name = "Samsung Galaxy S22",Price = 22000,Description="256 GB Beyaz",ImageUrl="2.jpg",Url="samsung-s22",IsApproved= false,IsHome = true},
            new Product(){Name = "IPhone 15 Pro Max",Price = 30000,Description="256 GB Siyah",ImageUrl="3.jpg",Url="iphone-15-pro-max",IsApproved= true,IsHome = true},
            new Product(){Name = "IPhone 14 Pro",Price = 25000,Description="512 GB Beyaz",ImageUrl="4.jpg",Url="iphone-14-pro",IsApproved= false,IsHome = true},
            new Product(){Name = "Huawei Mate 10 Pro",Price = 10000,Description="256 GB Beyaz",ImageUrl="5.jpg",Url="huawei-mate-10-pro",IsApproved= true,IsHome = true},
            new Product(){Name = "Çamaşır Makinesi",Price = 3000,Description="İkinci el çamaşır makinesi",ImageUrl="8.jpg",Url="camasir-makinesi",IsApproved= true,IsHome = true},
            new Product(){Name = "HP Victus 15",Price = 35000,Description="16 GB RAM, RTX 4050",ImageUrl="9.jpg",Url="hp-victus-15",IsApproved= true, IsHome = true},

        };

        private static ProductCategory[] ProductCategories =
        {
            new ProductCategory(){Product=Products[0],Category=Categories[0]},
            new ProductCategory(){Product=Products[0],Category=Categories[2]},
            new ProductCategory(){Product=Products[1],Category=Categories[0]},
            new ProductCategory(){Product=Products[1],Category=Categories[2]},
            new ProductCategory(){Product=Products[2],Category=Categories[0]},
            new ProductCategory(){Product=Products[2],Category=Categories[2]},
            new ProductCategory(){Product=Products[3],Category=Categories[0]},
            new ProductCategory(){Product=Products[3],Category=Categories[2]},
            new ProductCategory(){Product=Products[4],Category=Categories[0]},
            new ProductCategory(){Product=Products[4],Category=Categories[2]},
            new ProductCategory(){Product=Products[5],Category=Categories[2]},
            new ProductCategory(){Product=Products[5],Category=Categories[3]},
            new ProductCategory(){Product=Products[6],Category=Categories[1]},
            new ProductCategory(){Product=Products[6],Category=Categories[2]},
        };
    }
}