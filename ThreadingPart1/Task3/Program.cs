using System;
using System.Threading;

namespace Task3
{
    internal class Program
    {
        [ThreadStatic] private static readonly Buffer Buffer;
        private static void Main()
        {

            var writer = new Thread(PutBuffer);
            writer.Start();
            var reader = new Thread(ReadBuffer);
            reader.Start();
        }

        private static void PutBuffer()
        {
            var r = new Random();
            do
            {
                Buffer.Put(Console.ReadLine()[0]);
                Thread.Sleep(r.Next(1, 5000));
            } while (1 == 1);
        }
        private static void ReadBuffer()
        {
            var r = new Random();
            do
            {
                Console.Write(Buffer.Get());
                Thread.Sleep(r.Next(1, 5000));
            } while (1 == 1);
        }
    }
}
