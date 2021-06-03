using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    class Resources
    {
        public static Config mainConfig = CreateDefault();

        public static string[]  winner      ;
        public static string[]  winnerX     ;
        public static string[]  winnerO     ;
        public static string[]  line        ;
        public static string[]  draw        ;
                                            
        public static char[,]   FieldX      ;
        public static char[,]   FieldO      ;
        public static char[,]   FieldE      ;
        private static char[]   mBoarderList;

        public static char boarderLT;//'┌'
        public static char boarderTC;//'┬'
        public static char baorderTR;//'┐'
        public static char boarderRC;//'┤'
        public static char boarderRB;//'┘'
        public static char boarderBC;//'┴'
        public static char boarderBL;//'└'
        public static char boarderLC;//'├'
        public static char boarderCR;//'┼'
        public static char boarderVE;//'─'
        public static char boarderHO;//'│'

        public void SetValues() 
        {
            winner = LoadAs.StringArr(mainConfig.gameOverWinnerURL);
            winnerX = LoadAs.StringArr(mainConfig.gameOverWinBURL);
            winnerO = LoadAs.StringArr(mainConfig.gameOverWinAURL);
            line = LoadAs.StringArr(mainConfig.gameOverWinDURL);
            draw = LoadAs.StringArr(mainConfig.gameOverDrawURL);

            FieldX = LoadAs.CharMultidimensionalArr(mainConfig.playerBMarkURL);
            FieldO = LoadAs.CharMultidimensionalArr(mainConfig.playerAMarkURL);
            FieldE = LoadAs.CharMultidimensionalArr(mainConfig.EmptyMarkURL);
            mBoarderList = LoadAs.CharArrFirstOneFromLine(mainConfig.boarderURL);

            boarderLT = mBoarderList[0];//'┌
            boarderTC = mBoarderList[1];//'┬
            baorderTR = mBoarderList[2];//'┐
            boarderRC = mBoarderList[3];//'┤
            boarderRB = mBoarderList[4];//'┘
            boarderBC = mBoarderList[5];//'┴
            boarderBL = mBoarderList[6];//'└
            boarderLC = mBoarderList[7];//'├
            boarderCR = mBoarderList[8];//'┼
            boarderVE = mBoarderList[9];//'─
            boarderHO = mBoarderList[10];//'│

        }

        public static Config CreateDefault()
        {
            Config _return;
            string dirIMG = "TTTIMG/";
            _return.windowSizeY = 50;
            _return.windowSizeX = 90;
            _return.systemColorFront = ConsoleColor.White;
            _return.systemColorBack = ConsoleColor.Black;

            _return.playerAMarkColorBack = ConsoleColor.Blue;
            _return.playerAMarkColorFront = ConsoleColor.DarkBlue;
            _return.playerAMarkURL = dirIMG + "FieldA.txt";

            _return.playerBMarkColorBack = ConsoleColor.Red;
            _return.playerBMarkColorFront = ConsoleColor.DarkRed;
            _return.playerBMarkURL = dirIMG + "FieldB.txt";

            _return.EmptyMarkColorBack = ConsoleColor.Red;
            _return.EmptyMarkColorFront = ConsoleColor.DarkGray;
            _return.EmptyMarkURL = dirIMG + "FieldE.txt";

            _return.boarderURL = dirIMG + "Boarder.txt"; ;
            _return.fieldColorFront = ConsoleColor.White;
            _return.fieldColorBack = ConsoleColor.DarkGray;
            //Menu
            _return.menuColorFront = ConsoleColor.White;
            _return.menuColorBack = ConsoleColor.Black;
            _return.menuColorActive = ConsoleColor.Cyan;


            _return.gameOverWinnerURL = dirIMG + "Winner.txt";
            _return.gameOverDrawURL = dirIMG + "Draw.txt";
            _return.gameOverWinAURL = dirIMG + "WinA.txt";
            _return.gameOverWinBURL = dirIMG + "WinB.txt";
            _return.gameOverWinDURL = dirIMG + "WinD.txt";

            return _return;
        }

        private void SaveConfig(Config _config, string _name)
        {
            //TODO json Serialiser




        }
    }
}
