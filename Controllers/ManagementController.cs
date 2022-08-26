using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Demo.Data;

namespace MVC_Demo.Controllers
{
    public class ManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult GenerateOrder()
        {
            ViewData["Customers"] = _context.Customers.ToList();
            ViewData["Products"] = _context.Inventoryproducts.ToList();


            ViewBag.TestBag = "Here's some data I put in the ViewBag!";
            ViewData["TestData"] = "Here's some data I put in ViewData!";
            return View();
        }
    }
}
