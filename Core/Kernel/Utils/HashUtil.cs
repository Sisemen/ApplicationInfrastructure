using System.Globalization;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace Core.Kernel.Utils
{
    public class HashUtil
    {
        public static string CreateHexStringHash(HashAlgorithmType type, byte[] input)
        {
            byte[]? hash = null;
            using (var hashAlgorithm = HashAlgorithmFactory.Create(type))
            {
                hash = hashAlgorithm.ComputeHash(input);
            }

            var result = new StringBuilder(hash.Length);

            foreach (var key in hash)
            {
                result.Append(key.ToString("x2", CultureInfo.InvariantCulture));
            }

            return result.ToString();
        }

        public static byte[] CreateHash(HashAlgorithmType type, byte[] input)
        {
            byte[]? hash = null;
            using (var hashAlgorithm = HashAlgorithmFactory.Create(type))
            {
                hash = hashAlgorithm.ComputeHash(input);
            }

            return hash;
        }

        public static byte[] CreateHash(HashAlgorithmType type, byte[] input, byte[] salt)
        {
            byte[]? hash = null;
            using (var hashAlgorithm = HashAlgorithmFactory.Create(type))
            {
                hash = hashAlgorithm.ComputeHash(input);
            }

            return hash;
        }

        public static byte[] CalculatePasswordHash(byte[] password, byte[] salt)
        {
            return CalculatePasswordHash(password, salt, HashAlgorithmName.SHA256);
        }

        public static byte[] CalculatePasswordHash(byte[] password, byte[] salt, HashAlgorithmName algorithmName)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000, algorithmName))
            {
                return deriveBytes.GetBytes(32);
            }
        }
    }
}
