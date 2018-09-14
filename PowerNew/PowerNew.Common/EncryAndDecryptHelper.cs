using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace PowerNew.Common
{
    public class EncryAndDecryptHelper
    {

        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encryption(string str)
        {
            string htext = "";
            if (!string.IsNullOrWhiteSpace(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    htext = htext + (char)(str[i] + 10 - 1 * 2);
                }
            }

            return htext;
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decryption(string str)
        {
            string dtext = "";
            if (!string.IsNullOrWhiteSpace(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    dtext = dtext + (char)(str[i] - 10 + 1 * 2);
                }
            }
            return dtext;
        }

        public interface IBindesh
        {
            string encode(string str);
            string decode(string str);
        }

    }
}
