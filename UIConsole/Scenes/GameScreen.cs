using System;

namespace UIConsole
{
    class GameScreen : Scene 
    {
        protected bool mGameOver = false;
        protected TTTLogic.Logic mGameLogic;
        protected readonly int mFieldStartPosY;
        protected readonly int mFieldStartPosX;
        protected readonly int mFieldSizeY;
        protected readonly int mFieldSizeX;
        protected readonly int mGameSizeY;
        protected readonly int mGameSizeX;
        protected readonly int mNeedToWin;
        protected ConsoleColor mCurrentCursorColor;
        protected int currentY;
        protected int currentX;
        protected TTTLogic.TurnResult mTurnResult;
        protected Label[,] mFieldList;
        protected Label mBoarder;
        public GameScreen(int _startY, int _mGameSizeY , int _mGameSizeX ,int _mNeedToWin) 
        {
            Console.Clear();

            ;
            mNeedToWin = _mNeedToWin;
            currentY = _mGameSizeY / 2 + 1;
            currentX = _mGameSizeX / 2 + 1;
            mGameSizeY = _mGameSizeY;
            mGameSizeX = _mGameSizeX;        
            mFieldSizeY = MainResources.FieldX.Length;
            mFieldSizeX = MainResources.FieldX[0].Length;
            mFieldStartPosY = _startY;
            mGameLogic = new(mGameSizeY, mGameSizeX, mNeedToWin);


            mWindowSizeX = Math.Max( _mGameSizeX * mFieldSizeX + 15, MainResources.TikTakToe[0].Length+15);
            mWindowSizeY = _mGameSizeY * mFieldSizeY + MainResources.TikTakToe.Length +15;
            mBufferSizeX = mWindowSizeX;
            mBufferSizeY = mWindowSizeY;
            if (OperatingSystem.IsWindows()) WindowSize();

            mFieldStartPosX = (mWindowSizeX) / 2 - ((mGameSizeX)/2 + (mGameSizeX * mFieldSizeX )/2);
            
            mLabelList.Add(new Label(MainResources.TikTakToe,1, Positioning.center,  MainResources.SystemColorFront, MainResources.SystemColorBack));
            mFieldList = new Label[mGameSizeY, mGameSizeX];
            DrawBoarder();
            DrawField();

           
        }
        private void StartGame() 
        {
            if (OperatingSystem.IsWindows()) WindowSize();
            mGameLogic = new(mGameSizeY, mGameSizeX, mNeedToWin);
            DrawBoarder(); 
            DrawField();          
        }

        private void DrawOneField(int _fieldIdY,int _fieldIdX, string[] _fieldText, int _startPosY, int _startPosX, ConsoleColor _forground, ConsoleColor _background) 
        {
            Label _label = new(_startPosY, _startPosX, _fieldText,_forground, _background,true);
            mLabelList.Add(_label);
            mFieldList[_fieldIdY, _fieldIdX] = _label;
        }
        private void DrawField() 
        {
            TTTLogic.Board[,] GameBoard = mGameLogic.GetGameBoard();
            int startY;
            int startX;

            mCurrentCursorColor = mGameLogic.GetCurrentPlayer()? MainResources.PlayerBColorBack : MainResources.PlayerAColorBack;

            for (int countFY = 0; countFY < mGameSizeY; countFY++)
            {
                startY = mFieldStartPosY +(countFY * mFieldSizeY) + countFY + 1;

                for (int countFX = 0; countFX < mGameSizeX; countFX++)
                {
                    startX = mFieldStartPosX +(countFX * mFieldSizeX) + countFX + 1;
                    if (GameBoard[countFY, countFX] == TTTLogic.Board.Empty)
                    {
                        
                        DrawOneField(
                            countFY,
                            countFX,
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
                                countFY,
                                countFX,
                                MainResources.FieldO,
                                startY,
                                startX,
                                MainResources.PlayerAColorFront,
                                (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentCursorColor : MainResources.BoardColorBack
                            );

                    }
                    else if (GameBoard[countFY, countFX] == TTTLogic.Board.X)
                    {
                        DrawOneField(
                                countFY,
                                countFX,
                                MainResources.FieldX,
                                startY,
                                startX,
                                MainResources.PlayerBColorFront,
                                (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentCursorColor : MainResources.BoardColorBack
                            );
                    }       
                }
            }     
        }
        public void DrawBoarder()
        {
            Console.Clear();
            //Erstmal nur für 3x3
            int numberOfBoardLines = ((mFieldSizeY+1 )* mGameSizeY)+1;
            string[] boardBuffer =new string[numberOfBoardLines];
            string spacerLine = ""+ MainResources.boarderHO;
            for (int countF = 0; countF < mGameSizeX; countF++) 
            {
                for (int countFSX = 0; countFSX < mFieldSizeX; countFSX++)
                {
                    spacerLine += ' ';
                }
                spacerLine += MainResources.boarderHO;
            }
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
                boardBuffer[stepperY++] = lineBuffer;
                lineBuffer = "";
                if (counterY < mGameSizeY)
                { 
                    for (int countFSY = 0; countFSY < mFieldSizeY; countFSY++)
                    {             
                        boardBuffer[stepperY++] = spacerLine;                       
                    }
                }
            }
            mBoarder = new(mFieldStartPosY, mFieldStartPosX, boardBuffer,MainResources.BoardColorFront,MainResources.BoardColorBack,true);
            mLabelList.Add(mBoarder);          
        }
        public override void Update()
        {          
            if (mGameOver)
            {
                SceneManager.Instance.AddScene(new GameOver(mGameLogic, mTurnResult, this));
            }
            else 
            {
                if(Console.KeyAvailable == true) { 
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (currentY > 1) currentY--;
                            UpdateFields();
                            break;
                        case ConsoleKey.DownArrow:
                            if (currentY < mGameSizeY) currentY++;
                            UpdateFields();
                            break;
                        case ConsoleKey.LeftArrow:
                            if (currentX > 1) currentX--;
                            UpdateFields();
                            break;
                        case ConsoleKey.RightArrow:
                            if (currentX < mGameSizeX) currentX++;
                            UpdateFields();
                            break;

                        case ConsoleKey.Enter:
                            mTurnResult = mGameLogic.PlayerTurn(currentX-1, currentY-1);
                            UpdateFields();
                            if (mTurnResult == TTTLogic.TurnResult.Draw || mTurnResult == TTTLogic.TurnResult.WinO || mTurnResult == TTTLogic.TurnResult.WinX) mGameOver = true;
                            break;
                    }
                }
            }
        }
        public void UpdateFields() {
            TTTLogic.Board[,] GameBoard = mGameLogic.GetGameBoard();
            mCurrentCursorColor = mGameLogic.GetCurrentPlayer() ? MainResources.PlayerBColorBack : MainResources.PlayerAColorBack;
            for (int countFY = 0; countFY < mGameSizeY; countFY++)
            {
                for (int countFX = 0; countFX < mGameSizeX; countFX++)
                { 
                    if (GameBoard[countFY, countFX] == TTTLogic.Board.Empty)
                    {
                        mFieldList[countFY, countFX].Text       = MainResources.FieldE;
                        mFieldList[countFY, countFX].ColorFront = MainResources.BoardColorFront;
                        mFieldList[countFY, countFX].ColorBack  = (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentCursorColor : MainResources.BoardColorBack;
                        mFieldList[countFY, countFX].ReDeaw = true;
                    }
                    else if (GameBoard[countFY, countFX] == TTTLogic.Board.O)
                    {
                        mFieldList[countFY, countFX].Text = MainResources.FieldO;
                        mFieldList[countFY, countFX].ColorFront = MainResources.PlayerAColorFront;
                        mFieldList[countFY, countFX].ColorBack = (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentCursorColor : MainResources.BoardColorBack;
                        mFieldList[countFY, countFX].ReDeaw = true;
                    }
                    else if (GameBoard[countFY, countFX] == TTTLogic.Board.X)
                    {
                        mFieldList[countFY, countFX].Text = MainResources.FieldX;
                        mFieldList[countFY, countFX].ColorFront = MainResources.PlayerBColorFront;
                        mFieldList[countFY, countFX].ColorBack = (countFY + 1 == currentY && countFX + 1 == currentX) ? mCurrentCursorColor : MainResources.BoardColorBack;
                        mFieldList[countFY, countFX].ReDeaw = true;
                    }
                }
            }               
        }
        public void ResetGame()
        {
            Console.Clear();
            if (OperatingSystem.IsWindows()) WindowSize();
            mGameOver = false;
            mGameLogic.Reset();
            mBoarder.ReDeaw = true;
            UpdateFields();
        }
    }
}
