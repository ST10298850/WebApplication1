//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    public class TransactionController : Controller
//    {
//        [HttpPost]
//        public ActionResult PlaceOrder(int userID, int productID, int quantity)
//        {
//            if (userID <= 0)
//            {
//                return RedirectToAction("Login", "Login");
//            }

//            var transaction = new TransactionModel
//            {
//                UserID = userID,
//                ProductID = productID,
//                Quantity = quantity
//            };

//            var model = new TransactionModel();
//            var result = model.InsertTransaction(transaction);

//            if (result > 0)
//            {
//                return RedirectToAction("Index", "Home");
//            }
//            else
//            {
//                // Handle insertion failure
//                return View("Error");
//            }
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TransactionController : Controller
    {
        [HttpPost]
        public ActionResult PlaceOrder(int productID, int quantity)
        {
            int? userID = HttpContext.Session.GetInt32("UserID");

            if (userID == null || userID <= 0)
            {
                return RedirectToAction("Login", "Home");
            }

            var transaction = new TransactionModel
            {
                UserID = userID.Value,
                ProductID = productID,
                Quantity = quantity
            };

            var model = new TransactionModel();
            var result = model.InsertTransaction(transaction);

            if (result > 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Handle insertion failure
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult GetUserOrders()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");

            if (userID == null || userID <= 0)
            {
                return RedirectToAction("Login", "Home");
            }

            var model = new TransactionModel();
            List<TransactionModel> orders = model.GetUserTransactions(userID.Value);

            return View(orders);
        }
    }
}
