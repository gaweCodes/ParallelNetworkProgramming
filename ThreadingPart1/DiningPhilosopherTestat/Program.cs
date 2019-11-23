namespace DiningPhilosopherTestat
{
    internal class Program
    {
        private static void Main()
        {
            new Philosopher(0, 100, 500).Start();
            new Philosopher(1, 200, 400).Start();
            new Philosopher(2, 300, 300).Start();
            new Philosopher(3, 400, 200).Start();
            new Philosopher(4, 500, 100).Start();
        }
    }
}
