using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WLTIDeliveryTool.Helper
{
    public class AuthHelper
    {
        public enum Encryption
        {
            MD5,
            SHA
        }

        public static string EncryptString(Encryption enc, string str)
        {
            byte[] byte_array = Encoding.Default.GetBytes(str);
            HashAlgorithm alg = HashAlgorithm.Create(enc.ToString());

            byte[] byte_array2 = alg.ComputeHash(byte_array);

            var sb = new StringBuilder(byte_array2.Length);

            foreach (byte b in byte_array2)
            {
                sb.AppendFormat("{0:X2}", b);
            }

            return sb.ToString();
        }

        public string GenerateSalt(int len)
        {
            return GenerateRandChars(len, 36);
        }

        public static string GeneratePassword(int len)
        {
            return GenerateRandChars(len, 55);
        }

        private static string GenerateRandChars(int len, int chars)
        {
            int c1;
            string Result = "";
            var Rnd = new Random(DateTime.Now.Millisecond);
            for (c1 = 0; c1 < len; c1++)
            {
                Result += RndAlphaNum(Rnd, chars);
            }
            return Result;

        }

        private static char RndAlphaNum(Random InRnd, int chars)
        {
            int ThisVal = InRnd.Next(chars); // include 0-9 & A-Z (10 + 26)
            if (ThisVal > 9) ThisVal += 55;
            else ThisVal += 48;
            return (char)ThisVal;
        }
    }
}