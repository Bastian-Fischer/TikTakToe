using System;

namespace UIConsole
{
    //todo: finish button
    //todo: either sealed button or allow inheritance and make private protected
    class Button : Label
    {        
        protected ConsoleColor mColorSelected;

        protected Action mCommand;
        protected bool isSelected; 
        public bool IsSelected{
            get { return isSelected; }
            set { isSelected = value; }     
        }

        public Button(
            int _posY,
            int _posX,
            string _text,
            Action _mCommand
            ) : base(_posY, _posX, _text, Resources.MainResources.MenuColorFront, Resources.MainResources.MenuColorBack)
        {
            mColorSelected = MainResources.MenuColorActive;
            mCommand = _mCommand;
        }
        public Button(
            int _posY,
            Positioning _pos,
            string _text,
            Action _mCommand
            ) : base(_text,_posY, _pos,  Resources.MainResources.MenuColorFront, Resources.MainResources.MenuColorBack)
        {
            mColorSelected = MainResources.MenuColorActive;
            mCommand = _mCommand;
        }

        public Button(
            int _posY,
            int _posX,
            string[] _text,
            Action _mCommand
            ) : base(_posY, _posX, _text, Resources.MainResources.MenuColorFront, Resources.MainResources.MenuColorBack)
        {
            mColorSelected = MainResources.MenuColorActive;
            mCommand = _mCommand;
        }
        public Button(
            int _posY,
            Positioning _pos,
            string[] _text,
            Action _mCommand
            ) : base(_text,_posY, _pos,  Resources.MainResources.MenuColorFront, Resources.MainResources.MenuColorBack)
        {
            mColorSelected = MainResources.MenuColorActive;
            mCommand = _mCommand;
        }
        public void Execute()
        {
            mCommand();
        }

        public override void Draw()
        {

            Console.SetCursorPosition(mPosX, mPosY);
            Console.BackgroundColor = IsSelected ? mColorSelected : mColorBack;
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
