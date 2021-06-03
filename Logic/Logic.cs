using System;
using System.Collections.Generic;

namespace TTTLogic
{
    
    public class Logic
    {
        public const int AxisY = 0;
        public const int AxisX = 1;

        private bool mCurrentPlayer; 
        private TurnResult gameState;
        public bool GetCurrentPlayer() //Forgegeben /Struktur
        {
            return mCurrentPlayer;
        }
        public TurnResult GameState 
        {
            get { return gameState; }
        }
        private readonly List<TurnResult> scoreList;
        public List<TurnResult> GetScoreList() //Forgegeben /Struktur
        {
            return scoreList;
        }
        private readonly Board[,] mGameBoard; 
        public Board[,] GetGameBoard() 
        {
            return mGameBoard; 
        }

        public Logic() {
            int _sizeY = 3;
            int _sizeX = 3;
            scoreList = new();
            mGameBoard = new Board[_sizeY, _sizeX];
            SetRandomePlayer();
            gameState = TurnResult.Valid;
        }   
        /// <summary>
        /// Set the defaults and randome the current player
        /// </summary>
        public void Reset() //Forgegeben 
        {
            ClearBoard();
            gameState = TurnResult.Valid;
            mCurrentPlayer = !mCurrentPlayer;
        }
        /// <summary>
        /// Set the mark to the gameboard if the game isn't finished or invalid.
        /// Check if on player has won the game and set the winner on the list and return and set the gamestate to winX/winO
        /// If no one won the game and the gameboard it set the gamestate, and return draw.
        /// </summary>
        /// <param name="_Y">vertical value</param>
        /// <param name="_X">horizontal value</param>
        /// <returns></returns>
        public TurnResult PlayerTurn(int _X, int _y) //Forgegeben 
        {
            TurnResult _return;
            //The winner is certain.
            if (GameIsFinished())
            {
                _return = TurnResult.Invalid;
            }
            else
            {
                //Check whether coordinates are in the valid area and a blank field.
                if (IsInBoardRange(_y, _X) && mGameBoard[_y, _X] == Board.Empty)
                {
                    //entry the mark.
                    mGameBoard[_y, _X] = CurrentPlayerMark();
                    //Check whether a win or draw.
                    if (CurrentPlayerWin()) 
                    {
                        //When player won ...
                        //return winner.
                        _return = mCurrentPlayer ? TurnResult.WinX : TurnResult.WinO;

                        //Enter the winner in the list.
                        scoreList.Add(_return);      
                    }
                    else if(BordIsFull())
                    {
                        //When the board is full but no winner...
                        //return draw
                        _return = TurnResult.Draw;
                        //Enter draw in the list.
                        scoreList.Add(_return);
                    }
                    else
                    {
                        //When the playing field is not full and not a winner detected...
                        //return valid.
                        _return = TurnResult.Valid;
                        //Change the current player.
                        mCurrentPlayer = !mCurrentPlayer;
                    }
                }
                //otherwise the turn is invalid.
                else { _return = TurnResult.Invalid; }
            }
            //Set the gameState to the return TurnResult.
            gameState = _return;
            //return the TurnResult.
            return _return;
        }
        /// <summary>
        /// Give Back the player equivalent from (enumartion)Board.
        /// </summary>
        /// <returns>(enumartion)Board</returns>
        private Board CurrentPlayerMark() {
            if (mCurrentPlayer)
            {
                return Board.X;
            }
            else 
            {
                return Board.O;
            }        
        }
        /// <summary>
        /// Check the Y and X values are in the length of the board and not lesser than zero. 
        /// </summary>
        /// <param name="_Y">vertical value</param>
        /// <param name="_X">horizontal value</param>
        /// <returns></returns>
        private bool IsInBoardRange(int _Y,int _X) {
            if ((_Y < 0 || _Y > mGameBoard.GetLength(AxisY) - 1) 
             || (_X < 0 || _X > mGameBoard.GetLength(AxisX) - 1))return false ;
            return true;
        }
        /// <summary>
        /// Set all Fields to empty.
        /// </summary>
        private void ClearBoard()
        {
            for (int y = 0; y < mGameBoard.GetLength(AxisY); y++)
            {
                for (int x = 0; x < mGameBoard.GetLength(AxisX); x++)
                {
                    mGameBoard[y, x] = Board.Empty;
                }
            }
        }
        /// <summary>
        /// Set mCurrentPlayer random to true or false.
        /// </summary>
        private void SetRandomePlayer() {
            Random rand = new Random();
            int numb = rand.Next(1,3);//bei 1-2 wird nur eins ausgespucke bei 1-3  wird 1-2 !!!also ist 2 Parameter  nicht mehr im Pool
            switch (numb) 
            {
                case 0: mCurrentPlayer = false; break;
                case 1: mCurrentPlayer = true; break;
            }
        }
        /// <summary>
        /// Check if the current player has won
        /// </summary>
        /// <returns>bool for player</returns>
        private bool CurrentPlayerWin() 
        {
            Board _playerMark = CurrentPlayerMark();
            if (
                (
                   mGameBoard[0, 0] == _playerMark//#--
                && mGameBoard[1, 0] == _playerMark//#--
                && mGameBoard[2, 0] == _playerMark//#--
                ) || (
                   mGameBoard[0, 1] == _playerMark//-#-
                && mGameBoard[1, 1] == _playerMark//-#-
                && mGameBoard[2, 1] == _playerMark//-#-
                ) || (
                   mGameBoard[0, 2] == _playerMark//--#
                && mGameBoard[1, 2] == _playerMark//--#
                && mGameBoard[2, 2] == _playerMark//--#
                ) || (
                   mGameBoard[0, 0] == _playerMark//###
                && mGameBoard[0, 1] == _playerMark//---
                && mGameBoard[0, 2] == _playerMark//---
                ) || (
                   mGameBoard[1, 0] == _playerMark//---
                && mGameBoard[1, 1] == _playerMark//###
                && mGameBoard[1, 2] == _playerMark//---
                ) || (
                   mGameBoard[2, 0] == _playerMark//---
                && mGameBoard[2, 1] == _playerMark//---
                && mGameBoard[2, 2] == _playerMark//###
                ) || (
                   mGameBoard[0, 0] == _playerMark//#--
                && mGameBoard[1, 1] == _playerMark//-#-
                && mGameBoard[2, 2] == _playerMark//--#
                ) || (
                   mGameBoard[0, 2] == _playerMark//--#
                && mGameBoard[1, 1] == _playerMark//-#-
                && mGameBoard[2, 0] == _playerMark//#--
                )
            )
            { return true; }
            else { return false;}     
        }
        /// <summary>
        /// If no field is empty it returns true else false.
        /// </summary>
        /// <returns> true / fals </returns>
        private bool BordIsFull() {
            foreach (var field in mGameBoard)
            {
                if (field == Board.Empty) return false;
            }
            return true;
        }
        /// <summary>
        /// If TurnResult is Draw or Win it return ture else false.
        /// </summary>
        /// <returns>true / false</returns>
        public bool GameIsFinished()
        {
            if (gameState == TurnResult.Draw || gameState == TurnResult.WinO || gameState == TurnResult.WinX)
            {
                return true;
            }
            else { return false; }
        }
    }
}
