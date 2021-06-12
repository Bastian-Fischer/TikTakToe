
using System;
using System.Collections.Generic;

namespace TTTLogic
{

    public class Logic
    {
        private readonly int mSmallest = 3;
        private readonly int mBiggest = 21;
        private readonly int mBoardSizeY;
        private readonly int mBoardSizeX;
        private readonly int mNeedToWin;
        public int NeedToWin
        {
            get { return mNeedToWin; }
        }

        private bool mGameOver = false;
        private bool mCurrentPlayer;
        public bool GetCurrentPlayer() //Forgegeben /Struktur
        {
            return mCurrentPlayer;
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

        public Logic(int _mBoardSizeY = 3, int _mBoardSizeX = 3, int _mNeedToWin = 3)
        {
            if (_mBoardSizeY < mSmallest) _mBoardSizeY = mSmallest;
            if (_mBoardSizeX < mSmallest) _mBoardSizeX = mSmallest;
            if (_mBoardSizeY > mBiggest) _mBoardSizeY = mBiggest;
            if (_mBoardSizeX > mBiggest) _mBoardSizeX = mBiggest;
            if (_mNeedToWin < mSmallest) _mNeedToWin = mSmallest;
            int smallerAxis = Math.Min(_mBoardSizeY, _mBoardSizeX);
            if (smallerAxis < _mNeedToWin) _mNeedToWin = smallerAxis;
           mBoardSizeY = _mBoardSizeY;
            mBoardSizeX = _mBoardSizeX;
            mNeedToWin = _mNeedToWin;
            scoreList = new();
            mGameBoard = new Board[mBoardSizeY, mBoardSizeX];
            mSetRandomPlayer();
        }
        /// <summary>
        /// Set the defaults and randome the current player
        /// </summary>
        public void Reset() //Forgegeben 
        {
            ClearBoard();
            mGameOver = false;
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
        public TurnResult PlayerTurn(int _X, int _Y) //Forgegeben 
        {
            //The winner is certain.
            //if (GameIsFinished()) return TurnResult.Invalid;
            if (mGameOver) return TurnResult.Invalid;
            //Check whether coordinates are in the valid area and a blank field.
            if (IsInBoardRange(_Y, _X) && mGameBoard[_Y, _X] == Board.Empty)
            {
                //entry the mark.
                mGameBoard[_Y, _X] = CurrentPlayerMark();
                //Check whether a win or draw.
                if (mCurrentPlayerWin(_X, _Y))
                {       
                    mGameOver = true;
                    scoreList.Add(mCurrentPlayer ? TurnResult.WinX : TurnResult.WinO);
                    return mCurrentPlayer ? TurnResult.WinX : TurnResult.WinO;
                }
                else if (BordIsFull())
                {
                    mGameOver = true;
                    scoreList.Add(TurnResult.Draw);
                    return  TurnResult.Draw;
                }
                else
                {
                    mCurrentPlayer = !mCurrentPlayer;
                    return TurnResult.Valid;
                }
            }
            //otherwise the turn is invalid.
            else { return TurnResult.Invalid; }  
        }
        /// <summary>
        /// Give Back the player equivalent from (enumartion)Board.
        /// </summary>
        /// <returns>(enumartion)Board</returns>
        private Board CurrentPlayerMark()
        {
            return mCurrentPlayer ? Board.X : Board.O;
        }
        /// <summary>
        /// Check the Y and X values are in the length of the board and not lesser than zero. 
        /// </summary>
        /// <param name="_Y">vertical value</param>
        /// <param name="_X">horizontal value</param>
        /// <returns></returns>
        private bool IsInBoardRange(int _Y, int _X)
        {
           return (_Y > -1 && _Y < mBoardSizeY && _X > -1 && _X < mBoardSizeX);        
        }
        /// <summary>
        /// Set all Fields to empty.
        /// </summary>
        private void ClearBoard()
        {
            for (int y = 0; y < mBoardSizeY ; y++)
            {
                for (int x = 0; x < mBoardSizeX ; x++)
                {
                    mGameBoard[y, x] = Board.Empty;
                }
            }
        }
        /// <summary>
        /// Set mCurrentPlayer random to true or false.
        /// </summary>
        private void mSetRandomPlayer()
        {
            Random rand = new Random();
            mCurrentPlayer = rand.Next() % 2 == 1;
        }
        /// <summary>
        /// Check if the current player has won
        /// </summary>
        /// <returns>bool for player</returns>
        private bool mCurrentPlayerWin(int X, int Y)
        {
            Board _playerMark = CurrentPlayerMark();

            bool    NegativeHit   = true;
            bool    PositiveHit   = true;
            int     HitCounter    = 0;        //todo drl ausschreiben und negativehitcounter abkürzen

            //vague
            for (int i = 1; i < mNeedToWin; i++)
            {
                //  left
                if (X - i > -1 && NegativeHit)
                {
                    if (mGameBoard[Y, X - i] == _playerMark) //todo beide if sind mit && kombinierbar
                        HitCounter++;
                }
                else NegativeHit = false;
                //  right
                if (X + i < mBoardSizeX && PositiveHit)
                {
                    if (mGameBoard[Y, X + i] == _playerMark)
                        HitCounter++;
                }
                else PositiveHit = false;

                if (HitCounter + 1 == mNeedToWin) return true;
            }


            NegativeHit = true;
            PositiveHit = true;
            HitCounter = 0;
            for (int i = 1; i < mNeedToWin; i++)
            {
                    //----------------------------------------------------------------------------
                    //  top
                    if (Y - i > -1 && NegativeHit)
                {
                    if (mGameBoard[Y - i, X] == _playerMark)
                        HitCounter++;
                }
                else NegativeHit = false;
                //  down
                if (Y + i < mBoardSizeY && PositiveHit)
                {
                    if (mGameBoard[Y + i, X] == _playerMark)
                        HitCounter++;
                }
                else PositiveHit = false;
                //horizontal
                if (HitCounter + 1  == mNeedToWin) return true;
            }

            NegativeHit = true;
            PositiveHit = true;
            HitCounter = 0;

            for (int i = 1; i < mNeedToWin; i++)
            {

                //----------------------------------------------------------------------------
                // top left
                if (X - i > -1 && Y - i > -1 && NegativeHit)
                {
                    if (mGameBoard[Y - i, X - i] == _playerMark)
                        HitCounter++;
                }
                else NegativeHit = false;
                // down right
                if (X + i < mBoardSizeY && Y + i < mBoardSizeY && PositiveHit)
                {
                    if (mGameBoard[Y + i, X + i] == _playerMark)
                        HitCounter++;
                }
                else PositiveHit = false;
                //leftTop rightdown
                if (HitCounter + 1  == mNeedToWin) return true;
            }

            NegativeHit = true;
            PositiveHit = true;
            HitCounter = 0;

            for (int i = 1; i < mNeedToWin; i++)
            {
                //----------------------------------------------------------------------------
                // top right
                if ( X + i < mBoardSizeY && Y - i > -1 && NegativeHit)
                {
                    if (mGameBoard[Y - i, X + i] == _playerMark)
                        HitCounter++;
                }
                else NegativeHit = false;
                // down left
                if (X - i > -1 && Y + i < mBoardSizeY && PositiveHit)
                {
                    if (mGameBoard[Y + i, X - i] == _playerMark)
                        HitCounter++;
                }
                else PositiveHit = false;
                //rightTop lefttdown
                if (HitCounter + 1 == mNeedToWin) return true;
            }
            return false;
        }
        /// <summary>
        /// If no field is empty it returns true else false.
        /// </summary>
        /// <returns> true / fals </returns>
        private bool BordIsFull()
        {
            foreach (var field in mGameBoard)
            {
                if (field == Board.Empty) return false;
            }
            return true;
        }
    }
}