using System;
using System.Security.Cryptography;
using System.Text;

namespace GameUtils.Crypto
{
    public class Cryptogram
    {
        private static RSACryptoServiceProvider _rsaEncryptCrypto;
        private static RSACryptoServiceProvider _rsaDecryptCrypto;

        public static void InitializeWithPublicKey(string publicKey)
        {
            _rsaEncryptCrypto = new RSACryptoServiceProvider(2048);
            _rsaEncryptCrypto.FromXmlString(publicKey);
        }
        public static void InitializeWithPrivateKey(string privateKey)
        {
            _rsaDecryptCrypto = new RSACryptoServiceProvider(2048);
            _rsaDecryptCrypto.FromXmlString(privateKey);
        }
        public static string EncryptString(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return EncryptBytes(bytes);
        }
        public static string EncryptBytes(byte[] bytes)
        {
            if (_rsaEncryptCrypto == null)
            {
                throw new NullReferenceException(nameof(RSACryptoServiceProvider));
            }
            var encryptBytes = _rsaEncryptCrypto.Encrypt(bytes, false);
            return Convert.ToBase64String(encryptBytes);
        }

        public static Tuple<string, string> GenerateKeyPair()
        {
            var dummy = new RSACryptoServiceProvider(2048);
            return Tuple.Create(dummy.ToXmlString(false), dummy.ToXmlString(true)); ;
        }
        public static string DecryptString(string value)
        {
            var bytes = Convert.FromBase64String(value);
            return DecryptBytes(bytes);
        }
        public static string DecryptBytes(byte[] bytes)
        {
            if (_rsaDecryptCrypto == null)
            {
                throw new NullReferenceException(nameof(RSACryptoServiceProvider));
            }
            var decryptBytes = _rsaDecryptCrypto.Decrypt(bytes, false);
            return Encoding.UTF8.GetString(decryptBytes);
        }
    }

}