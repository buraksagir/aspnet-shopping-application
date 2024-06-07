using System;
using System.Collections.Generic;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.webui.ViewModels;


namespace shopapp.webui.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private IUserRepository _userRepository;

        public HomeController(IProductService productService, IUserRepository userRepository)
        {
            _productService = productService;
            _userRepository = userRepository;

        }

        public IActionResult Index()
        {
            int time = DateTime.Now.Hour;
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetHomePageProducts()
            };
            ViewData.Add("product", productViewModel);
            ViewBag.Greeting = time > 12 ? "İyi günler." : " Günaydın.";
            ViewBag.UserName = "Burak";
            return View(productViewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userRepository.Login(request.Username, request.Password);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid username or password.";
                return View(request);
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _userRepository.Register(request.Username, request.Password, request.Name, request.Surname);

            if (result.Success)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", result.ErrorMessage);
            return View(request);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}