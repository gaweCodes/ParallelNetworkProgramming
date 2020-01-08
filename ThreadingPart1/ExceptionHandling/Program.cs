using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    internal class Program
    {
        private static void Main()
        {
            var t = Task<int>.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                throw new NotImplementedException();
            });
            try
            {
                var x = t.Result;
                Console.WriteLine(x);
            }
            catch (AggregateException ae)
            {
                ae.Flatten().Handle(e =>
                {
                    if (!(e is NotImplementedException)) return false;
                    Console.WriteLine("Exception handling");
                    Console.ReadLine();
                    return true;
                });
            }
        }
    }
}
