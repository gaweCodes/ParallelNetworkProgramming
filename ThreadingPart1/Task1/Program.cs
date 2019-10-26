using System;
using System.Threading;

namespace Task1
{
    internal class Program
    {
        private static char _ch = '*';

        private static void Main()
        {
            var printer = new Thread(Print);
            var reader = new Thread(Read);
            printer.Start();
            reader.Start();
            Console.WriteLine("Terminate the program with Ctrl-C");
        }

        private static void Print()
        {
            while (true)
            {
                Console.Write(_ch); 
                Thread.Sleep(10);
            } 
        }

        private static void Read()
        {
            while (true)
            {
                var enteredValue = Console.ReadLine();
                if (!string.IsNullOrEmpty(enteredValue)) _ch = enteredValue[0];
            }
        }
    }
}
