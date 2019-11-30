using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace ThreadPoolDemo
{
    internal delegate List<int> Primes(int max);
    internal class Program
    {
        private static void Main()
        {
            var primesCalculatorDel = new Primes(FindPrimes);
            primesCalculatorDel.BeginInvoke(200000000, FindPrimesCallback, primesCalculatorDel);
            // Func<int, List<int>> ftn = FindPrimes;
            // ftn.BeginInvoke(200000000, FindPrimesCallback, ftn);
            for (var i = 0; i < 10; i++)
            {
                Console.Write("1");
                Thread.Sleep(1000);
            }
        }
        private static List<int> FindPrimes(int max)
        {
            var vals = new List<int>((int) (max / (Math.Log(max) - 1.08366))) {2};
            var maxSquareRoot = Math.Sqrt(max);
            var eliminated = new BitArray(max + 1);
            
            for (var i = 3; i <= max; i += 2)
            {
                if (eliminated[i]) continue;
                if (i < maxSquareRoot)
                {
                    for (var j = i * i; j <= max; j += 2 * i)
                        eliminated[j] = true;
                }
                vals.Add(i);
            }
            return vals;
        }
        private static void FindPrimesCallback(IAsyncResult asyncResult)
        {
            var result = ((Primes)asyncResult.AsyncState).EndInvoke(asyncResult);
            Console.WriteLine();
            Console.WriteLine(result[result.Count - 1]);
        }
    }
}
