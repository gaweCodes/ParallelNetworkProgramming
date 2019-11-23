using System.Threading;

namespace DiningPhilosopherTestat
{
    internal class ForkPool
    {
        private readonly Fork[] _forks = {new Fork(), new Fork(), new Fork(), new Fork(), new Fork()};
        public void GetMyForks(int left, int right)
        {
            lock (this)
            {
                while (_forks[left].IsUsed || _forks[right].IsUsed) Monitor.Wait(this);
                _forks[left].IsUsed = true;
                _forks[right].IsUsed = true;
            }
        }
        public void PutForksBack(int left, int right)
        {
            lock (this)
            {
                _forks[left].IsUsed = false;
                _forks[right].IsUsed = false;
                Monitor.PulseAll(this);
            }
        }
    }
}
