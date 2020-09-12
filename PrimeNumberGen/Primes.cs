using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PrimeNumberGen {
    public static class Primes {

        private static List<int> KnownPrimes = new List<int>() { 2, 3, 5, 7, 11, 13 };
        private static Stopwatch stopwatch = new Stopwatch();

        private static int GenerateNewPrimes(int numPrimes)
        {
            int bitLevel = BinaryHelper.bitCount(KnownPrimes[KnownPrimes.Count - 1]);
            int newPrimes = 0;
            while (numPrimes > KnownPrimes.Count)
            {
                int[] suspects = BinaryHelper.nBitPrimeSuspects(bitLevel);
                for (int i = 0; i < suspects.Length; i++)
                {
                    int suspect = suspects[i];
                    if (IsPrime(suspect) && KnownPrimes.Contains(suspect) == false)
                    {
                        KnownPrimes.Add(suspect);
                        newPrimes++;
                    }
                    if (numPrimes <= KnownPrimes.Count) break;
                }
                bitLevel++;
            }
            return newPrimes;
        }

        public static void DisplayNthPrimeInfo(int numPrimes)
        {
            stopwatch.Restart();
            int newPrimes = 0;
            if (numPrimes > KnownPrimes.Count) newPrimes = GenerateNewPrimes(numPrimes);
            stopwatch.Stop();

            Console.WriteLine("");
            Console.WriteLine($"Number of primes known: {KnownPrimes.Count}");
            Console.WriteLine($"Number of new primes generated: {newPrimes}");
            Console.WriteLine($"Nth Prime (N = {numPrimes}): {KnownPrimes[numPrimes - 1]}");
            Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("");
        }

        public static bool IsPrime(int value)
        {
            //-- Already accounted for
            //if (value <= 3 && value > 1) return true;
            //if (value == 1 || value % 2 == 0 || value % 3 == 0) return false;

            if (value % 3 == 0) return false;

            //-- Improved further by observing that all primes are of the form 6k ± 1
            for (int i = 5; i * i <= value; i += 6)
            {
                if (value % i == 0 || value % (i + 2) == 0) return false;
            }

            return true;
        }

    }
}
