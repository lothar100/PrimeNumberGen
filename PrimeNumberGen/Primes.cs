using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace PrimeNumberGen {
    public static class Primes {

        private static List<int> _KnownPrimes = new List<int>() { 2, 3, 5, 7, 11, 13 };
        private static Stopwatch _Stopwatch = new Stopwatch();
        private static int _SearchDepth = 1;

        private static int GenerateNewPrimes(int numPrimes)
        {
            int newPrimes = 0;
            while (numPrimes > _KnownPrimes.Count)
            {
                var pair = NextPrimeCompositePair();

                if (IsPrime(pair.First))
                {
                    _KnownPrimes.Add(pair.First);
                    newPrimes++;
                }
                if (IsPrime(pair.Second))
                {
                    _KnownPrimes.Add(pair.Second);
                    newPrimes++;
                }
            }
            return newPrimes;
        }

        public static void DisplayNthPrimeInfo(int numPrimes)
        {
            ConsoleSpinner.Start();
            _Stopwatch.Restart();
            int newPrimes = GenerateNewPrimes(numPrimes);
            _Stopwatch.Stop();
            ConsoleSpinner.Stop();
            
            Console.WriteLine("");
            Console.WriteLine($"Number of primes known: {_KnownPrimes.Count}");
            Console.WriteLine($"Number of new primes generated: {newPrimes}");
            Console.WriteLine($"Nth Prime (N = {numPrimes}): {_KnownPrimes[numPrimes - 1]}");
            Console.WriteLine($"Elapsed Time: {_Stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("");
        }

        private static (int First, int Second) NextPrimeCompositePair()
        {
            _SearchDepth++;
            return (5 + (6 * _SearchDepth), 5 + (6 * _SearchDepth) + 2);
        }

        public static bool IsPrime(int value)
        {
            //-- Already accounted for
            //if (value <= 3 && value > 1) return true;
            //if (value == 1 || value % 2 == 0 || value % 3 == 0) return false;

            //-- Improved further by observing that all primes are of the form 6k ± 1
            for (int i = 5; i * i <= value; i += 6)
            {
                if (value % i == 0 || value % (i + 2) == 0) return false;
            }

            return true;
        }

    }
}
