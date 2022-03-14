using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AppEncrypitor
{
    public static class Seguranca
    {
        private static byte[] key = Convert.FromBase64String("wLJcecy1V8O+2xfZN6Sb3+9PRSHEpuZcTgihIN2I904=");
        private static byte[] iv = Convert.FromBase64String("JTfM6sPKRElOe5oqEJrivA==");

        private static string RemoveAcento(this string palavra)
        {
            palavra = palavra.Normalize(NormalizationForm.FormD);
            var chars = palavra.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public static string ToCrypt(this string aux)
        {
            aux = aux.RemoveAcento();

            return String.Join(" ", aux.Split(' ').Select(s => s.Encrypt(key, iv)));
        }

        public static string FromCript(this string crypt)
        {
            try
            {
                return String.Join(" ", crypt.Split(' ').Select(s => s.Decrypt(key, iv)));
            }
            catch
            {
                return crypt;
            }
        }

        private static string Encrypt(this string txt, byte[] key = null, byte[] iv = null)
        {
            string encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(txt);
                        }
                        encrypted = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
            return encrypted;
        }

        public static string Decrypt(this string base64, byte[] key, byte[] iv)
        {
            // Declare the string used to hold
            // the decrypted text.
            string result = null;

            byte[] cipherText = Convert.FromBase64String(base64);

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            result = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return result;
        }

    }

}
