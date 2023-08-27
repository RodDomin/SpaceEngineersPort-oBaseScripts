namespace IngameScript
{
    class Timer
    {
        private readonly int ticks;
        private int currentTick;
        private bool isRunning = false;

        public Timer(int ticks)
        {
            this.ticks = ticks;
        }

        public bool Finished()
        {
            return ticks <= currentTick;
        }

        public void Reset()
        {
            currentTick = 0;
        }

        public void Tick()
        {
            currentTick++;
        }

        public void Start()
        {
            isRunning = true;
        }

        public void Stop()
        {
            isRunning = false;
        }

        public bool IsRunning()
        {
            return isRunning;
        }
    }
}
