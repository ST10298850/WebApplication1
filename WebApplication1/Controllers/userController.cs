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

    }
}
