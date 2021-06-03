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

        private readonly int mFieldStartPosY;
        private readonly int mFieldStartPosX;
        private readonly int mFieldSizeY;
        private readonly int mFieldSizeX;
        private readonly int mGameSizeY;
        private readonly int mGameSizeX;                      
        private ConsoleColor mCurrentCursorColor;
        private int currentY;
        private int currentX;

        public GameScreen(int _startY) 
        {
            mGameLogic = new();
            currentY = 2;
            currentX = 2;
            mGameSizeY = 3;
            mGameSizeX = 3;        

            mFieldSizeY = Resources.FieldX.GetLength(0);
            mFieldSizeX = Resources.FieldX.GetLength(1);
            mFieldStartPosY = _startY;
            mFieldStartPosX =  Console.BufferWidth /2 - (2+(mGameSizeX * Resources.FieldE.GetLength(1)+1)/2);

            StartGame();
        }
        private void StartGame() 
        {
            mGameLogic = new();
            DrawBoarder();           
        }
        //TODO  alles was mit dem Feld zu schaffen hat, in eine Klasse auslagern.
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

            mCurrentCursorColor = mGameLogic.GetCurrentPlayer()? Resources.PlayerBColorBack : Resources.PlayerAColorBack;

            for (int countFY = 0; countFY < mGameSizeY; countFY++)
            {
                startY = mFieldStartPosY +(countFY * mFieldSizeY) + countFY + 2;

                for (int countFX = 0; countFX < mGameSizeX; countFX++)
                {
                    startX = mFieldStartPosX +(countFX * mFieldSizeX) + countFX + 1;
                    if (GameBoard[countFY, countFX] == TTTLogic.Board.Empty)
                    {
                        
                        DrawOneField(
                            Resources.FieldE,
                            startY,
                            startX,
                            Resources.BoardColorFront,
                            (countFY+1 == currentY && countFX+1 == currentX) ? mCurrentCursorColor : Resources.BoardColorBack
                        );

                    }
                    else if(GameBoard[countFY, countFX] == TTTLogic.Board.O)
                    {
                        DrawOneField(
                                Resources.FieldO,
                                startY,
                                startX,
                                Resources.PlayerAColorFront,
                                (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentCursorColor : Resources.PlayerAColorBack
                            );

                    }
                    else if (GameBoard[countFY, countFX] == TTTLogic.Board.X)
                    {
                        DrawOneField(
                                Resources.FieldX,
                                startY,
                                startX,
                                Resources.PlayerBColorFront,
                                (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentCursorColor : Resources.PlayerBColorBack
                            );
                    }       
                }
            }     
        }
        public void DrawBoarder()
        {
            Console.Clear();
            //Erstmal nur für 3x3

            string spacerLine = ""+ Resources.boarderHO;
            for (int countF = 0; countF < mGameSizeX; countF++) 
            {
                for (int countFSX = 0; countFSX < mFieldSizeX; countFSX++)
                {
                    spacerLine += ' ';
                }
                spacerLine += Resources.boarderHO;
            }
            Console.ForegroundColor = Resources.BoardColorFront;
            Console.BackgroundColor = Resources.BoardColorBack;
            string lineBuffer = "";
            int stepperY = 0;
            for (int counterY = 0; counterY <= mGameSizeY; counterY++)
            {
                for (int counterX = 0; counterX < mGameSizeX; counterX++)
                {
                    if (counterY == 0)
                    { 
                        if (counterX == 0) lineBuffer += Resources.boarderLT;//┌
                        if (counterX > 0 && counterX != mGameSizeX) lineBuffer += Resources.boarderTC; //┬
                        for (int count = 0; count < mFieldSizeX; count++)
                        {
                            lineBuffer += Resources.boarderVE;// Feldbreite entlang ─
                        }
                        if (counterX == mGameSizeX - 1) lineBuffer += Resources.baorderTR;// am ende ┐
                    }
                    if (counterY > 0 && counterY < mGameSizeY)
                    {
                        if (counterX == 0) lineBuffer += Resources.boarderLC;//├
                        if (counterX > 0 && counterX != mGameSizeX ) lineBuffer += Resources.boarderCR; //┼
                        for (int count = 0; count < mFieldSizeX; count++)
                        {
                            lineBuffer += Resources.boarderVE;// Feldbreite entlang ─
                        }
                        if (counterX == mGameSizeX - 1) lineBuffer += Resources.boarderRC;// am ende ┤
                    }
                    if (counterY == mGameSizeY) 
                    {
                        if (counterX == 0) lineBuffer += Resources.boarderBL;//└
                        if (counterX > 0 && counterX != mGameSizeX) lineBuffer += Resources.boarderBC; //┴
                        for (int count = 0; count < mFieldSizeX; count++)
                        {
                            lineBuffer += Resources.boarderVE;// Feldbreite entlang ─
                        }
                        if (counterX == mGameSizeX - 1) lineBuffer += Resources.boarderRB;// am ende ┘
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
        public override void Update()
        {
            DrawField();
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
