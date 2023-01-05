using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RICH_Connector.Clover
{
    public static class Utils
    {

        public static string GenerateRandomString(int length)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            byte[] data = new byte[length];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }
    }
}
