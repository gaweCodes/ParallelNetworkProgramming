using System;
using System.Threading;

namespace DiningPhilosopherSemaphore
{
    internal static class Program
    {
        public const int NumberOfPhilosophers = 5;
        public static readonly Semaphore[] Forks = new Semaphore[NumberOfPhilosophers];
        private static void Main()
        {
            for (var i = 0; i < NumberOfPhilosophers; i++)
                Forks[i] = new Semaphore(1, 1);
            
            for (var i = 0; i < NumberOfPhilosophers; i++)
            {
                var p = new Philosopher(i);
                var t = new Thread(p.PhilosopherLife) { IsBackground = false };
                t.Start();
            }
        }
    }
    internal class Philosopher
    {
        private readonly int _number;
        public Philosopher(int number)
        {
            _number = number;
        }
        public void PhilosopherLife()
        {
            var left = _number;
            var right = (_number + 1) % Program.NumberOfPhilosophers;
            var max = Math.Max(left, right);
            var min = Math.Min(left, right);
            while (true)
            {
                Thread.Sleep(new Random().Next(1000, 5000));
                Program.Forks[min].WaitOne();
                Program.Forks[max].WaitOne();
                // Program.Forks[left].WaitOne();
                // Program.Forks[right].WaitOne();
                Console.WriteLine(_number + " eats");
                // Program.Forks[left].Release();
                // Program.Forks[right].Release();
                Program.Forks[min].Release();
                Program.Forks[max].Release();
            }
        }
    }
}
