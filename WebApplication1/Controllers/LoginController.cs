using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginModel login;

        public LoginController()
        {
            login = new LoginModel();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var loginModel = new LoginModel();
            int userID = loginModel.SelectUser(username, password);
            if (userID != -1)
            {
                // Store userID in session
                HttpContext.Session.SetInt32("UserID", userID);

                // User found, proceed with login logic
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // User not found, handle accordingly
                TempData["ErrorMessage"] = "Invalid username or password. Please try again.";
                return RedirectToAction("Login", "Home");
            }
        }
    }
}
