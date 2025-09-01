using System.Security.Cryptography;
using System.Text;

namespace TestHash
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = "admin123";
            string hash = HashPassword(password);
            Console.WriteLine($"Password: {password}");
            Console.WriteLine($"Hash: {hash}");
        }
        
        static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
