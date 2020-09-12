using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PrimeNumberGen {
    public static class Primes {

        private static List<int> _KnownPrimes = new List<int>() { 2, 3, 5, 7, 11, 13 };
        private static Stopwatch _Stopwatch = new Stopwatch();

        private static int GenerateNewPrimes(int numPrimes)
        {
            int bitLevel = BinaryHelper.bitCount(_KnownPrimes[_KnownPrimes.Count - 1]);
            int newPrimes = 0;
            while (numPrimes > _KnownPrimes.Count)
            {
                int[] suspects = BinaryHelper.nBitPrimeSuspects(bitLevel);
                for (int i = 0; i < suspects.Length; i++)
                {
                    int suspect = suspects[i];
                    if (IsPrime(suspect) && _KnownPrimes.Contains(suspect) == false)
                    {
                        _KnownPrimes.Add(suspect);
                        newPrimes++;
                    }
                    if (numPrimes <= _KnownPrimes.Count) break;
                }
                bitLevel++;
            }
            return newPrimes;
        }

        public static void DisplayNthPrimeInfo(int numPrimes)
        {
            _Stopwatch.Restart();
            int newPrimes = GenerateNewPrimes(numPrimes);
            _Stopwatch.Stop();

            Console.WriteLine("");
            Console.WriteLine($"Number of primes known: {_KnownPrimes.Count}");
            Console.WriteLine($"Number of new primes generated: {newPrimes}");
            Console.WriteLine($"Nth Prime (N = {numPrimes}): {_KnownPrimes[numPrimes - 1]}");
            Console.WriteLine($"Elapsed Time: {_Stopwatch.ElapsedMilliseconds}ms");
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
