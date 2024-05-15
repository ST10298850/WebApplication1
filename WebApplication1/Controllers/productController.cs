using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class productController : Controller
    {
        public productTable prdtbl = new productTable();
            
        [HttpPost]
        public ActionResult AddProduct(productTable products)
        {
            var result = prdtbl.insert_product(products);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult MyWork()
        {
            return View(prdtbl);
        }
    }
}

