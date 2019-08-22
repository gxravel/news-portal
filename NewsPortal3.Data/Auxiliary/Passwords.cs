using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NewsPortal3.Data.Auxiliary
{
    public static class Passwords
    {
        private const int Length = 8;
        private const string AlphanumericCharacters =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
        "abcdefghijklmnopqrstuvwxyz" +
        "0123456789";
        public static string New()
        {
            var bytes = new byte[Length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[Length];
            IEnumerable<char> characterSet = AlphanumericCharacters;
            var characterArray = characterSet.Distinct().ToArray();
            for (int i = 0; i<Length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string (result);
        }
    }
}
