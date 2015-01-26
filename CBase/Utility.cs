using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBase
{
    public static class BytesConvertUtility
    {
        public static byte[] ToBytes(this string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }

        public static byte[] ToBytes(this int input)
        {
            return BitConverter.GetBytes(input);
        }
        public static int ToInt32(this byte[] input)
        {
            return BitConverter.ToInt32(input, 0);
        }

        public static byte[] ToBytes(this long input)
        {
            return BitConverter.GetBytes(input);
        }

        public static long ToInt64(this byte[] input)
        {
            return BitConverter.ToInt64(input,0);
        }

        public static byte[] ToBytes(this bool input)
        {
            return BitConverter.GetBytes(input);
        }
        public static bool ToBoolean(this byte[] input)
        {
            return BitConverter.ToBoolean(input, 0);
        }
    }
}
