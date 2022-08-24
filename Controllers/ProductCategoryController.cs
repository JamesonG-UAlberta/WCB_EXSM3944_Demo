using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Demo.Data;
using MVC_Demo.Models;

namespace MVC_Demo.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductCategory
        public async Task<IActionResult> Index()
        {
              return _context.ProductCategories != null ? 
                          View(await _context.ProductCategories.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProductCategories'  is null.");
        }

        // GET: ProductCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductCategories == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(m => m.ProductCategoryID == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: ProductCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductCategoryID,Name,Description")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductCategories == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }

        // POST: ProductCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductCategoryID,Name,Description")] ProductCategory productCategory)
        {
            if (id != productCategory.ProductCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.ProductCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductCategories == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(m => m.ProductCategoryID == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductCategories'  is null.");
            }
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory != null)
            {
                _context.ProductCategories.Remove(productCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(int id)
        {
          return (_context.ProductCategories?.Any(e => e.ProductCategoryID == id)).GetValueOrDefault();
        }
    }
}
