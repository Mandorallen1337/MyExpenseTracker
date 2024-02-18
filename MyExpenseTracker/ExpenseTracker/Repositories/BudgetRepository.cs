using MyExpenseTracker.ExpenseTracker.Interfaces;
using MyExpenseTracker.ExpenseTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseTracker.ExpenseTracker.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly string connectionString;

        public BudgetRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void AddBudget(Budget budget)
        {
            using(MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO budgets (budget_id, user_id, category_id, budget_amount) VALUES (@budget_id, @user_id, @category_id, @budget_amount)";
                cmd.Parameters.AddWithValue("@budget_id", budget.Budget_id);
                cmd.Parameters.AddWithValue("@user_id", budget.User_id);
                cmd.Parameters.AddWithValue("@category_id", budget.Category_id);
                cmd.Parameters.AddWithValue("@budget_amount", budget.Budget_amount);
                cmd.ExecuteNonQuery();
                
            }
        }

        public void DeleteBudget(int budgetId)
        {
            using(MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM budgets WHERE budget_id = @budget_id";
                cmd.Parameters.AddWithValue("@budget_id", budgetId);
                cmd.ExecuteNonQuery();
            }
        }

        public Budget GetBudgetsByUser(int userId)
        {
            using(MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM budgets WHERE user_id = @user_id";
                cmd.Parameters.AddWithValue("@user_id", userId);
                MySqlDataReader reader = cmd.ExecuteReader();
                Budget budget = new Budget();
                while(reader.Read())
                {
                    budget.Budget_id = reader.GetInt32("budget_id");
                    budget.User_id = reader.GetInt32("user_id");
                    budget.Category_id = reader.GetInt32("category_id");
                    budget.Budget_amount = reader.GetDecimal("budget_amount");
                }
                return budget;
            }
        }

        public void UpdateBudget(Budget budget)
        {
            using(MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE budgets SET budget_id = @budget_id, user_id = @user_id, category_id = @category_id, budget_amount = @budget_amount WHERE budget_id = @budget_id";
                cmd.Parameters.AddWithValue("@budget_id", budget.Budget_id);
                cmd.Parameters.AddWithValue("@user_id", budget.User_id);
                cmd.Parameters.AddWithValue("@category_id", budget.Category_id);
                cmd.Parameters.AddWithValue("@budget_amount", budget.Budget_amount);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
