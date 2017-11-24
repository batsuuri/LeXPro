using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LeXPro.Core
{
    public class Crypto
    {
        private static Random random = new Random();
        private static string KEY = "mvpdwe";
        internal static string GenerateRandomCode(int len)
        {
            string s = "";
            for (int i = 0; i < len; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public static string DecryptText(string pText)
        {
            return DecryptText(KEY, pText);
        }

        public static string EncryptText(string pText)
        {
            return EncryptText(KEY, pText);
        }

        static public string EncryptText(string key, string text)
        {
            //{&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
            byte[] iv = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] k = Encoding.UTF8.GetBytes(Func.Mid(key, 1, 8));
            byte[] textbytes = Encoding.UTF8.GetBytes(text);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(k, iv), CryptoStreamMode.Write))
            {
                cs.Write(textbytes, 0, textbytes.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        static public string DecryptText(string key, string text)
        {
            byte[] iv = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] k = Encoding.UTF8.GetBytes(Func.Mid(key, 1, 8));
            byte[] textbytes = Convert.FromBase64String(text);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(k, iv), CryptoStreamMode.Write))
            {
                cs.Write(textbytes, 0, textbytes.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        static public string Hash(string toHash)
        {
            byte[] inArr;
            byte[] outArr;

            inArr = System.Text.Encoding.ASCII.GetBytes(toHash);
            SHA1Managed hash = new SHA1Managed();
            outArr = hash.ComputeHash(inArr);
            return Convert.ToBase64String(outArr);
        }

        static public int GenerateActivationCode()
        {
            int code = 0;
            Random random = new Random();

            code = random.Next(1000, 9999);
            return code;
        }


    }
}
