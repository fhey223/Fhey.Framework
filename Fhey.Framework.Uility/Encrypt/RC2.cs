using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Uility.Encrypt
{
    public class RC2 : EncryptBase
    {
        public override string Decrypt(string str, params string[] args)
        {
            string result;
            try
            {
                string key = args[0] ?? defaultKey;
                string iv = args[1] ?? defaultIV;
                RC2CryptoServiceProvider rC2 = new RC2CryptoServiceProvider();
                byte[] byteEncryptString = Encoding.UTF8.GetBytes(str);
                MemoryStream memorystream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memorystream, rC2.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv)), CryptoStreamMode.Write);
                cryptoStream.Write(byteEncryptString, 0, byteEncryptString.Length);
                cryptoStream.FlushFinalBlock();
                result = Convert.ToBase64String(memorystream.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public override string Encrypt(string str, params string[] args)
        {
            string result;
            try
            {
                string key = args[0] ?? defaultKey;
                string iv = args[1] ?? defaultIV;
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                RC2CryptoServiceProvider rC2 = new RC2CryptoServiceProvider();
                byte[] byteDecrytString = Convert.FromBase64String(str);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, rC2.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv)), CryptoStreamMode.Write);
                cryptoStream.Write(byteDecrytString, 0, byteDecrytString.Length);
                cryptoStream.FlushFinalBlock();
                result = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
