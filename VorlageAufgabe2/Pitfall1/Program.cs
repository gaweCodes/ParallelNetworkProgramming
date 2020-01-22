using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Pitfall1 
{
    internal class Program 
    {
        // What is the difference? Is there a problem with my way?
        private static void Main()
        {
            var sw1 = new Stopwatch();
            sw1.Start();
            var task = IsPrimeAsync(10000000000000061L);
            Console.WriteLine("Other work");
            Console.WriteLine($"Result {task.Result}");
            sw1.Stop();

            var sw2 = new Stopwatch();
            sw2.Start();
            var task1 = IsPrimeTask(10000000000000061L);
            Console.WriteLine("Other work");
            Console.WriteLine($"Result {task1.Result}");
            sw2.Stop();
            Console.WriteLine($"Solution way duration: {sw1.ElapsedMilliseconds}");
            Console.WriteLine($"My way duration: {sw2.ElapsedMilliseconds}");
            Console.ReadLine();
        }
        // My Way
        private static Task<bool> IsPrimeTask(long number)
        {
            return Task.Run(() =>
            {
                for (long i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0) return false;
                }

                return true;
            });
        }
        // Solution Way 1
        private static async Task<bool> IsPrimeAsync(long number)
        {
            return await Task.Run(() =>
            {
                for (long i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0)  return false;
                }

                return true;
            });
        }
    }
}
