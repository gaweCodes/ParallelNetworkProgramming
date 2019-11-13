namespace Task3
{
    public class Buffer
    {
        private const int Size = 4;
        private readonly char[] _buf = new char[Size];
        private int _head, _tail;

        public void Put(char ch)
        {
            _buf[_tail] = ch;
            _tail = (_tail + 1) % Size;
        }

        public char Get()
        {
            var ch = _buf[_head];
            _head = (_head + 1) % Size;
            return ch;
        }
    }
}
