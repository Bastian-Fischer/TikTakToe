using System;

namespace UIConsole
{
    //todo: finish button
    class Button : Label
    {
        private ConsoleColor mColorSelected;
        private ConsoleColor mColorNotSelected;
        private Action mCommand;
        private bool isSelected; 
        public bool IsSelected{
            get { return isSelected; }
            set { isSelected = value; }     
        }

        public Button(
            int _posY,
            int _posX,
            string _text,
            Action _mCommand,
            ConsoleColor _mColorSelected = ConsoleColor.DarkBlue,
            ConsoleColor _mColorNotSelected = ConsoleColor.Black,
            ConsoleColor _back = ConsoleColor.Black
            ) : base(_posY, _posX, _text, _mColorNotSelected, _back)
        {
            mColorNotSelected = _mColorNotSelected;
            mColorSelected = _mColorSelected;
            mCommand = _mCommand;
        }
        public Button(
            int _posY,
            Positioning _pos,
            string _text,
            Action _mCommand,
            ConsoleColor _mColorSelected = ConsoleColor.DarkBlue,
            ConsoleColor _mColorNotSelected = ConsoleColor.Black,
            ConsoleColor _back = ConsoleColor.Black
            ) : base(_posY, _pos, _text, _mColorNotSelected, _back)
        {
            mColorNotSelected = _mColorNotSelected;
            mColorSelected = _mColorSelected;
            mCommand = _mCommand;
        }
        public void Execute()
        {
            mCommand();
        }

        public override void Draw()
        {

            Console.SetCursorPosition(mPosX, mPosY);
            Console.BackgroundColor = IsSelected ? mColorSelected : mColorNotSelected;
            int posY = mPosY;
            foreach (var item in mText)
            {         
                Console.SetCursorPosition(mPosX, posY++);
                Console.WriteLine(item);  
            }
            Console.ResetColor();
        }


    }
}
