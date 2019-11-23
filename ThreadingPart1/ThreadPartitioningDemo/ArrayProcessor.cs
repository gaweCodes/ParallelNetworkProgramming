using System.Threading;

namespace ThreadPartitioningDemo
{
    public class ArrayProcessor
    {
        public double Sum { get; private set; }
        private readonly double[] _data;
        private readonly int _firstIndex;
        private readonly int _lastIndex;

        public ArrayProcessor(double[] data, int firstIndex, int lastIndex)
        {
            _data = data;
            _firstIndex = firstIndex;
            _lastIndex = lastIndex;
        }

        public void ComputeSum()
        {
            Sum = 0;

            for (var n = _firstIndex; n <= _lastIndex; n++)
            {
                Sum += _data[n];
                Thread.Sleep(1);
            }
        }
    }
}
