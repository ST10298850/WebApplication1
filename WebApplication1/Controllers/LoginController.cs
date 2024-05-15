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
                //HttpContext.Session.SetInt32("UserID", userID);

                // User found, proceed with login logic (e.g., set authentication cookie)
                // For demonstration, redirecting to a dummy page
                return RedirectToAction("Index", "Home", new { userID = userID });
            }
            else
            {
                // User not found, handle accordingly (e.g., show error message)
                return View("LoginFailed");
            }
        }
    }
}
