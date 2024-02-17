using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseTracker.ExpenseTracker.Models
{
    public class Categories
    {
        public int CategoryId { get; set; }
        private string CategoryName { get; set;}
        public Categories()
        {
        }
        public Categories(int categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
    }
    
}
