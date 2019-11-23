using System;
using System.Threading;

namespace DiningPhilosopherTestat
{
    internal class Philosopher
    {
        private readonly int _philosopherNo;
        private readonly int _thinkDuration;
        private readonly int _eatDuration;
        private readonly int _leftForkNo;
        private readonly int _rightForkNo;
        private readonly ForkPool _forkPool;
        private readonly Thread _philosopherThread;
        internal Philosopher(int id, int thinkDuration, int eatDuration)
        {
            _philosopherNo = id;
            _thinkDuration = thinkDuration;
            _eatDuration = eatDuration;
            _forkPool = new ForkPool();
            _leftForkNo = id == 0 ? 4 : id - 1;
            _rightForkNo = (id + 1) % 5;
            _philosopherThread = new Thread(this.Run);
        }

        public void Start() => _philosopherThread.Start();
        private void Run()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(_thinkDuration);
                    _forkPool.GetMyForks(_leftForkNo, _rightForkNo);
                    Console.WriteLine("Philosopher " + _philosopherNo + " is eating...");
                    Thread.Sleep(_eatDuration);
                    _forkPool.PutForksBack(_leftForkNo, _rightForkNo);
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
