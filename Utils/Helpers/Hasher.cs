using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Helpers
{
    public enum HashAlg
    {
        SHA1,
        SHA256,
        SHA384,
        SHA512,
        HMACSHA1,
        HMACSHA256,
        HMACSHA384,
        HMACSHA512
    }

    public static class Hasher
    {
        private static byte[] Salt = new byte[] { 121, 213, 43, 57, 89, 56, 72, 87 };

        public static string? Hash(string? value, HashAlg? alg = HashAlg.SHA512)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            var hashedValue = string.Empty;

            using (HashAlgorithm algorithm = alg switch
            {
                HashAlg.SHA1 => SHA1.Create(),
                HashAlg.SHA256 => SHA256.Create(),
                HashAlg.SHA384 => SHA384.Create(),
                HashAlg.SHA512 => SHA512.Create(),
                HashAlg.HMACSHA1 => HMAC.Create("System.Security.Cryptography.HMACSHA1"),
                HashAlg.HMACSHA256 => HMAC.Create("System.Security.Cryptography.HMACSHA256"),
                HashAlg.HMACSHA384 => HMAC.Create("System.Security.Cryptography.HMACSHA384"),
                HashAlg.HMACSHA512 => HMAC.Create("System.Security.Cryptography.HMACSHA512"),
                _ => HMAC.Create("System.Security.Cryptography.HMACSHA512")
            })
            {
                var pswBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
                hashedValue = string.Concat(Array.ConvertAll(pswBytes, b => b.ToString("x2")));
            }

            return hashedValue;
        }

        public static string? Encrypt(string? value, string? key)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var valueBytes = Encoding.UTF8.GetBytes(value);

            var dKey = new Rfc2898DeriveBytes(keyBytes, Salt, 1000);

            using var mStream = new MemoryStream();

            using var encryptor = new RijndaelManaged()
            {
                KeySize = 256,
                BlockSize = 128,
                Key = dKey.GetBytes(256 / 8),
                IV = dKey.GetBytes(128 / 8),
                Mode = CipherMode.CBC
            };

            using (var cs = new CryptoStream(mStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(valueBytes, 0, valueBytes.Length);
                cs.Close();
            }

            return Convert.ToBase64String(mStream.ToArray());
        }

        public static string? Decrypt(string? value, string? key)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var valueBytes = Convert.FromBase64String(value);

            var dKey = new Rfc2898DeriveBytes(keyBytes, Salt, 1000);

            using var mStream = new MemoryStream();

            using var encryptor = new RijndaelManaged()
            {
                KeySize = 256,
                BlockSize = 128,
                Key = dKey.GetBytes(256 / 8),
                IV = dKey.GetBytes(128 / 8),
                Mode = CipherMode.CBC
            };

            using (var cs = new CryptoStream(mStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(valueBytes, 0, valueBytes.Length);
                cs.Close();
            }

            return Encoding.UTF8.GetString(mStream.ToArray());

            /*using var encryptor = TripleDES.Create();
            encryptor.Key = keyBytes;
            encryptor.Mode = CipherMode.ECB;
            encryptor.Padding = PaddingMode.PKCS7;

            var decryptedValue = encryptor.CreateDecryptor().TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            encryptor.Clear();

            return Convert.ToBase64string?(decryptedValue);*/
        }
    }
}
