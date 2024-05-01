using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class userController : Controller
    {
        public UserTable usrtbl = new UserTable();
        public LoginModel lm = new LoginModel();

        [HttpPost]
        public ActionResult SignUp(UserTable Users)
        {
            //Connected to about Page 
            var result = usrtbl.insert_User(Users);
           return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginModel l)
        {

            int userId = lm.SelectUser(l);
            if (userId != -1)
            {
                // Redirect After User Found
                TempData["userID"] = userId;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // User not found
                return Content("User Not Found", "text / html");
            }
        }
    }
}
