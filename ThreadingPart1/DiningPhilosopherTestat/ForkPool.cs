using System.Threading;

namespace DiningPhilosopherTestat
{
    internal class ForkPool
    {
        private readonly Fork[] _forks = {new Fork(), new Fork(), new Fork(), new Fork(), new Fork()};
        private readonly object _locker = new object();
        public void GetMyForks(int left, int right)
        {
            lock (_locker)
            {
                while (_forks[left].IsUsed || _forks[right].IsUsed)
                {
                    Monitor.Wait(_locker);
                }

                _forks[left].IsUsed = true;
                _forks[right].IsUsed = true;
            }
        }
        public void PutForksBack(int left, int right)
        {
            lock (_locker)
            {
                _forks[left].IsUsed = false;
                _forks[right].IsUsed = false;
                Monitor.PulseAll(_locker);
            }
        }
    }
}
