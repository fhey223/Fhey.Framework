using System;
using System.IO;
using System.Security.Cryptography;

namespace Fhey.Framework.Uility.Encrypt
{
    public class AES: EncryptBase
    {
        //默认密钥
        protected static readonly new string defaultKey = "edoctorBaseFrameedoctorB";

        //默认密矢量
        protected static readonly new string defaultIV = "Rkb4jvUy/ye7Cd7k89QQgQ==";

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="encryptKey">加密密匙</param>
        /// <returns></returns>
        public override string Encrypt(string str, params string[] args)
        {
            string key = args[0]?? defaultKey;
            string iv = args[1] ?? defaultIV;
            string result = "";
            byte[] temp = Convert.FromBase64String(iv);
            Rijndael AESProvider = Rijndael.Create();
            try
            {
                byte[] byteDecryptString = Convert.FromBase64String(str);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, AESProvider.CreateDecryptor(encoding.GetBytes(key), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteDecryptString, 0, byteDecryptString.Length);
                cryptoStream.FlushFinalBlock();
                result = encoding.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }


        /// <summary>
        ///AES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="key">解密密钥</param>
        /// <returns></returns>
        public override string Decrypt(string str, params string[] args)
        {
            string key = args[0] ?? defaultKey;
            string iv = args[1] ?? defaultIV;

            string result = "";
            byte[] temp = Convert.FromBase64String(iv);
            Rijndael AESProvider = Rijndael.Create();
            try
            {
                byte[] byteDecryptString = Convert.FromBase64String(str);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, AESProvider.CreateDecryptor(encoding.GetBytes(key), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteDecryptString, 0, byteDecryptString.Length);
                cryptoStream.FlushFinalBlock();
                result = encoding.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
