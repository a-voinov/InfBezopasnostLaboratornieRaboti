using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public static class ByteOperations
    {
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static byte[] GetBytes(int num)
        {
            byte[] intBytes = BitConverter.GetBytes(num);
            Array.Reverse(intBytes);
            return intBytes;
        }

        public static byte[] GetBytes(int[] array)
        {
            byte[] intBytes = new byte[array.Length * sizeof(int)];
            Buffer.BlockCopy(array, 0, intBytes, 0, intBytes.Length);
            return intBytes;
        }

        /// <summary>
        /// циклический сдвиг влево
        /// </summary>
        /// <param name="input">число для сдвига</param>
        /// <param name="n">n бит</param>
        /// <param name="b">на b бит</param>
        /// <returns></returns>
        public static uint shiftLeft(uint input, int n, int b)
        {
            if ((b - n) > 0)
                return input << n | input >> (b - n);
            else
                throw new Exception("Shift left exception");
        }

        /// <summary>
        /// циклический сдвиг вправо
        /// </summary>
        /// <param name="input">число для сдвига</param>
        /// <param name="n">n бит</param>
        /// <param name="b">на b бит</param>
        /// <returns></returns>
        public static uint shiftRight(uint input, int n, int b)
        {
            if ((b - n) > 0)
                return input >> n | ((input << (b - n)) & 255);
            else
                throw new Exception("Shift right exception");
        }
    }
}
