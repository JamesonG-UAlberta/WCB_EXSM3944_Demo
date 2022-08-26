using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewData["Customers"] = new SelectList(_context.Customers, "Id", "Fullname");
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");


            ViewBag.TestBag = "Here's some data I put in the ViewBag!";
            ViewData["TestData"] = "Here's some data I put in ViewData!";
            return View();
        }
    }
}
