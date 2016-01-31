using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.LFSR
{
    public class LFSR
    {
        bool[] bits;

        public static string Random16()
        {
            Random random = new Random();
            short value = Convert.ToInt16(random.Next(Int16.MaxValue));

            BitArray bits = new BitArray(new int[] { value });
            
            string res = "";

            int counter = 0;
            foreach (bool bit in bits)
            {
                res += bit ? "1" : "0";
                if (++counter == 16) break;
            }

            return res;
        }

        #region конвертация
        private static BitArray get16BitArrayFromString(string s)
        {
            BitArray res = new BitArray(16);

            int counter = 0;
            foreach (char c in s)
            {              
                bool bit = c == '1' ? true : false;
                res[counter] = bit;
                counter++;
            }

            return res;
        }

        public static int getIntFrom16BitString(string s)
        {
            BitArray bitArray = get16BitArrayFromString(s);

            if (bitArray.Length > 16)
                throw new ArgumentException("Argument length shall be at most 16 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }
        #endregion

        public LFSR(string seed)
        {
            bits = new bool[seed.Length];

            for (int i = 0; i < seed.Length; i++)
                bits[i] = seed[i] == '1' ? true : false;

        }

        public string Registry
        {
            get
            {
                char[] reg = new char[bits.Length];
                for (int i = 0; i < bits.Length; i++)
                    reg[i] = bits[i] ? '1' : '0';

                return new string(reg);
            }
        }

        public string RegistryReverse
        {
            get
            {
                string reg = Registry;

                char[] charArray = reg.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }

        }

        public void Shift()
        {
            bool bnew = !(bits[bits.Length - 1] == bits[bits.Length - 2]);

            for (int i = bits.Length - 1; i > 0; i--)
            {
                bits[i] = bits[i - 1];
            }
            bits[0] = bnew;
        }

    }
}
