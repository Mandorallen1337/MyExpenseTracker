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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly string connectionString;
        public ExpenseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void AddExpense(Expense expense)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO expenses (expenseId, userId, categoryId, expenseDate, notes) VALUES (@expenseId, @userId, @categoryId, @expenseDate, @notes)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@expenseId", expense.ExpenseId);
                command.Parameters.AddWithValue("@userId", expense.UserId);
                command.Parameters.AddWithValue("@expenseName", expense.CategoryId);
                command.Parameters.AddWithValue("@expenseAmount", expense.ExpenseDate);
                command.Parameters.AddWithValue("@expenseDate", expense.Notes);
                command.ExecuteNonQuery();
            }

                
        }

        public void DeleteExpense(int expenseId)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM expenses WHERE expenseId = @expenseId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@expenseId", expenseId);
                command.ExecuteNonQuery();
            }
        }

        public Expense GetExpenseByUser(int userId)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM expenses WHERE userId = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader reader = command.ExecuteReader();
                Expense expense = new Expense();
                while(reader.Read())
                {
                    expense.ExpenseId = reader.GetInt32("expenseId");
                    expense.UserId = reader.GetInt32("userId");
                    expense.CategoryId = reader.GetInt32("categoryId");
                    expense.Amount = reader.GetDecimal("amount");
                    expense.ExpenseDate = reader.GetDateTime("expenseDate");
                    expense.Notes = reader.GetString("notes");
                }
                return expense;
            }
        }

        public void UpdateExpense(Expense expense)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE expenses SET userId = @userId, categoryId = @categoryId, amount = @amount, expenseDate = @expenseDate, notes = @notes WHERE expenseId = @expenseId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@expenseId", expense.ExpenseId);
                command.Parameters.AddWithValue("@userId", expense.UserId);
                command.Parameters.AddWithValue("@categoryId", expense.CategoryId);
                command.Parameters.AddWithValue("@amount", expense.Amount);
                command.Parameters.AddWithValue("@expenseDate", expense.ExpenseDate);
                command.Parameters.AddWithValue("@notes", expense.Notes);
                command.ExecuteNonQuery();
            }
        }
    }
}
