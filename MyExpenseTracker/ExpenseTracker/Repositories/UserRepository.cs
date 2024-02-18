using MyExpenseTracker.ExpenseTracker.DataAccess;
using MyExpenseTracker.ExpenseTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseTracker.ExpenseTracker.Repositories
{
    public class UserRepository : IUserRepository
    {        
        private readonly string connectionString;
        PasswordHasher passwordHasher = new PasswordHasher();

        public UserRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString;
        }
        public void AddUser(User user)
        {
            if (UsernameExists(user.Username))
            {
                throw new Exception("Username already exists");
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    //Hash the password before storing it in the database
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO users (username, password, email) VALUES (@username, @password, @email)", conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.ExecuteNonQuery();
                }
            }                                     
        }

        public void DeleteUser(int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM users WHERE user_id = @userId", conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
        }

        public User GetUserById(int userId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE user_id = @userId", conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader reader = cmd.ExecuteReader();
                User user = new User();
                while (reader.Read())
                {
                    user.UserId = reader.GetInt32("user_id");
                    user.Username = reader.GetString("username");
                    user.Password = reader.GetString("password");
                    user.Email = reader.GetString("email");
                }
                return user;
            }
        }

        public User GetUserByUsername(string username)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                MySqlDataReader reader = cmd.ExecuteReader();
                User user = new User();
                while (reader.Read())
                {
                    user.UserId = reader.GetInt32("user_id");
                    user.Username = reader.GetString("username");
                    user.Password = reader.GetString("password");
                    user.Email = reader.GetString("email");
                }
                return user;
            }
        }

        public void UpdateUser(User user)
        {
            
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE users SET username = @username, password = @password, email = @email WHERE user_id = @userId", conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@userId", user.UserId);
                cmd.ExecuteNonQuery();
            }
        }

        public void LoginCheck(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Retrieve the hashed password from the database based on the provided username
                string hashedPasswordFromDb;
                using (MySqlCommand cmd = new MySqlCommand("SELECT password FROM users WHERE username = @username", connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    hashedPasswordFromDb = (string)cmd.ExecuteScalar();
                }

                // If no password is found for the username, or if the provided password doesn't match the hashed password from the database, throw an exception
                if (hashedPasswordFromDb == null || !passwordHasher.VerifyPassword(password, hashedPasswordFromDb))
                {
                    throw new Exception("Invalid username or password");
                }
            }
        }


        public bool UsernameExists(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE username = @username", connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        public void RegisterUser(string username, string password, string email)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO users (username, password, email) VALUES (@username, @password, @email)", connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
