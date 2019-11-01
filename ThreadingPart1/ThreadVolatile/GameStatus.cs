namespace ThreadVolatile
{
    internal class GameStatus
    {
        private /*volatile*/ bool _stopFlag;
        public bool StopFlag
        {
            get => _stopFlag;
            set => _stopFlag = value;
        }
    }
}
