using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Demo.Data;
using MVC_Demo.Models;

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

        public ActionResult DoTheGeneration(string customerId, string productId, string qty)
        {
            Invoice newInvoice = new Invoice()
            {
                Customerid = int.Parse(customerId)
            };
            _context.Invoices.Add(newInvoice);
            _context.SaveChanges();
            _context.InvoiceProducts.Add(new InvoiceProduct()
            {
                Orderid = newInvoice.Id,
                Inventoryid = int.Parse(productId),
                Quantity = int.Parse(qty)
            });
            _context.SaveChanges();
            return RedirectToAction("GenerateOrder");
        }
    }
}
