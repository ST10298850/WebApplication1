using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class userController : Controller
    {
        public Table_1 usrtbl = new Table_1();

        [HttpPost]
        public ActionResult About(Table_1 Users)
        {
            var result = usrtbl.insert_User(Users);
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult About()
        {
            return View(usrtbl);
        }
    }
}
