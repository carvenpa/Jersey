using Jersey.Areas.Customer.Controllers;
using Jersey.DataAccess.Repository.IRepository;
using Jersey.Models;
using Jersey.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security.Claims;


namespace Jersey.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IStringLocalizer<HomeController> localizer,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _localizer=localizer;
            _sharedLocalizer=sharedLocalizer;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");            
            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Details(int productId)
        {
            //Product product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties:"Category");                
            //return View(product);
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1, //basic number as 1 for something put into shopping cart
                ProductId = productId
            };
            return View(cart); //return the cart view
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Details(ShoppingCart shoppingCart) 
        {
            var claimsIdentity =(ClaimsIdentity)User.Identity;
            var userId  = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value; 
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUser.Id == userId && u.ProductId ==
                shoppingCart.ProductId && u.Edition == shoppingCart.Edition &&
                u.Size == shoppingCart.Size && u.Printing == shoppingCart.Printing);

            if (cartFromDb != null)
            {
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else 
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }                         
           
            TempData["success"] = "Addition of product into Shopping Cart is successful!!!";
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        //For multilingual
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl ?? "/");
        }
    }
}
