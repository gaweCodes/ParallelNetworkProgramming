using System;
using System.Threading;

namespace Example
{
    internal class Program
    {
        private static void Main()
        {
            var t = new Thread(SayHello) {Name = "Say Hello Thread", Priority = ThreadPriority.Lowest};
            t.Start();
            Console.WriteLine("Bye");
        }
        private static void SayHello()
        {
            Console.WriteLine("Hello");
            Console.ReadKey();
        }
    }
}
