//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            var userID = HttpContext.Session.GetInt32("UserID");
//            ViewData["userID"] = userID;
//            List<productTable> products = productTable.GetAllProducts();
//            ViewData["products"] = products;
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        public IActionResult About()
//        {
//            return View();
//        }

//        public IActionResult MyWork()
//        {
//            List<productTable> products = productTable.GetAllProducts();
//            ViewData["products"] = products;
//            return View();
//        }

//        public IActionResult Account()
//        {
//            return View();
//        }

//        public IActionResult Cart()
//        {
//            return View();
//        }

//        public IActionResult Login()
//        {
//            return View();
//        }

//        public IActionResult SignUp()
//        {
//            return View();
//        }



//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userID = HttpContext.Session.GetInt32("UserID");
            ViewData["userID"] = userID;
            List<productTable> products = productTable.GetAllProducts();
            ViewData["products"] = products;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult MyWork()
        {
            List<productTable> products = productTable.GetAllProducts();
            ViewData["products"] = products;
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        public IActionResult Cart()
        {
            //int? userID = HttpContext.Session.GetInt32("UserID");

            //if (userID == null || userID <= 0)
            //{
            //    return RedirectToAction("Login");
            //}

            //var transactionModel = new TransactionModel();
            //var orders = transactionModel.GetUserTransactions(userID.Value);
            //return View(orders);
            
            int? userID = HttpContext.Session.GetInt32("UserID");

            if (userID == null || userID <= 0)
            {
                return RedirectToAction("Login");
            }

            var transactionModel = new TransactionModel();
            var orders = transactionModel.GetUserTransactions(userID.Value);
            return View(orders); 
    }
        public IActionResult ClearCart()
        {
            var userID = HttpContext.Session.GetInt32("UserID");
            if (userID.HasValue)
            {
                TransactionModel.ClearUserTransactions(userID.Value);
            }

            return RedirectToAction("Cart");
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignOut()
        {
            // Clear the user's session
            HttpContext.Session.Clear();
            // Redirect to the login page or home page after sign-out
            return RedirectToAction("Login", "Home");
        }
        public IActionResult Transaction()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");

            if (userID == null || userID <= 0)
            {
                return View();
            }

            var transactionModel = new TransactionModel();
            var transactions = transactionModel.GetUserTransactions(userID.Value);
            return View(transactions);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

