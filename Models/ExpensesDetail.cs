using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class ExpensesDetail
    {
        [Key]
        public int Expensesid { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int Categoryid { get; set; }
        public CategoryDetail? Category { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Description { get; set;}

        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        public int Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;


    }
}
