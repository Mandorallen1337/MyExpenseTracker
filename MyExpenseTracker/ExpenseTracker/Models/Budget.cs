using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseTracker.ExpenseTracker.Models
{
    public class Budget
    {    
        public int Budget_id { get; set; }
        public int User_id { get; set; }            
        public int Category_id { get; set; }
        public decimal Budget_amount { get; set; }
        public Budget() { }
        
        public Budget(int budget_id, int user_id, int category_id, decimal budget_amount)
        {
            Budget_id = budget_id;
            User_id = user_id;
            Category_id = category_id;
            Budget_amount = budget_amount;
        }

    }
}
