using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Security
{
    public class PasswordHasher
    {
        public static string HashPasswrodMD5(string password)
        {
            Byte[] originPass;
            Byte[] encodedPath;
            MD5 md5;
            md5=new MD5CryptoServiceProvider();
            originPass = ASCIIEncoding.Default.GetBytes(password);
            encodedPath = md5.ComputeHash(originPass);
            return BitConverter.ToString(encodedPath);
        }
    }
}
