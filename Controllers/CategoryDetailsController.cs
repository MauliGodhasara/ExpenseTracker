using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;

namespace ExpenseTracker.Controllers
{
    public class CategoryDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoryDetails
        public async Task<IActionResult> Index()
        {
              return View(await _context.categoryDetails.ToListAsync());
        }

        // GET: CategoryDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Categoryid,Name,Expenseslimet")] CategoryDetail categoryDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDetail);
        }

        // GET: CategoryDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.categoryDetails == null)
            {
                return NotFound();
            }

            var categoryDetail = await _context.categoryDetails.FindAsync(id);
            if (categoryDetail == null)
            {
                return NotFound();
            }
            return View(categoryDetail);
        }

        // POST: CategoryDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Categoryid,Name,Expenseslimet")] CategoryDetail categoryDetail)
        {
            if (id != categoryDetail.Categoryid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryDetailExists(categoryDetail.Categoryid))
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
            return View(categoryDetail);
        }

        // GET: CategoryDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.categoryDetails == null)
            {
                return NotFound();
            }

            var categoryDetail = await _context.categoryDetails
                .FirstOrDefaultAsync(m => m.Categoryid == id);
            if (categoryDetail == null)
            {
                return NotFound();
            }

            return View(categoryDetail);
        }

        // POST: CategoryDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.categoryDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.categoryDetails'  is null.");
            }
            var categoryDetail = await _context.categoryDetails.FindAsync(id);
            if (categoryDetail != null)
            {
                _context.categoryDetails.Remove(categoryDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryDetailExists(int id)
        {
          return _context.categoryDetails.Any(e => e.Categoryid == id);
        }
    }
}
