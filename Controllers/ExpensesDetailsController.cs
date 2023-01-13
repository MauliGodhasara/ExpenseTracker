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
    public class ExpensesDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExpensesDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.expensesDetails.Include(e => e.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExpensesDetails/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            ViewData["Categoryid"] = new SelectList(_context.categoryDetails, "Categoryid", "Name");
            if (id == 0)
                return View(new ExpensesDetail());
            else
                return View(_context.expensesDetails.Find(id));
        }
        // POST: ExpensesDetails/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Expensesid,Categoryid,Title,Description,Amount,Date")] ExpensesDetail expensesDetail)
        {
            if (ModelState.IsValid)
            {
                if(expensesDetail.Expensesid == 0)
                    _context.Add(expensesDetail);
                else
                    _context.Update(expensesDetail);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categoryid"] = new SelectList(_context.categoryDetails, "Categoryid", "Categoryid", expensesDetail.Categoryid);
            return View(expensesDetail);
        }
        // GET: CategoryDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.expensesDetails == null)
            {
                return NotFound();
            }

            var expensesDetails = await _context.expensesDetails
                .FirstOrDefaultAsync(m => m.Expensesid == id);
            if (expensesDetails == null)
            {
                return NotFound();
            }

            return View(expensesDetails);
        }
        // POST: ExpensesDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.expensesDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.expensesDetails'  is null.");
            }
            var expensesDetail = await _context.expensesDetails.FindAsync(id);
            if (expensesDetail != null)
            {
                _context.expensesDetails.Remove(expensesDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        

    }
}
