using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers;

public class AdminController : Controller
{

    private IProductService _productService;

    public AdminController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult ProductList()
    {
        return View(new ProductListViewModel()
        {
            Products = _productService.GetAll()
        });
    }
    
    [HttpGet]
    public IActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductModel model)
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
    public IActionResult Edit(int? id)
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
    public IActionResult Edit(ProductModel model)
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

    public IActionResult DeleteProduct(int productId)
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
}