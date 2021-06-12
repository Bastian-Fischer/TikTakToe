using System;

namespace UIConsole
{
    class FramesPerSecond
    {
        private DateTime mTickTime;
        private DateTime mLastTickTime;
        private long mTicksCounter;
        private long mTicksPerSecond;
        private readonly int y;
        private readonly int x;
        private readonly ConsoleColor back;
        private readonly ConsoleColor front;
        private DateTime start;
        private readonly string name;
        public FramesPerSecond(int _Y, int _X, ConsoleColor _back, ConsoleColor _front, string _name= "") 
        {
            mTickTime = DateTime.Now;
            mLastTickTime = mTickTime;
            y = _Y;
            x = _X;
            back = _back;
            front = _front;
            mTicksCounter = 0;
            mTicksPerSecond = 0;
            name = _name;
            if (name != "") name += ": ";
        }
        public void ShowFramesPerSecond()
        {
            mTickTime = DateTime.Now;
            int timeTickDifer = (mTickTime.Second * 1000 + mTickTime.Millisecond) - (mLastTickTime.Second * 1000 + mLastTickTime.Millisecond);
            mTicksCounter++;
            if (timeTickDifer >= 1000)
            {
                mTicksPerSecond = mTicksCounter;
                mTicksCounter = 0;
                Console.ForegroundColor = front;
                Console.BackgroundColor = back;
                Console.SetCursorPosition(x, y);
                Console.Write(name+mTicksPerSecond + " FpS");
                Console.ResetColor();
                mLastTickTime = mTickTime;
            }
        }
        public void HowLongStart()
        {       
            start = DateTime.Now;
        }
        public void HowLongEnd() {
            DateTime now = DateTime.Now;
            int timeTickDifer = (now.Second * 1000 + now.Millisecond) - (start.Second * 1000 + start.Millisecond);
            Console.ForegroundColor = front;
            Console.BackgroundColor = back;
            Console.SetCursorPosition(x, y);
            Console.Write("{0} {1,5} Milliseconds",name,timeTickDifer);
            Console.ResetColor();
        }
    }
}
