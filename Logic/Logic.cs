using System;
using System.Collections.Generic;

namespace TTTLogic
{
    
    public class Logic
    {
        public const int AxisY = 0;
        public const int AxisX = 1;


        private TurnResult gameState;
        public TurnResult GameState 
        {
            get { return gameState; }
        }
        private List<TurnResult> scoreList;

        private Board[,] mGameBoard; 
        private bool mCurrentPlayer; 

        public Logic(int _sizeY = 3, int _sizeX = 3) {
            scoreList = new();
            mGameBoard = new Board[_sizeY, _sizeX];
            SetRandomePlayer();
            gameState = TurnResult.Valid;

        }
        public bool GetCurrentPlayer() //Forgegeben /Struktur
        {
            return mCurrentPlayer;
        }
        
        public List<TurnResult> GetScoreList() //Forgegeben /Struktur
        {
            return scoreList;
        }
        public void Reset() //Forgegeben 
        {
            ClearBoard();
            gameState = TurnResult.Valid;
            mCurrentPlayer = !mCurrentPlayer;
        }
        public Board[,] GetGameBoard() 
        {
            return mGameBoard; 
        }
        public TurnResult PlayerTurn(int _X, int _y) //Forgegeben 
        {
            TurnResult _return;
            //Sieger steht fest
            if (GameIsFinished())
            {
                _return = TurnResult.Invalid;
            }
            else
            {
                //Prüfen ob Koordinaten im gültigen Bereich und ein Leerfeld
                if (IsInBoardRange(_y, _X) && mGameBoard[_y, _X] == Board.Empty)
                {
                    //Eintrag 
                    mGameBoard[_y, _X] = CurrentPlayerMark(mCurrentPlayer);
                    //Prüfen ob gewonnen oder Unendschieden
                    if (PlayerWin(CurrentPlayerMark(mCurrentPlayer)))
                    {
                        //Wenn Spieler gewonnen
                        //rückgabe Gewinner 
                        _return = mCurrentPlayer ? TurnResult.WinX : TurnResult.WinO;

                        //Sieger in Liste eintragen
                        scoreList.Add(_return);      
                    }
                    else if(BordIsFull())
                    {
                        //Wenn Spielbrett voll aber kein Gewinner
                        _return = TurnResult.Draw;
                        scoreList.Add(_return);
                    }
                    else
                    {
                        //Wenn Spielfeld nicht voll und kein Gewinner
                        _return = TurnResult.Valid;
                        mCurrentPlayer = !mCurrentPlayer;
                    }
                }
                //ansonsten Zug ungültig
                else { _return = TurnResult.Invalid; }
            }
            gameState = _return;
            return _return;
        }
        
        private Board CurrentPlayerMark(bool _mCurrentPlayer) {
            if (_mCurrentPlayer)
            {
                return Board.X;
            }
            else 
            {
                return Board.O;
            }        
        }
        private bool IsInBoardRange(int _Y,int _X) {
            if ((_Y < 0 || _Y > mGameBoard.GetLength(AxisY) - 1) 
             || (_X < 0 || _X > mGameBoard.GetLength(AxisX) - 1))return false ;
            return true;
        }
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
        private void SetRandomePlayer() {
            Random rand = new Random();
            int numb = rand.Next(1,3);//bei 1-2 wird nur eins ausgespucke bei 1-3  wird 1-2 !!!also ist 2 Parameter  nicht mehr im Pool
            switch (numb) 
            {
                case 0: mCurrentPlayer = false; break;
                case 1: mCurrentPlayer = true; break;
            }
        }
        private bool PlayerWin(Board _playerMark) 
        {
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
        private bool BordIsFull() {
            foreach (var field in mGameBoard)
            {
                if (field == Board.Empty) return false;
            }
            return true;
        }
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
