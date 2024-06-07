using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using shopapp.business.Abstract;
using shopapp.business.Concrete;
using shopapp.data.Abstract;
using shopapp.data.Concrete;
using shopapp.data.Concrete.EfCore;
using shopapp.entity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddScoped<IProductRepository, EfCoreProductRepository>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IUserRepository, EfCoreUserRepository>();
// builder.Services.AddIdentity<User, IdentityRole>(options =>
// {
//    //identity ayarları
// })
//     .AddEntityFrameworkStores<ShopContext>()
//     .AddDefaultTokenProviders();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    SeedDatabase.Seed();
    app.UseDeveloperExceptionPage();
    app.UseStaticFiles();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
        RequestPath = "/modules"
    }
    );
}
// app.UseAuthentication();
// app.UseAuthorization();
app.MapControllerRoute(name: "search", pattern: "search", defaults: new { controller = "Shop", action = "search" });
app.MapControllerRoute(name: "productdetails", pattern: "{url}", defaults: new { controller = "Shop", action = "details" });
app.MapControllerRoute(name: "products", pattern: "products/{category?}", defaults: new { controller = "Shop", action = "list" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();


