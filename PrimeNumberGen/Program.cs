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

                while (canParse == false || numPrimes <= 0)
                {
                    ResetCursorPosition(inputString.Length);
                    inputString = Console.ReadLine();
                    canParse = int.TryParse(inputString, out numPrimes);
                }

                Primes.DisplayNthPrimeInfo(numPrimes);
            }
        }

        private static void ResetCursorPosition(int length)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write("N = ");
            for (int i = 0; i < length; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(4, Console.CursorTop);
        }
    }
}
