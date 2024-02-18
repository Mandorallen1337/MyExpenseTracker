using MyExpenseTracker.ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseTracker.ExpenseTracker.Interfaces
{
    public interface IBudgetRepository
    {
        Budget GetBudgetsByUser(int userId);
        void AddBudget(Budget budget);
        void UpdateBudget(Budget budget);
        void DeleteBudget(int budgetId);

    }
}
