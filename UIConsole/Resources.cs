using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    class Resources
    {
        private static Config mainConfig = CreateDefault();

        public static string[] winner;
        public static string[] winX;
        public static string[] winO;
        public static string[] winD;
        public static string[] draw;

        public static char[,] FieldX;
        public static char[,] FieldO;
        public static char[,] FieldE;
        private static char[] mBoarderList;

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
                                     //Board
        public static ConsoleColor BoardColorFront
        {
            get { return mainConfig.boardColorFront; }
        }
        public static ConsoleColor BoardColorBack
        {
            get { return mainConfig.boardColorBack; }
        }
        //Player A
        public static ConsoleColor PlayerAColorFront
        {
            get{ return mainConfig.playerAMarkColorFront; }
        }
        public static ConsoleColor PlayerAColorBack
        {
            get { return mainConfig.playerAMarkColorBack; }
        }
        //Player B
        public static ConsoleColor PlayerBColorFront
        {
            get { return mainConfig.playerBMarkColorFront; }
        }
        public static ConsoleColor PlayerBColorBack
        {
            get { return mainConfig.playerBMarkColorBack; }
        }
        //Empty Field
        public static ConsoleColor EmptyColorFront
        {
            get { return mainConfig.EmptyMarkColorFront; }
        }
        public static ConsoleColor EmptyColorBack
        {
            get { return mainConfig.EmptyMarkColorBack; }
        }
        //System
        public static ConsoleColor SystemColorFront
        {
            get { return mainConfig.systemColorFront; }
        }
        public static ConsoleColor SystemColorAcent
        {
            get { return mainConfig.systemColorAcent; }
        }
        public static ConsoleColor SystemColorBack
        {
            get { return mainConfig.systemColorBack; }
        }
        //Menu
        public static ConsoleColor MenuColorFront
        {
            get { return mainConfig.menuColorFront; }
        }
        public static ConsoleColor MenuColorBack
        {
            get { return mainConfig.menuColorBack; }
        }
        public static ConsoleColor MenuColorActive
        {
            get { return mainConfig.menuColorActive; }
        }



        public static void SetValues() 
        {
            winner = LoadAs.StringArr(mainConfig.gameOverWinnerURL);
            winX = LoadAs.StringArr(mainConfig.gameOverWinBURL);
            winO = LoadAs.StringArr(mainConfig.gameOverWinAURL);
            winD = LoadAs.StringArr(mainConfig.gameOverWinDURL);
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
            _return.systemColorAcent = ConsoleColor.Gray;
            _return.systemColorBack = ConsoleColor.Black;

            _return.playerAMarkColorFront = ConsoleColor.Blue;
            _return.playerAMarkColorBack = ConsoleColor.DarkBlue;
            _return.playerAMarkURL = dirIMG + "FieldA.txt";

            _return.playerBMarkColorFront = ConsoleColor.Red;
            _return.playerBMarkColorBack = ConsoleColor.DarkRed;
            _return.playerBMarkURL = dirIMG + "FieldB.txt";

            _return.EmptyMarkColorFront = ConsoleColor.Gray;
            _return.EmptyMarkColorBack = ConsoleColor.DarkGray;
            _return.EmptyMarkURL = dirIMG + "FieldE.txt";

            _return.boarderURL = dirIMG + "Boarder.txt"; ;
            _return.boardColorFront = ConsoleColor.White;
            _return.boardColorBack = ConsoleColor.DarkGray;
            //Menu
            _return.menuColorFront = ConsoleColor.White;
            _return.menuColorBack = ConsoleColor.Black;
            _return.menuColorActive = ConsoleColor.DarkBlue;


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
