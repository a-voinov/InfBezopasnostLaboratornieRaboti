using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.LFSR
{
    public static class LFSR16
    {
        public static short Random16()
        {
            Random random = new Random();
            short value = Convert.ToInt16(random.Next(Int16.MaxValue));

            return value;
        }

        public static int GetFullCycleShiftCount(int num)
        {
            int lfsr = num;
            int counter = 0;

            do
            {
                int bit = ((lfsr >> 0) ^ (lfsr >> 2) ^ (lfsr >> 3) ^ (lfsr >> 5)) & 1;
                lfsr = (lfsr >> 1) | (bit << 15);
                counter++;
            }
            while (lfsr != num);

            return counter;
        }

        public static int Shift(int num)
        {
            int lfsr = num;

            int bit = ((lfsr >> 0) ^ (lfsr >> 2) ^ (lfsr >> 3) ^ (lfsr >> 5)) & 1;
                lfsr = (lfsr >> 1) | (bit << 15);

            return lfsr;
        }       

    }
}
