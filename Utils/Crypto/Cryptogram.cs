using System;
using System.Security.Cryptography;
using System.Text;

namespace GameUtils.Crypto
{
    public class Cryptogram
    {
        private RSACryptoServiceProvider _rsaEncryptCrypto;
        private RSACryptoServiceProvider _rsaDecryptCrypto;

        public Cryptogram()
        {
        }
        public Cryptogram(string publicKey, string privateKey)
        {
            _rsaEncryptCrypto = new RSACryptoServiceProvider(2048);
            _rsaEncryptCrypto.FromXmlString(publicKey);

            _rsaDecryptCrypto = new RSACryptoServiceProvider(2048);
            _rsaDecryptCrypto.FromXmlString(privateKey);
        }
        public void InitializeWithPublicKey(string publicKey)
        {
            _rsaEncryptCrypto = new RSACryptoServiceProvider(2048);
            _rsaEncryptCrypto.FromXmlString(publicKey);
        }
        public void InitializeWithPrivateKey(string privateKey)
        {
            _rsaDecryptCrypto = new RSACryptoServiceProvider(2048);
            _rsaDecryptCrypto.FromXmlString(privateKey);
        }
        public string EncryptString(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return EncryptBytes(bytes);
        }
        public string EncryptBytes(byte[] bytes)
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
            return Tuple.Create(dummy.ToXmlString(false), dummy.ToXmlString(true));
        }
        public string DecryptString(string value)
        {
            var bytes = Convert.FromBase64String(value);
            return DecryptBytes(bytes);
        }
        public string DecryptBytes(byte[] bytes)
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