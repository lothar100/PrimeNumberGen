using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeNumberGen {
    static class BinaryHelper {

        public static int reverseBits(int value)
        {
            int rev = 0;
            while(value > 0)
            {
                rev <<= 1;
                if ((value & 1) == 1) rev ^= 1;
                value >>= 1;
            }
            return rev;
        }

        public static int[] nBitPrimeSuspects(int nPower)
        {
            int min = (1 << nPower - 1);
            int[] result = new int[min / 2];

            for (int i = 0; i < min / 2; i++)
            {
                result[i] = min + (2*(i+1)) - 1;
            }

            return result;
        }

        public static int bitCount(int value)
        {
            int count = 0;
            while(value > 0)
            {
                count++;
                value = value >> 1;
            }
            return count;
        }
    }
}
