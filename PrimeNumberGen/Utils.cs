using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeNumberGen {
    public static class Utils {

        public static void ResetCursorPosition(int length, string displayText)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(displayText);
            for (int i = 0; i < length; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(displayText.Length, Console.CursorTop);
        }

    }
}
