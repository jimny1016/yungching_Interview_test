using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Encryption
    {
        /// <summary>
        /// base64加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string EncryptBase64(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            return Convert.ToBase64String(inputBytes);
        }

        /// <summary>
        /// base64解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string DecryptBase64(string input)
        {
            byte[] inputBytes = Convert.FromBase64String(input);

            return Encoding.UTF8.GetString(inputBytes);
        }
    }
}
