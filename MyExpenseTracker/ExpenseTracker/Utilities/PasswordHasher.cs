using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

public class PasswordHasher
{
    public string HashPassword(string password)
    {
        // Generate a salt and hash the password
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return hashedPassword;
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        // Verify the password against the hashed password
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
