using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    class ClearLayer : Button
    {
        private static char[] mFillLine = new char[Console.BufferWidth];
        private static string _text = "";
        private static string[] text =  new string[Console.BufferHeight];
        private static int _posY = 0;
        private static int _posX = 0;

        private static ConsoleColor _front = Resources.MainResources.SystemColorFront;

        private static ConsoleColor _back = Resources.MainResources.SystemColorBack;
        private static bool _mDrawOnce = true;


        public ClearLayer() : base(_posY, _posX, _text, () =>{ })
        {
        }

        public override void Draw()
        {
            if (mReDeaw)
            {
                Array.Fill<char>(mFillLine, ' ');
                Array.Fill<string>(text,mFillLine.ToString());
                Console.SetCursorPosition(0,0);
                Console.ForegroundColor = _front;
                Console.BackgroundColor = _back;
                foreach (var item in text)
                {
                    Console.WriteLine(item);
                }
                if (mDrawOnce) mReDeaw = false;
            }
        }      
    }
}
