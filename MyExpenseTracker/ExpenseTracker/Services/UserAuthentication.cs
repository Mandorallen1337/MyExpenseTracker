using MyExpenseTracker.ExpenseTracker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseTracker.ExpenseTracker.Services
{
    public class UserAuthentication
    {
        private readonly IUserRepository userRepository;

        public UserAuthentication(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool AuthenticateUser(string username, string password)
        {
            var user = userRepository.GetUserByUsername(username);
            if (user != null && user.Password == password)
            {
                return true;
            }
            return false;
        }
    }
}
