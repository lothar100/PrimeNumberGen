using System;
using System.Collections.Generic;

namespace PrimeNumberGen {
    class Program {
        static void Main(string[] args)
        {
            while (true)
            {
                string inputString = string.Empty;
                int numPrimes = 0;

                Console.WriteLine("Display Info for Nth Prime: ");
                Console.Write("N = ");

                while (int.TryParse(inputString, out numPrimes) == false || numPrimes <= 0)
                {
                    inputString = Console.ReadLine();
                }

                Primes.DisplayNthPrimeInfo(numPrimes);
            }
            
        }
    }
}
