using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace ExpenseTracker.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<CategoryDetail> categoryDetails { get; set; }
        public DbSet<ExpensesDetail> expensesDetails { get; set; }

    }
}
