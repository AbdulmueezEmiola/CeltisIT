using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CeltisIT.Model
{
    public static class SecurePassword
    {
        private static byte[] salt = new byte[16];

        public static string HashPassword(string value)
        {
            new RNGCryptoServiceProvider().GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(value, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashbytes = new byte[36];
            Array.Copy(salt, 0, hashbytes, 0, 16);
            Array.Copy(hash, 0, hashbytes, 16, 20);
            return Convert.ToBase64String(hashbytes);
        }
        public static bool ConfirmPassword(string password, string hashedPassword)
        {
            byte[] hashbytes = Convert.FromBase64String(hashedPassword);
            Array.Copy(hashbytes, 0, salt,0,16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (var i = 0; i < 20; i++)
            {
                if (hashbytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
