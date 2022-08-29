using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Demo.Data;
using MVC_Demo.Models;
using MVC_Demo.Models.Exceptions;
using Newtonsoft.Json;

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
            // If there are exceptions, store them in the view data/bag so we can inform the user about them.
            if (TempData["Exceptions"] != null)
                ViewData["Exceptions"] = JsonConvert.DeserializeObject(TempData["Exceptions"].ToString(), typeof(BLLValidationException));

            ViewData["Customers"] = new SelectList(_context.Customers, "Id", "Fullname");
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");


            ViewBag.TestBag = "Here's some data I put in the ViewBag!";
            ViewData["TestData"] = "Here's some data I put in ViewData!";
            return View();
        }

        public ActionResult DoTheGeneration(string customerId, string productId, string qty)
        {
            // Let's do some validation!
            BLLValidationException validationState = new BLLValidationException();

            // Do validation, if something fails, add it as a sub exception.

            // TEST EACH ITEM INDIVIDUALLY FIRST.

            // First validation item is typically "does this even exist", before we test it.
            if (string.IsNullOrWhiteSpace(customerId))
                validationState.SubExceptions.Add(new Exception("Customer ID was not provided."));
            else
                // Do any other validation with this item - does it exist in the database, does it have necessary properties/permissions, etc.
                // Remember, if we're dealing with ID's, to test and make sure they are integers before querying the database.
                if (!int.TryParse(customerId, out int temp))
                    validationState.SubExceptions.Add(new Exception("Customer ID was not in the correct format."));
                else
                    // Test to see if this customer exists or not.
                    if (!_context.Customers.Any(x => x.Id == temp))
                        validationState.SubExceptions.Add(new Exception("No customer with the provided Customer ID was found."));

            if (string.IsNullOrWhiteSpace(productId))
                validationState.SubExceptions.Add(new Exception("Product ID was not provided."));
            else
                if (!int.TryParse(productId, out int temp))
                    validationState.SubExceptions.Add(new Exception("Product ID was not in the correct format."));
                else
                    if (!_context.Products.Any(x => x.Id == temp))
                        validationState.SubExceptions.Add(new Exception("No product with the provided Product ID was found."));

            if (string.IsNullOrWhiteSpace(qty))
                validationState.SubExceptions.Add(new Exception("Quantity was not provided."));
            else
                if (!int.TryParse(qty, out int temp))
                    validationState.SubExceptions.Add(new Exception("Quantity was not in the correct format."));
                else
                    if (temp <= 0)
                        validationState.SubExceptions.Add(new Exception("Quantity must be greater than zero."));

            // ONCE EACH ITEM IS TESTED, THEN DO ANY TESTS THAT RELY ON MULTIPLE INPUTS.


            if (validationState.Any)
            {
                TempData["Exceptions"] = JsonConvert.SerializeObject(validationState);
            }
            // If we go in here, we've not encountered any validation issues, so we are good to proceed.
            else
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
            }
            return RedirectToAction("GenerateOrder");
        }
    }
}
