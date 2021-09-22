using System;
using System.Diagnostics;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            BiseyYap();
            Console.WriteLine(Convert.ToInt32(stopwatch.Elapsed.TotalSeconds));
        }

        static void BiseyYap()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true)
            {
                if (stopwatch.Elapsed.TotalSeconds == 5)
                {
                    Console.WriteLine("İşlem tamamlandı");
                    return;
                }
            }
        }
    }
}