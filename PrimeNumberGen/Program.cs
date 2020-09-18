using System;
using System.Collections.Generic;

namespace PrimeNumberGen {
    class Program {
        static void Main(string[] args)
        {
            while (true)
            {
                string inputString = string.Empty;
                bool canParse = false;
                int numPrimes = 0;

                Console.WriteLine("Display Info for N-th Prime: ");
                Console.WriteLine("N = ");

                while (canParse == false || numPrimes <= 1)
                {
                    Utils.ResetCursorPosition(inputString.Length, "N = ");
                    inputString = Console.ReadLine();
                    canParse = int.TryParse(inputString, out numPrimes);
                }

                Primes.DisplayNthPrimeInfo(numPrimes);
            }
        }
    }
}
