using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    struct Config
    {

        //---System
        public int windowSizeY;
        public int windowSizeX;
        public ConsoleColor systemColorFront;
        public ConsoleColor systemColorBack;


        //---GameScreen
        //PlayerAMark
        public ConsoleColor playerAMarkColorBack;
        public ConsoleColor playerAMarkColorFront;
        public string playerAMarkURL;
        //PlayerBMark
        public ConsoleColor playerBMarkColorBack;
        public ConsoleColor playerBMarkColorFront;
        public string playerBMarkURL;
        public string boarderURL;
        public ConsoleColor fieldColorFront;
        public ConsoleColor fieldColorBack;

        //---Menu
        public ConsoleColor menuColorFront;
        public ConsoleColor menuColorBack;
        public ConsoleColor menuColorActive;
        //---GameOver
        public string gameOverWinnerURL;
        public string gameOverDrawURL;
        public string gameOverWinAURL;
        public string gameOverWinBURL;
    }
}
