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
            if (ModelState.IsValid)
            {
                try
                {
                    var result = prdtbl.insert_product(products);
                    return RedirectToAction("MyWork", "Home");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    // For now, let's just return the view with the model
                    ModelState.AddModelError("", "Error occurred while adding product: " + ex.Message);
                    return View(products);
                }
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult MyWork()
        {
            List<productTable> products = productTable.GetAllProducts();
            ViewData["products"] = products;
            return View(products);
        }
    }
}

