using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class CategoryDetail
    {
        [Key]
        public int Categoryid{ get; set; }

        [Column (TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
       

        [Required(ErrorMessage = "Expenses Limet is required.")]
        [DisplayName("Expenses Limit")]
        public int Expenseslimet { get; set; }
    }
}
