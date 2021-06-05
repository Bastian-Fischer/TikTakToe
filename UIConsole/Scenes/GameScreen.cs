using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    class GameScreen : Scene 
    {
        protected TTTLogic.Logic mGameLogic;
        protected readonly int mFieldStartPosY;
        protected readonly int mFieldStartPosX;
        protected readonly int mFieldSizeY;
        protected readonly int mFieldSizeX;
        protected readonly int mGameSizeY;
        protected readonly int mGameSizeX;                      
        protected ConsoleColor mCurrentCursorColor;
        protected int currentY;
        protected int currentX;
        protected TTTLogic.TurnResult mTurnResult;
        public GameScreen(int _startY) 
        {
            mGameLogic = new();
            currentY = 2;
            currentX = 2;
            mGameSizeY = 3;
            mGameSizeX = 3;        

            mFieldSizeY = Resources.MainResources.FieldX.GetLength(0);
            mFieldSizeX = MainResources.FieldX.GetLength(1);
            mFieldStartPosY = _startY;
            mFieldStartPosX =  Console.BufferWidth /2 - (2+(mGameSizeX * MainResources.FieldE.GetLength(1)+1)/2);

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

            mCurrentCursorColor = mGameLogic.GetCurrentPlayer()? MainResources.PlayerBColorFront : MainResources.PlayerAColorFront;

            for (int countFY = 0; countFY < mGameSizeY; countFY++)
            {
                startY = mFieldStartPosY +(countFY * mFieldSizeY) + countFY + 2;

                for (int countFX = 0; countFX < mGameSizeX; countFX++)
                {
                    startX = mFieldStartPosX +(countFX * mFieldSizeX) + countFX + 1;
                    if (GameBoard[countFY, countFX] == TTTLogic.Board.Empty)
                    {
                        
                        DrawOneField(
                            MainResources.FieldE,
                            startY,
                            startX,
                            MainResources.BoardColorFront,
                            (countFY+1 == currentY && countFX+1 == currentX) ? mCurrentCursorColor : MainResources.BoardColorBack
                        );

                    }
                    else if(GameBoard[countFY, countFX] == TTTLogic.Board.O)
                    {
                        DrawOneField(
                                MainResources.FieldO,
                                startY,
                                startX,
                                MainResources.PlayerAColorFront,
                                (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentCursorColor : MainResources.PlayerAColorBack
                            );

                    }
                    else if (GameBoard[countFY, countFX] == TTTLogic.Board.X)
                    {
                        DrawOneField(
                                MainResources.FieldX,
                                startY,
                                startX,
                                MainResources.PlayerBColorFront,
                                (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentCursorColor : MainResources.PlayerBColorBack
                            );
                    }       
                }
            }     
        }
        public void DrawBoarder()
        {
            Console.Clear();
            //Erstmal nur für 3x3

            string spacerLine = ""+ MainResources.boarderHO;
            for (int countF = 0; countF < mGameSizeX; countF++) 
            {
                for (int countFSX = 0; countFSX < mFieldSizeX; countFSX++)
                {
                    spacerLine += ' ';
                }
                spacerLine += MainResources.boarderHO;
            }
            Console.ForegroundColor = MainResources.BoardColorFront;
            Console.BackgroundColor = MainResources.BoardColorBack;
            string lineBuffer = "";
            int stepperY = 0;
            for (int counterY = 0; counterY <= mGameSizeY; counterY++)
            {
                for (int counterX = 0; counterX < mGameSizeX; counterX++)
                {
                    if (counterY == 0)
                    { 
                        if (counterX == 0) lineBuffer += MainResources.boarderLT;//┌
                        if (counterX > 0 && counterX != mGameSizeX) lineBuffer += MainResources.boarderTC; //┬
                        for (int count = 0; count < mFieldSizeX; count++)
                        {
                            lineBuffer += MainResources.boarderVE;// Feldbreite entlang ─
                        }
                        if (counterX == mGameSizeX - 1) lineBuffer += MainResources.baorderTR;// am ende ┐
                    }
                    if (counterY > 0 && counterY < mGameSizeY)
                    {
                        if (counterX == 0) lineBuffer += MainResources.boarderLC;//├
                        if (counterX > 0 && counterX != mGameSizeX ) lineBuffer += MainResources.boarderCR; //┼
                        for (int count = 0; count < mFieldSizeX; count++)
                        {
                            lineBuffer += MainResources.boarderVE;// Feldbreite entlang ─
                        }
                        if (counterX == mGameSizeX - 1) lineBuffer += MainResources.boarderRC;// am ende ┤
                    }
                    if (counterY == mGameSizeY) 
                    {
                        if (counterX == 0) lineBuffer += MainResources.boarderBL;//└
                        if (counterX > 0 && counterX != mGameSizeX) lineBuffer += MainResources.boarderBC; //┴
                        for (int count = 0; count < mFieldSizeX; count++)
                        {
                            lineBuffer += MainResources.boarderVE;// Feldbreite entlang ─
                        }
                        if (counterX == mGameSizeX - 1) lineBuffer += MainResources.boarderRB;// am ende ┘
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
                SceneManager.Instance.AddScene(new GameOver(mGameLogic, mTurnResult, this));
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
                        mTurnResult = mGameLogic.PlayerTurn(currentX-1, currentY-1);
                        break;
                }
            }
        }
    }
}
