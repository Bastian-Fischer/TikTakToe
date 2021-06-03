using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public FramesPerSecond(int _Y, int _X, ConsoleColor _back, ConsoleColor _front) 
        {
            mTickTime = DateTime.Now;
            mLastTickTime = mTickTime;
            y = _Y;
            x = _X;
            back = _back;
            front = _front;
            mTicksCounter = 0;
            mTicksPerSecond = 0;
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
                Console.Write(mTicksPerSecond + " FpS");
                Console.ResetColor();
                mLastTickTime = mTickTime;
            }
        }
    }
}
