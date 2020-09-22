using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace PrimeNumberGen {
    public static class Primes {

        private static List<int> _KnownPrimes = new List<int>() { 2, 3, 5, 7, 11, 13 };
        private static ConcurrentQueue<int> _NewPrimes = new ConcurrentQueue<int>();
        private static Stopwatch _Stopwatch = new Stopwatch();
        private static int _SearchDepth = 1;
        private static Queue<(int First, int Second)> _WorkQueue = new Queue<(int First, int Second)>();

        private static int GenerateNewPrimes(int numPrimes)
        {
            _NewPrimes.Clear();

            int threadCount = 8;

            double lowerBound = _KnownPrimes.Count * Math.Log(_KnownPrimes.Count);
            double upperBound = numPrimes * Math.Log(numPrimes);
            int numPrimesNeeded = (int)(upperBound - lowerBound) / 2 / 3;

            int numPrimesNeeded = numPrimes - _KnownPrimes.Count;

            for (int i = 0; i < numPrimesNeeded; i++)
            {
                var pair = NextPrimeCompositePair();
                _WorkQueue.Enqueue(pair);
            }

            var threads = new Thread[threadCount];
            for (int i = 0; i < threadCount - 1; i++)
            {
                threads[i] = new Thread(() =>
                {
                    while (_WorkQueue.TryDequeue(out (int First, int Second) pair))
                    {
                        if (IsPrime(pair.First))
                        {
                            _NewPrimes.Enqueue(pair.First);
                        }
                        if (IsPrime(pair.Second))
                        {
                            _NewPrimes.Enqueue(pair.Second);
                        }
                    }
                });
                threads[i].Start();
            }

            for (var i = 0; i < threadCount - 1; i++)
                threads[i].Join();

            while (numPrimesNeeded > _NewPrimes.Count)
            {
                var pair = NextPrimeCompositePair();

                if (IsPrime(pair.First))
                {
                    _NewPrimes.Enqueue(pair.First);
                }
                if (IsPrime(pair.Second))
                {
                    _NewPrimes.Enqueue(pair.Second);
                }
            }

            var newPrimes = _NewPrimes.ToList();
            newPrimes.Sort();
            _KnownPrimes.AddRange(newPrimes);

            return _NewPrimes.Count;
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
            Console.WriteLine($"Nth Prime (N = {numPrimes}): {_KnownPrimes.ToArray().GetValue(numPrimes - 1)}");
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
