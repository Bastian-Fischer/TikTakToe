using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UIConsole
{
    class Resources
    {
        private static Resources mMainResources;
        public static Resources MainResources
        {
            get
            {
                if (mMainResources is null) // wenn noch kein Objekt erstellt wurde holen wir das nach. "Lazy Initialization"
                    mMainResources = new(); // das einzige new für den SceneManager das es je geben wird
                return mMainResources;
            }
        }
        public Config mainConfig;
        public string[] TikTakToe;
        public string[] Menu;
        public string[] winner;
        public string[] winX;
        public string[] winO;
        public string[] winD;
        public string[] draw;

        public string[] FieldX;
        public string[] FieldO;
        public string[] FieldE;

        private char[] mBoarderList;
        public char boarderLT;//'┌'
        public char boarderTC;//'┬'
        public char baorderTR;//'┐'
        public char boarderRC;//'┤'
        public char boarderRB;//'┘'
        public char boarderBC;//'┴'
        public char boarderBL;//'└'
        public char boarderLC;//'├'
        public char boarderCR;//'┼'
        public char boarderVE;//'─'
        public char boarderHO;//'│'
        private Resources()//nur für Singleton
        { 
            mainConfig = CreateDefault();
            SetValues();
        }
        //Board
        public ConsoleColor BoardColorFront
        {
            get { return mainConfig.boardColorFront; }
        }
        public ConsoleColor BoardColorBack
        {
            get { return mainConfig.boardColorBack; }
        }
        //Player A
        public ConsoleColor PlayerAColorFront
        {
            get{ return mainConfig.playerAMarkColorFront; }
        }
        public ConsoleColor PlayerAColorBack
        {
            get { return mainConfig.playerAMarkColorBack; }
        }
        //Player B
        public ConsoleColor PlayerBColorFront
        {
            get { return mainConfig.playerBMarkColorFront; }
        }
        public ConsoleColor PlayerBColorBack
        {
            get { return mainConfig.playerBMarkColorBack; }
        }
        //Empty Field
        public ConsoleColor EmptyColorFront
        {
            get { return mainConfig.EmptyMarkColorFront; }
        }
        public ConsoleColor EmptyColorBack
        {
            get { return mainConfig.EmptyMarkColorBack; }
        }
        //System
        public ConsoleColor SystemColorFront
        {
            get { return mainConfig.systemColorFront; }
        }
        public ConsoleColor SystemColorAcent
        {
            get { return mainConfig.systemColorAcent; }
        }
        public ConsoleColor SystemColorBack
        {
            get { return mainConfig.systemColorBack; }
        }
        //Menu
        public ConsoleColor MenuColorFront
        {
            get { return mainConfig.menuColorFront; }
        }
        public ConsoleColor MenuColorBack
        {
            get { return mainConfig.menuColorBack; }
        }
        public ConsoleColor MenuColorActive
        {
            get { return mainConfig.menuColorActive; }
        }
        public void SetValues() 
        {
            TikTakToe = LoadAs.StringArr(mainConfig.TikTakToeURL);
            Menu = LoadAs.StringArr(mainConfig.MenuURL);

            winner = LoadAs.StringArr(mainConfig.gameOverWinnerURL);
            winX = LoadAs.StringArr(mainConfig.gameOverWinBURL);
            winO = LoadAs.StringArr(mainConfig.gameOverWinAURL);
            winD = LoadAs.StringArr(mainConfig.gameOverWinDURL);
            draw = LoadAs.StringArr(mainConfig.gameOverDrawURL);

            FieldX = LoadAs.StringArr(mainConfig.playerBMarkURL);
            FieldO = LoadAs.StringArr(mainConfig.playerAMarkURL);
            FieldE = LoadAs.StringArr(mainConfig.EmptyMarkURL);
            mBoarderList = LoadAs.CharArrFirstOneFromLine(mainConfig.boarderURL);

            //todo replace variables with enum access to list
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
        public Config CreateDefault()
        {
            Config _return;
            string dirIMG = "TTTIMG/";
            _return.windowSizeY = 50;
            _return.windowSizeX = 90;
            _return.systemColorFront = ConsoleColor.White;
            _return.systemColorAcent = ConsoleColor.Gray;
            _return.systemColorBack = ConsoleColor.Black;
            _return.TikTakToeURL = dirIMG + "TikTakToe.txt";
            _return.MenuURL = dirIMG + "Menu.txt";

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
        public void SaveConfigJson(Config _config, string _name, string _dir = "System/")
        {
            //TODO json Serialiser, idealerweise mit StreamWriter
            string jsonString = JsonSerializer.Serialize(_config);
            File.WriteAllText(_dir+ _name+".json", jsonString);
        }
        public Config LoadConfigJson(string _dir) {

            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            string jsonString =  File.ReadAllText(_dir); //todo StreamReader währe cooler
            Config _return = JsonSerializer.Deserialize<Config>(jsonString, options);
            return _return;
        }

    }
}
