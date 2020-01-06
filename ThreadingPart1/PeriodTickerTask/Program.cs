using System;
using System.Threading;
using System.Threading.Tasks;

namespace PeriodTickerTask
{
    internal class Program
    {
        private static void Main()
        {
            var t1 = new Task(() => { PeriodTicker('.', 10); });
            var t2 = t1.ContinueWith(delegate { PeriodTicker('"', 10); });
            t1.Start();
            t2.Wait();
            Console.WriteLine("\nEnde");
            Parallel.Invoke(() => PeriodTicker('.',10), () => PeriodTicker('*', 20));
            Console.ReadKey();
            Console.WriteLine("Ende 2");
        }

        private static void PeriodTicker(char sign, int intervallMillis)
        {

            for (var i = 0; i < 100; i++)
            {
                Console.Write(sign);
                Thread.Sleep(intervallMillis);
            }
        }
    }
}
