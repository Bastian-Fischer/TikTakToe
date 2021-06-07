
using System;
using System.Collections.Generic;

namespace TTTLogic
{

    public class Logic
    {
        private readonly int mBoardSizeY;
        private readonly int mBoardSizeX;
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

        public Logic()
        {
            mBoardSizeY = 3;
            mBoardSizeX = 3;
            scoreList = new();
            mGameBoard = new Board[mBoardSizeY, mBoardSizeX];
            SetRandomePlayer();
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
        public TurnResult PlayerTurn(int _X, int _y) //Forgegeben 
        {
            //The winner is certain.
            //if (GameIsFinished()) return TurnResult.Invalid;
            if (mGameOver) return TurnResult.Invalid;
            //Check whether coordinates are in the valid area and a blank field.
            if (IsInBoardRange(_y, _X) && mGameBoard[_y, _X] == Board.Empty)
            {
                //entry the mark.
                mGameBoard[_y, _X] = CurrentPlayerMark();
                //Check whether a win or draw.
                if (CurrentPlayerWin())
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
           return ((_Y > -1 && _Y < mBoardSizeY) && (_X > -1 && _X < mBoardSizeX));        
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
        private void SetRandomePlayer()
        {
            Random rand = new Random();
            switch (rand.Next(1, 3))
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
            return (
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
            );
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