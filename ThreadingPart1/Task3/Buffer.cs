using System;
using System.Threading;

namespace Task3
{
    public class Buffer
    {
        private readonly int _size;
        private readonly char[] _buf;
        private int _head, _tail, _n;
        private readonly object _lockerObj = new object();
        public Buffer(int size)
        {
            _buf = new char[size];
            _size = size;
        }
        public void Put(char ch)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} calls Put");
            lock (_lockerObj)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} access granted");
                while (_n == _size) Monitor.Wait(_lockerObj);
                _buf[_tail] = ch;
                _tail = (_tail + 1) % _size;
                _n++;
                Console.WriteLine($"{Thread.CurrentThread.Name} ready: n = {_n}");
                Monitor.Pulse(_lockerObj);
            }
        }

        public char Get()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} calls Get");
            lock (_lockerObj)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} ccess granted");
                while (_n == 0) Monitor.Wait(_lockerObj);
                var ch = _buf[_head];
                _head = (_head + 1) % _size;
                _n--;
                Console.WriteLine($"{Thread.CurrentThread.Name} ready: n={_n}");
                Console.WriteLine();
                Monitor.Pulse(_lockerObj);
                return ch;
            }
        }
    }
}
