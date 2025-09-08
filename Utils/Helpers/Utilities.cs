using System.Security.Cryptography;
using System.Text;

namespace Utils.Helpers
{
    public static class Utilities
    {
        public static string Obfuscate(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            int visibleCount = Math.Min(2, text.Length); // Muestra 2 caracteres
            var ofuscatedLenght = Math.Min(6, text.Length);
            string obfuscatedPart = new string('*', ofuscatedLenght - visibleCount);
            string visibleEnd = text.Substring(text.Length - visibleCount);
            string visibleStart = text.Substring(0, visibleCount);

            return visibleStart + obfuscatedPart + visibleEnd;
        }

        public static string GenerateSalt(int length = 16)
        {
            byte[] saltBytes = new byte[length];
            RandomNumberGenerator.Fill(saltBytes); // Genera bytes aleatorios seguros
            return Convert.ToBase64String(saltBytes);
        }
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
                _ => throw new ArgumentNullException(nameof(alg)),
            })
            {
                var pswBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
                hashedValue = string.Concat(Array.ConvertAll(pswBytes, b => b.ToString("x2")));
            }

            return hashedValue;
        }

        public static string Encrypt(string plainText, Guid key, string secret)
        {
            byte[] iv = Convert.FromBase64String(secret); // IV en bytes

            using var aes = Aes.Create();
            aes.Key = key.ToByteArray();
            aes.IV = iv;

            using var encryptor = aes.CreateEncryptor();
            byte[] buffer = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string cipherText, Guid key, string secret)
        {
            byte[] iv = Convert.FromBase64String(secret);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = key.ToByteArray();
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
        public static Dictionary<string, int> GetEnumDefinition<T>() where T : Enum
        {
            // Obtener todos los valores del enum
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();

            // Crear un diccionario clave-valor
            var enumDictionary = enumValues.ToDictionary(
                value => value.ToString().ToLower(), // Clave: nombre del valor del enum
                value => Convert.ToInt32(value)        // Valor: número equivalente
            );
            return enumDictionary;
        }


    }

}
