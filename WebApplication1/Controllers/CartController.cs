using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
        [HttpPost]
        public IActionResult AddToCart(int productID, int quantity)
        {
            var userID = HttpContext.Session.GetInt32("UserID");

            if (userID == null || userID <= 0)
            {
                return RedirectToAction("Login","Home");
            }

            var cartModel = new CartModel();
            var cartItem = new CartModel
            {
                UserID = userID.Value,
                ProductID = productID,
                Quantity = quantity
            };

            cartModel.AddToCart(cartItem);

            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public IActionResult UpdateCartItem(int cartID, int quantity)
        {
            var cartModel = new CartModel();
            cartModel.UpdateCartItem(cartID, quantity);

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public IActionResult ProcessTransaction()
        {
            var userID = HttpContext.Session.GetInt32("UserID");

            if (userID == null || userID <= 0)
            {
                return RedirectToAction("Login", "Home");
            }

            var cartModel = new CartModel();
            var transactionModel = new TransactionModel();
            var cartItems = cartModel.GetUserCartItems(userID.Value);

            foreach (var item in cartItems)
            {
                var transactionItem = new TransactionModel
                {
                    UserID = userID.Value,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity
                };

                transactionModel.InsertTransaction(transactionItem);
            }

            cartModel.ClearUserCart(userID.Value);

            return RedirectToAction("Transaction", "Home");
        }


        public IActionResult ClearCart()
        {
            var userID = HttpContext.Session.GetInt32("UserID");
            if (userID.HasValue)
            {
                var cartModel = new CartModel();
                cartModel.ClearUserCart(userID.Value);
            }

            return RedirectToAction("Cart");
        }
    }
}
