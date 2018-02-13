using System;
using System.Text;

namespace Fhey.Framework.Uility.Cache.Interface
{
    public interface IEncryptBase
    {
        string Encrypt(string str, params string[] args);

        string Decrypt(string str,  params string[] args);
    }
}
