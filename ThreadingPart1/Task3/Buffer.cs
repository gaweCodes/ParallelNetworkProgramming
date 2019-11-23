using System;
using System.Threading;

namespace Task3
{
    public class Buffer
    {
        private readonly int _size;
        private readonly char[] _buf;
        private int _head, _tail, _n;
        public Buffer(int size)
        {
            _buf = new char[size];
            _size = size;
            _head = _tail = _n = 0;
        }
        public void Put(char ch)
        {
            Console.WriteLine(Thread.CurrentThread.Name + " calls Put");
            lock (this)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " access granted");
                while (_n == _size) Monitor.Wait(this);
                _buf[_tail] = ch;
                _tail = (_tail + 1) % _size;
                _n++;
                Console.WriteLine(Thread.CurrentThread.Name + " ready: n=" +_n);
                Console.WriteLine();
                Monitor.Pulse(this);
            }
        }

        public char Get()
        {
            Console.WriteLine(Thread.CurrentThread.Name + " calls Get");
            lock (this)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " access granted");
                while (_n == 0) Monitor.Wait(this);
                var ch = _buf[_head];
                _head = (_head + 1) % _size;
                _n--;
                Console.WriteLine(Thread.CurrentThread.Name + " ready: n=" + _n);
                Console.WriteLine();
                Monitor.Pulse(this);
                return ch;
            }
        }
    }
}
