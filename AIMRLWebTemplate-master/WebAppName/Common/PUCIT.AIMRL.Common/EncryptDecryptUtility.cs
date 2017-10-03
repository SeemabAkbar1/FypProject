 using System;
 using System.IO;
 using System.Text;
 using System.Security.Cryptography;
namespace PUCIT.AIMRL.Common
{
    public class EncryptDecryptUtility
    {
        private static string passPhrase = "pUcITaImRLarrRPRojecT";
        private static string saltValue = "pUcITaImRLarrRPRojecT";
        private static string hashAlgorithm = "MD5";
        private static int passwordIterations = 50;
        private static string initVector = "aIMRLpuCIToReRPJ";
        private static int keySize = 256;

        public static void SetParameters(String pPassPhrase, String pSaltValue, String pHashAlgo, int pPassworIterations, String pInitVector, int pKeySize)
        {
            passPhrase = pPassPhrase;
            saltValue = pSaltValue;
            hashAlgorithm = pHashAlgo;
            passwordIterations = pPassworIterations;
            initVector = pInitVector;
            keySize = pKeySize;
        }

        public static string Encrypt(string plainText)
        {
            byte[] initVectorBytes;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            byte[] plainTextBytes;
            plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            PasswordDeriveBytes password;
            password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            byte[] keyBytes;
            keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey;
            symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor;
            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            MemoryStream memoryStream;
            memoryStream = new MemoryStream();

            CryptoStream cryptoStream;
            cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes;
            cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText;
            cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;

            // Return encrypted string.
        }
        public static string Decrypt(string cipherText)
        {

            byte[] initVectorBytes;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            byte[] cipherTextBytes;
            cipherTextBytes = Convert.FromBase64String(cipherText);

            PasswordDeriveBytes password;
            password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            byte[] keyBytes;
            keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey;
            symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor;
            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            MemoryStream memoryStream;
            memoryStream = new MemoryStream(cipherTextBytes);

            CryptoStream cryptoStream;
            cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount;

            decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string plainText;
            plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            return plainText;

        }

    }
}
