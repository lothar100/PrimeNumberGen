using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PrimeNumberGen {
    public static class ConsoleSpinner {

        private static Thread _SpinThread = new Thread(() => Run(CalcText, _TokenSource.Token));
        private static CancellationTokenSource _TokenSource = new CancellationTokenSource();
        private static string CalcText => "Calculating... ";

        public static void Start()
        {
            _SpinThread.Start();
        }

        public static void Stop()
        {
            _TokenSource.Cancel();
            Thread.Sleep(200);
            _TokenSource = new CancellationTokenSource();
            _SpinThread = new Thread(() => Run(CalcText, _TokenSource.Token));
            Console.WriteLine("");
            Utils.ResetCursorPosition(CalcText.Length + 2, string.Empty);
        }

        private static void Run(string displayText, CancellationToken cancelToken)
        {
            Console.Write(displayText);
            int counter = 0;
            while (cancelToken.IsCancellationRequested == false)
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); counter = 0; break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Thread.Sleep(100);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }

    }
}
