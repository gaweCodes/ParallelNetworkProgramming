namespace DiningPhilosopherTestat
{
    internal class Program
    {
        private static void Main()
        {
            var pool = new ForkPool();
            new Philosopher(0, 100, 500, pool).Start();
            new Philosopher(1, 200, 400, pool).Start();
            new Philosopher(2, 300, 300, pool).Start();
            new Philosopher(3, 400, 200, pool).Start();
            new Philosopher(4, 500, 100, pool).Start();
        }
    }
}
