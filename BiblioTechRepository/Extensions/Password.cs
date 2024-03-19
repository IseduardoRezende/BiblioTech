using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace BiblioTechDomain.Extensions
{
    public static class Password
    {
        public static string HashPassword(this string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public static byte[] SaltPassword(this string salt)
        {
            return Encoding.UTF8.GetBytes(salt);
        }
    }
}
