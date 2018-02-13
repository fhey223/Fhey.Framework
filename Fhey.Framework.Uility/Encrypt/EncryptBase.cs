using Fhey.Framework.Uility.Cache.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Uility.Encrypt
{
    public abstract class EncryptBase : IEncryptBase
    {
        protected static readonly Encoding encoding = Encoding.UTF8;

        //默认密钥
        protected static readonly string defaultKey = "123456";

        //默认密矢量
        protected static readonly string defaultIV = "123456";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract string Encrypt(string str, params string[] args);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract string Decrypt(string str, params string[] args);
    }
}
