using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadPartitioningDemo
{
    internal class Program
    {
#if true
        private static void Main()
        {
            var coreCount = Environment.ProcessorCount;
            Console.WriteLine($"Process/core count = {coreCount}");

            var data = GetData();
            var sw = Stopwatch.StartNew();
            var wholeArray = new ArrayProcessor(data, 0, data.Length - 1);
            wholeArray.ComputeSum();
            sw.Stop();

            Console.WriteLine($"1 thread computed {wholeArray.Sum:n0} in {sw.ElapsedMilliseconds:n0} ms");
        }
#else
        private static void Main() 
        {
            var coreCount = Environment.ProcessorCount;
            Console.WriteLine($"Process/core count = {coreCount}");

            var data = GetData();
            var sw = Stopwatch.StartNew();

            var slices = new ArrayProcessor[coreCount];
            var threads = new Thread[coreCount];

            var indexesPerThread = data.Length / coreCount;
            var leftOverIndexes = data.Length % coreCount;

            for (var n = 0; n < coreCount; n++) 
            {
                var firstIndex = (n * indexesPerThread);
                var lastIndex = firstIndex + indexesPerThread - 1;

                if (n == coreCount - 1) lastIndex += leftOverIndexes;
                
                var slice = new ArrayProcessor(data, firstIndex, lastIndex);
                slices[n] = slice;

                threads[n] = new Thread(slice.ComputeSum);
                threads[n].Start();
            }

            double sum = 0;

            for (var n = 0; n < coreCount; n++) 
            {
                threads[n].Join();
                sum += slices[n].Sum;
            }
            sw.Stop();

            Console.WriteLine($"{coreCount} threads computed {sum:n0} in {sw.ElapsedMilliseconds:n0} ms");
        }
#endif
        private static double[] GetData()
        {
            var data = new double[5000];
            for (var n = 0; n < data.Length; n++) data[n] = n;
            return data;
        }
    }
}