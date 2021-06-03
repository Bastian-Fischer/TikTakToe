using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UIConsole
{
    class Properties
    {
        private Config mMain;
        public Config Main
        {
            get { return mMain; }
        }
        private Config mDefaultConfig;
        
        private Config CreateDefault() 
        {
            Config _return;
            string dirIMG = "TTTIMG/";
            _return.windowSizeY = 50;
            _return.windowSizeX = 90;
            _return.systemColorFront = ConsoleColor.White;
            _return.systemColorBack = ConsoleColor.Black;

            _return.playerAMarkColorBack = ConsoleColor.Blue;
            _return.playerAMarkColorFront = ConsoleColor.DarkBlue;
            _return.playerAMarkURL = dirIMG + "";

            _return.playerBMarkColorBack = ConsoleColor.Red;
            _return.playerBMarkColorFront = ConsoleColor.DarkRed;
            _return.playerBMarkURL = dirIMG + "";

            _return.boarderURL = dirIMG + ""; ;
            _return.fieldColorFront = ConsoleColor.White;
            _return.fieldColorBack = ConsoleColor.DarkGray;
            //Menu
            _return.menuColorFront = ConsoleColor.White;
            _return.menuColorBack = ConsoleColor.Black;
            _return.menuColorActive = ConsoleColor.Cyan;


            _return.gameOverWinnerURL = dirIMG + "";
            _return.gameOverDrawURL = dirIMG +   "";
            _return.gameOverWinAURL = dirIMG +   "";
            _return.gameOverWinBURL = dirIMG +   "";

            return _return;
    }

        private void SaveConfig(Config _config , string _name)
        {
            //TODO json Serialiser
            
        


        }



    }
}
