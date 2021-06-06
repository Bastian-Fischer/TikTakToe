using System;

namespace UIConsole
{
    //todo finish label
    class Label
    {
        protected Resources MainResources = Resources.MainResources;
        protected  string[] mText;
        protected bool mDrawOnce;
        protected bool mReDeaw;
        public bool ReDeaw
        {
            get { return mReDeaw; }
            set { mReDeaw = value; }
        }
        public string[] Text
        {
            get { return mText; }
            set { mText = value; }
        }
        protected int mPosY;
        protected int mPosX;
        protected ConsoleColor mColotFront;
        public ConsoleColor ColorFront
        {
            get { return mColotFront; }
            set { mColotFront = value; }
        }
        protected ConsoleColor mColorBack;
        public ConsoleColor ColorBack
        {
            get { return mColorBack; }
            set { mColorBack = value; }
        }

        public Label(string _text,int _posY, Positioning _pos,  ConsoleColor _front , ConsoleColor _back,bool _mDrawOnce = false ) {
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
            mDrawOnce = _mDrawOnce;
            mReDeaw = true;
            mPosY = _posY;       
            mColotFront = _front;
            mColorBack = _back;
            mText = new string[1];
            mText[0] = _text;
        }
        public Label(int _posY, int _posX, string _text,ConsoleColor _front , ConsoleColor _back, bool _mDrawOnce = false)
        {
            mDrawOnce = _mDrawOnce;
            mReDeaw = true;
            mPosY = _posY;
            mPosX = _posX;
            mColotFront = _front;
            mColorBack  = _back;
            mText = new string[1];
            mText[0] = _text;
        }
        public Label(string[] _bigText,int _posY, Positioning _pos,  ConsoleColor _front , ConsoleColor _back,bool _mDrawOnce = false)
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
            mDrawOnce = _mDrawOnce;
            mReDeaw = true;
            mColotFront = _front;
            mColorBack = _back;
            mPosY = _posY;
            mText = _bigText;
        }
        public Label(int _posY, int _posX, string[] _bigText, ConsoleColor _front, ConsoleColor _back, bool _mDrawOnce = false)
        {
            mDrawOnce = _mDrawOnce;
            mReDeaw = true;
            mColotFront = _front;
            mColorBack = _back;
            mPosY = _posY;
            mPosX = _posX;
            mText = _bigText;
        }
        public virtual void Draw()
        {
            if (mReDeaw) 
            { 
                int posY = mPosY;
                foreach (var item in mText)
                {
                    Console.SetCursorPosition(mPosX, posY++);
                    Console.ForegroundColor = mColotFront;
                    Console.BackgroundColor = mColorBack;
                    Console.WriteLine(item);
                }
                if (mDrawOnce) mReDeaw = false;
            }
        }
    }
}
