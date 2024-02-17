using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseTracker.ExpenseTracker.Models
{
    public class Expense
    {
   
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Notes { get; set; }

        public Expense() { }

        public Expense(int expenseId, int userId, int categoryId, decimal amount, DateTime expenseDate, string notes)
        {
            ExpenseId = expenseId;
            UserId = userId;
            CategoryId = categoryId;
            Amount = amount;
            ExpenseDate = expenseDate;
            Notes = notes;
        }


    }
}
