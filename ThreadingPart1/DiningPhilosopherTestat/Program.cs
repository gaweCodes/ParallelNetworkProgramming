namespace DiningPhilosopherTestat
{
    internal class Program
    {
        private static void Main()
        {
            var pool = new ForkPool();
            new Philosopher(0, 100, 500, pool);
            new Philosopher(1, 200, 400, pool);
            new Philosopher(2, 300, 300, pool);
            new Philosopher(3, 400, 200, pool);
            new Philosopher(4, 500, 100, pool);
        }
    }
}
