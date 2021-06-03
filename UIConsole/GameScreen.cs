using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    class GameScreen : Scene
    {
        private TTTLogic.Logic mGameLogic;

        //UI
        //O
        private ConsoleColor mPlayerAColor; 
        private ConsoleColor mPlayerABackColor;

        //X
        private ConsoleColor mPlayerBColor;
        private ConsoleColor mPlayerBBackColor;

        private ConsoleColor mBorderBackColor;
        private ConsoleColor mBorderColor;

        private ConsoleColor mBoardColor;
        private ConsoleColor mBoardBackColor;
        private int mFieldStartPosY;
        private int mFieldStartPosX;
        private int mFieldSizeY;
        private int mFieldSizeX;
        private int mGameSizeY;
        private int mGameSizeX;
                       
        private char[,] mFieldX = LoadAs.CharMultidimensionalArr("TTTIMG/FieldB.txt");
        private char[,] mFieldO = LoadAs.CharMultidimensionalArr("TTTIMG/FieldA.txt");   
        private char[,] mFieldE = LoadAs.CharMultidimensionalArr("TTTIMG/FieldE.txt");
        private char[] mBoarderList = LoadAs.CharArrFirstOneFromLine("TTTIMG/Boarder.txt");

        private char mBoarderLT;//'┌'
        private char mBoarderTC;//'┬'
        private char mBaorderTR;//'┐'
        private char mBoarderRC;//'┤'
        private char mBoarderRB;//'┘'
        private char mBoarderBC;//'┴'
        private char mBoarderBL;//'└'
        private char mBoarderLC;//'├'
        private char mBoarderCR;//'┼'
        private char mBoarderVE;//'─'
        private char mBoarderHO;//'│'

        private ConsoleColor mCurrentBackgroundColor;
        private int currentY;
        private int currentX;
        public GameScreen(
            int _startY,
            ConsoleColor _mPlayerAColor = ConsoleColor.Blue,
            ConsoleColor _mPlayerABackColor = ConsoleColor.DarkBlue,       
            ConsoleColor _mPlayerBColor = ConsoleColor.Red,
            ConsoleColor _mPlayerBBackColor = ConsoleColor.DarkRed,
            ConsoleColor _mBorderColor = ConsoleColor.White,
            ConsoleColor _mBorderBackColor = ConsoleColor.DarkGray,
            ConsoleColor _mBoardColor = ConsoleColor.White,
            ConsoleColor _mBoardBackColor = ConsoleColor.Gray,
            ConsoleColor _mCurrentBackgroundColor = ConsoleColor.White
            ) 
        {
            mBoarderLT = mBoarderList[0];//'┌
            mBoarderTC = mBoarderList[1];//'┬
            mBaorderTR = mBoarderList[2];//'┐
            mBoarderRC = mBoarderList[3];//'┤
            mBoarderRB = mBoarderList[4];//'┘
            mBoarderBC = mBoarderList[5];//'┴
            mBoarderBL = mBoarderList[6];//'└
            mBoarderLC = mBoarderList[7];//'├
            mBoarderCR = mBoarderList[8];//'┼
            mBoarderVE = mBoarderList[9];//'─
            mBoarderHO = mBoarderList[10];//'│

            currentY = 2;
            currentX = 2;

            mGameSizeY = 3;
            mGameSizeX = 3;
            

            mPlayerAColor = _mPlayerAColor;
            mPlayerABackColor = _mPlayerABackColor;

            mPlayerBColor = _mPlayerBColor;
            mPlayerBBackColor = _mPlayerBBackColor;

            mBorderColor = _mBorderColor;
            mBorderBackColor = _mBorderBackColor;

            mBoardColor = _mBoardColor;
            mBoardBackColor = _mBoardBackColor;

            mCurrentBackgroundColor = _mCurrentBackgroundColor;

            mFieldSizeY = mFieldX.GetLength(0);
            mFieldSizeX = mFieldX.GetLength(1);
            mFieldStartPosY = _startY;
            mFieldStartPosX =  Console.BufferWidth /2 - (2+(mGameSizeX * mFieldE.GetLength(1)+1)/2);


            mGameLogic = new();
            StartGame();
        }
        private void StartGame() 
        {
            mGameLogic = new();
            DrawBoarder();           
        }
        private void DrawOneField(char[,] _field, int _startY, int _startX, ConsoleColor _forground, ConsoleColor _background) 
        {
            Console.ForegroundColor = _forground;
            Console.BackgroundColor = _background;
            string buffer = "";
            
            for (int countY = 0; countY < _field.GetLength(0) ; countY++) 
            {
                for (int countX = 0; countX < _field.GetLength(1) ; countX++)
                {
                    buffer += (_field[countY, countX]);
                }
                Console.SetCursorPosition(_startX, countY + _startY);
                Console.Write(buffer);
                buffer = "";
            }
            Console.ResetColor();
        }
        private void DrawField() 
        {
            TTTLogic.Board[,] GameBoard = mGameLogic.GetGameBoard();
            int startY;
            int startX;

            mCurrentBackgroundColor = mGameLogic.GetCurrentPlayer()? mPlayerBBackColor : mPlayerABackColor;

            for (int countFY = 0; countFY < mGameSizeY; countFY++)
            {
                startY = mFieldStartPosY +(countFY * mFieldSizeY) + countFY + 2;

                for (int countFX = 0; countFX < mGameSizeX; countFX++)
                {
                    startX = mFieldStartPosX +(countFX * mFieldSizeX) + countFX + 1;
                    if (GameBoard[countFY, countFX] == TTTLogic.Board.Empty)
                    {
                        
                        DrawOneField(
                            mFieldE,
                            startY,
                            startX,
                            mBoardColor,
                            (countFY+1 == currentY && countFX+1 == currentX) ? mCurrentBackgroundColor : mBoardBackColor
                        );

                    }
                    else if(GameBoard[countFY, countFX] == TTTLogic.Board.O)
                    {
                        DrawOneField(
                                mFieldO,
                                startY,
                                startX,
                                mPlayerAColor,
                                (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentBackgroundColor : mBoardBackColor
                            );

                    }
                    else if (GameBoard[countFY, countFX] == TTTLogic.Board.X)
                    {
                        DrawOneField(
                                mFieldX,
                                startY,
                                startX,
                                mPlayerBColor,
                                (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentBackgroundColor : mBoardBackColor
                            );
                    }       
                }
            }
            

        
        }
        public void DrawBoarder()
        {
            Console.Clear();
            //Erstmal nur für 3x3

            string spacerLine = ""+mBoarderHO;
            for (int countF = 0; countF < mGameSizeX; countF++) 
            {
                for (int countFSX = 0; countFSX < mFieldSizeX; countFSX++)
                {
                    spacerLine += ' ';
                }
                spacerLine +=  mBoarderHO;
            }
            Console.ForegroundColor = mBorderColor;
            Console.BackgroundColor = mBorderBackColor;
            string lineBuffer = "";
            int stepperY = 0;
            for (int counterY = 0; counterY <= mGameSizeY; counterY++)
            {

                for (int counterX = 0; counterX < mGameSizeX; counterX++)
                {
                    if (counterY == 0)
                    { 
                        if (counterX == 0) lineBuffer += mBoarderLT;//┌
                        if (counterX > 0 && counterX != mGameSizeX) lineBuffer += mBoarderTC; //┬
                        for (int count = 0; count < mFieldSizeX; count++)
                        {
                            lineBuffer += mBoarderVE;// Feldbreite entlang ─
                        }
                        if (counterX == mGameSizeX - 1) lineBuffer += mBaorderTR;// am ende ┐
                    }
                    if (counterY > 0 && counterY < mGameSizeY)
                    {
                        if (counterX == 0) lineBuffer += mBoarderLC;//├
                        if (counterX > 0 && counterX != mGameSizeX ) lineBuffer += mBoarderCR; //┼
                        for (int count = 0; count < mFieldSizeX; count++)
                        {
                            lineBuffer += mBoarderVE;// Feldbreite entlang ─
                        }
                        if (counterX == mGameSizeX - 1) lineBuffer += mBoarderRC;// am ende ┤
                    }
                    if (counterY == mGameSizeY) 
                    {
                        if (counterX == 0) lineBuffer += mBoarderBL;//└
                        if (counterX > 0 && counterX != mGameSizeX) lineBuffer += mBoarderBC; //┴
                        for (int count = 0; count < mFieldSizeX; count++)
                        {
                            lineBuffer += mBoarderVE;// Feldbreite entlang ─
                        }
                        if (counterX == mGameSizeX - 1) lineBuffer += mBoarderRB;// am ende ┘
                    }

                }
                Console.SetCursorPosition(mFieldStartPosX, ++stepperY + mFieldStartPosY);
                Console.Write(lineBuffer);
                lineBuffer = "";
                if (counterY < mGameSizeY)
                { 
                    for (int countFSY = 0; countFSY < mFieldSizeY; countFSY++)
                    {
                        Console.SetCursorPosition(mFieldStartPosX, ++stepperY + mFieldStartPosY);
                        Console.Write(spacerLine);
                    }
                }
            }
            Console.ResetColor();
        }
        private void CheckInteraktion() 
        { 
        
        
        }
        public override void Update()
        {
            DrawField();
            CheckInteraktion();
            if (mGameLogic.GameIsFinished())
            {
                SceneManager.Instance.AddScene(new GameOver(mGameLogic,this));
            }
            else 
            {
                
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentY > 1) currentY--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentY < mGameSizeY) currentY++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (currentX > 1) currentX--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentX < mGameSizeX) currentX++;
                        break;

                    case ConsoleKey.Enter:
                        mGameLogic.PlayerTurn(currentX-1, currentY-1);
                        break;
                }
            }
        }
    }
}
