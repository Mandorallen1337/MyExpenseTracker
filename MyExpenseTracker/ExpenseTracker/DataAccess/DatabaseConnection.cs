using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseTracker.ExpenseTracker.DataAccess
{
    public class DatabaseConnection
    {
                
        
        public string connectionString = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString;
        
        public DatabaseConnection(string server, string database, string uid, string password)
        {
            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(database) || string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Server, Database, Uid and Password are required");
            }
            connectionString = $"Server={server};Database={database};Uid={uid};Pwd={password};";
        }

        public DatabaseConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string is required");
            }
            this.connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        
    }
}
