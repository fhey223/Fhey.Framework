using Fhey.framework.Uility.Cache.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Uility.Encrypt
{
    public class DES : EncryptBase
    {


        /// <summary>
        ///     DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public override string Encrypt(string str, params string[] args)
        {
            string key = args[0]??defaultKey;
            string iv = args[1]??defaultIV;

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] bytesOut;

            sa = new DESCryptoServiceProvider();
            sa.Key = encoding.GetBytes(key);
            sa.IV = encoding.GetBytes(iv);
            ct = sa.CreateEncryptor();

            bytesOut = encoding.GetBytes(str);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(bytesOut, 0, bytesOut.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(bytesOut);
        }

        /// <summary>
        ///     DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public override string Decrypt(string str, params string[] args)
        {
            string key = args[0] ?? defaultKey;
            string iv = args[1] ?? defaultIV;

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] bytesOut;

            sa = new DESCryptoServiceProvider();
            sa.Key = encoding.GetBytes(key);
            sa.IV = encoding.GetBytes(iv);
            ct = sa.CreateDecryptor();

            bytesOut = Convert.FromBase64String(str);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(bytesOut, 0, bytesOut.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(bytesOut);
        }
    }
}
