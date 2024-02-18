using MyExpenseTracker.ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseTracker.ExpenseTracker.Interfaces
{
    public interface IExpenseRepository
    {
        Expense GetExpenseByUser(int userId);
        void AddExpense(Expense expense);
        void UpdateExpense(Expense expense);
        void DeleteExpense(int expenseId);
        
    }
}
