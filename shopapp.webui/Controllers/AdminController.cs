using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers;

public class AdminController : Controller
{

    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public AdminController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public IActionResult ProductList()
    {
        return View(new ProductListViewModel()
        {
            Products = _productService.GetAll()
        });
    }
    
    [HttpGet]
    public IActionResult ProductCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ProductCreate(ProductModel model)
    {
        var entity = new Product()
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Url = model.Url,
            ImageUrl = model.ImageUrl,

        };
        _productService.Create(entity);
        var msg = new AlertMessage()
        {
            Message = $"Product named: {entity.Name} successfully created.",
            AlertType = "success"
        };

        TempData["message"] = JsonConvert.SerializeObject(msg);
        return RedirectToAction("ProductList");
    }
    
    [HttpGet]
    public IActionResult ProductEdit(int? id)
    {
        if (id==null)
        {
            return NotFound();
        }

        var entity = _productService.GetById((int)id);

        if (entity== null)
        {
            return NotFound();
        }

        var model = new ProductModel()
        {
            ProductId = entity.ProductID,
            Name = entity.Name,
            Url = entity.Url,
            Price = entity.Price,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl
        };
        
        return View(model);
    }
    
    [HttpPost]
    public IActionResult ProductEdit(ProductModel model)
    {
        var entity = _productService.GetById(model.ProductId);
        if (entity == null)
        {
            return NotFound();
        }

        entity.Name = model.Name;
        entity.Price = model.Price;
        entity.Url = model.Url;
        entity.ImageUrl = model.ImageUrl;
        entity.Description = model.Description;
        
        _productService.Update(entity);
        
        var msg = new AlertMessage()
        {
            Message = $"Product named: {entity.Name} successfully edited.",
            AlertType = "warning"
        };

        TempData["message"] = JsonConvert.SerializeObject(msg);
        
        return RedirectToAction("ProductList");
    }
    
    public IActionResult ProductDelete(int productId)
    {
        var entity = _productService.GetById(productId);
        if (entity != null)
            
        {
            _productService.Delete(entity);
        }
        var msg = new AlertMessage()
        {
            Message = $"Product with Id: {productId} successfully deleted.",
            AlertType = "danger"
        };

        TempData["message"] = JsonConvert.SerializeObject(msg);
        return RedirectToAction("ProductList");
    }

    public IActionResult CategoryList()
    {
        return View(new CategoryListViewModel()
        {
            Categories = _categoryService.GetAll()
        });
    }
    
    [HttpGet]
    public IActionResult CategoryCreate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CategoryCreate(CategoryModel model)
    {
        var entity = new Category()
        {
            Name = model.Name,
            Url = model.Url,
        };
        _categoryService.Create(entity);
        var msg = new AlertMessage()
        {
            Message = $"Category named: {entity.Name} successfully created.",
            AlertType = "success"
        };

        TempData["message"] = JsonConvert.SerializeObject(msg);
        return RedirectToAction("CategoryList");
    }
    
    
    [HttpGet]
    public IActionResult CategoryEdit(int? id)
    {
        if (id==null)
        {
            return NotFound();
        }

        var entity = _categoryService.GetByIdWithProducts((int)id);

        if (entity== null)
        {
            return NotFound();
        }

        var model = new CategoryModel()
        {
            CategoryId = entity.CategoryId,
            Name = entity.Name,
            Url = entity.Url,
            Products = entity.ProductCategories.Select(p => p.Product).ToList()
            
        };
        
        return View(model);
    }
    
    [HttpPost]
    public IActionResult CategoryEdit(CategoryModel model)
    {
        var entity = _categoryService.GetById(model.CategoryId);
        if (entity == null)
        {
            return NotFound();
        }

        entity.CategoryId = model.CategoryId;
        entity.Name = model.Name;
        entity.Url = model.Url;
        
        _categoryService.Update(entity);
        
        var msg = new AlertMessage()
        {
            Message = $"Category named: {entity.Name} successfully edited.",
            AlertType = "warning"
        };

        TempData["message"] = JsonConvert.SerializeObject(msg);
        
        return RedirectToAction("CategoryList");
    }
    
    public IActionResult CategoryDelete(int categoryId)
    {
        var entity = _categoryService.GetById(categoryId);
        if (entity != null)
        {
            _categoryService.Delete(entity);
        }
        var msg = new AlertMessage()
        {
            Message = $"Category with Id: {categoryId} successfully deleted.",
            AlertType = "danger"
        };

        TempData["message"] = JsonConvert.SerializeObject(msg);
        return RedirectToAction("CategoryList");
    }

    [HttpPost]
    public IActionResult DeleteFromCategory(int productId, int categoryId)
    {
        _categoryService.DeleteFromCategory(productId, categoryId);
        return Redirect($"/admin/categories/{categoryId}");
    }
    
    
    [HttpGet]
    public IActionResult AddCategoryToProduct(int? id)
    {
        if (id==null)
        {
            return NotFound();
        }

        var category = _categoryService.GetById((int)id);

        if (category== null)
        {
            return NotFound();
        }

        var model = new ProductCategory()
        {
            CategoryId = category.CategoryId,
            
        };
        
        return View(model);
    }

    [HttpPost]
    public IActionResult AddCategoryToProduct(int productId, int categoryId)
    {
        _categoryService.AddCategoryToProduct(categoryId: categoryId, productId: productId);
        return Redirect($"/admin/categories/{categoryId}");
    }
}