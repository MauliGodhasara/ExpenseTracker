using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ExpenseTracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ActionResult> Index()
        {
            //List Of Expenes
            List<ExpensesDetail> SelectedExpenses = await _context.expensesDetails
                .Include(x => x.Category).ToListAsync();
            ViewBag.Expenses = SelectedExpenses;

            //List Of Category
            List<CategoryDetail> SelectCategory = await _context.categoryDetails.ToListAsync();

            //TotalExpenses
            int TotalExpenses = SelectedExpenses.Sum(j => j.Amount);
            ViewBag.TotalExpenses = TotalExpenses;

            //TotalExpensesLimit
            int TotalExpensesLimit = SelectCategory.Sum(i => i.Expenseslimet);
                ViewBag.TotalExpensesLimit = TotalExpensesLimit;

            //List Of CateoryName
            List<CategoryDetail> categoryname = await _context.categoryDetails.ToListAsync();
            ViewBag.Category = categoryname;

            return View();
        }

        public async Task<IActionResult> Expenselist(int? id)
        {
            if (id == null || _context.expensesDetails == null)
            {
                return NotFound();
            }

            var expensesDetail = await _context.expensesDetails
                .Include(e => e.Category).Where(f => f.Categoryid == id).ToListAsync();
                
            if (expensesDetail == null)
            {
                return NotFound();
            }

            //select Category
            List<CategoryDetail> SelectCategory = await _context.categoryDetails.Where(m=>m.Categoryid ==id).ToListAsync();
            ViewBag.CategoryLimit = SelectCategory;

            //select Expensess
            List<ExpensesDetail> SelectExpenses = await _context.expensesDetails.Include(n => n.Category).ToListAsync();

            int TotalCategory = SelectExpenses.Where(j => j.Categoryid ==id).Sum(k => k.Amount);
            ViewBag.CategoryExpenses = TotalCategory;

            int categorylimit = SelectCategory.Where(y => y.Categoryid == id).Sum(m => m.Expenseslimet);
            ViewBag.Categorylimit = categorylimit;
       

            return View(expensesDetail);
        }

    }
}
