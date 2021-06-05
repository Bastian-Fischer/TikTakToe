using System;

namespace UIConsole
{
    //todo finish label
    class Label
    {
        protected Resources MainResources = Resources.MainResources;
        protected readonly string[] mText;
        protected readonly int mPosY;
        protected readonly int mPosX;
        protected readonly ConsoleColor front;
        protected readonly ConsoleColor back;

        public Label(int _posY, Positioning _pos, string _text, ConsoleColor _front , ConsoleColor _back ) {
            int bufferWidth = Console.BufferWidth;
            switch (_pos)
            {
                case Positioning.center:
                    mPosX = (bufferWidth - _text.Length)/2;
                    break;
                case Positioning.left:
                    mPosX =  0;
                    break;
                case Positioning.right:
                    mPosX = bufferWidth - _text.Length;
                    break;
            }
            
            mPosY = _posY;       
            front = _front;
            back = _back;
            mText = new string[1];
            mText[0] = _text;
        }
        public Label(int _posY, int _posX, string _text,ConsoleColor _front , ConsoleColor _back ) 
        {
            mPosY = _posY;
            mPosX = _posX;
            front = _front;
            back  = _back;
            mText = new string[1];
            mText[0] = _text;
        }
        public Label(int _posY, Positioning _pos, string[] _bigText, ConsoleColor _front , ConsoleColor _back)
        {
            int bufferWidth = Console.BufferWidth;
            switch (_pos)
            {
                case Positioning.center:
                    mPosX = (bufferWidth - _bigText[0].Length) / 2;
                    break;
                case Positioning.left:
                    mPosX = 0;
                    break;
                case Positioning.right:
                    mPosX = bufferWidth - _bigText[0].Length;
                    break;
            }
            front = _front;
            back = _back;
            mPosY = _posY;
            mText = _bigText;
        }
        public Label(int _posY, int _posX, string[] _bigText, ConsoleColor _front, ConsoleColor _back)
        {
            front = _front;
            back = _back;
            mPosY = _posY;
            mPosX = _posX;
            mText = _bigText;
        }
        public virtual void Draw()
        {
            int posY = mPosY;
            foreach (var item in mText)
            {
                Console.SetCursorPosition(mPosX, posY++);
                Console.ForegroundColor = front;
                Console.BackgroundColor = back;
                Console.WriteLine(item);
                Console.ResetColor(); //todo versuchen auszubauen. jedes element stellt immer seine farben vor dem zeichnen ein
            }
           
        }
    }
}
