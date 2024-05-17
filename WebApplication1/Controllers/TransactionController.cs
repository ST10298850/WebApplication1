using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TransactionController : Controller
    {
        [HttpPost]
        public ActionResult PlaceOrder( int productID, int quantity)
        {
            //var userID = HttpContext.Session.GetInt32("userID"); - from HomeController
            var userID = HttpContext.Session.GetInt32("userID");
            if (userID > 0)
            {
                return RedirectToAction("Login", "Login");
            }

            var transaction = new TransactionModel();
            var result = transaction.InsertTransaction(userID, productID, quantity);

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
    }
}
