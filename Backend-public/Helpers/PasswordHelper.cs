using System;
using System.Security.Cryptography;
using System.Text;

namespace SportsApp.Helpers
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16; 
        private const int KeySize = 32; 
        private const int Iterations = 10000; 

       
        public static string HashPassword(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256))
            {
                var salt = algorithm.Salt;
                var key = algorithm.GetBytes(KeySize);

               
                var saltBase64 = Convert.ToBase64String(salt);
                var keyBase64 = Convert.ToBase64String(key);

                
                return $"{saltBase64}:{keyBase64}";
            }
        }

        
        public static bool VerifyPassword(string hashedPasswordWithSalt, string inputPassword)
        {
            var parts = hashedPasswordWithSalt.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            var salt = Convert.FromBase64String(parts[0]);
            var key = Convert.FromBase64String(parts[1]);

            using (var algorithm = new Rfc2898DeriveBytes(inputPassword, salt, Iterations, HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                return keyToCheck.SequenceEqual(key);
            }
        }
    }
}
