using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UIConsole
{
    struct Config
    {
        //---System
        [JsonInclude]
        public int          windowSizeY;
        [JsonInclude]
        public int          windowSizeX;
        [JsonInclude]
        public ConsoleColor systemColorFront;
        [JsonInclude]
        public ConsoleColor systemColorAcent;
        [JsonInclude]
        public ConsoleColor systemColorBack;


        //---GameScreen
        //PlayerAMark
        [JsonInclude]
        public ConsoleColor playerAMarkColorBack;
        [JsonInclude]
        public ConsoleColor playerAMarkColorFront;
        [JsonInclude]
        public string       playerAMarkURL;
        //PlayerBMark
        public ConsoleColor playerBMarkColorBack;
        [JsonInclude]
        public ConsoleColor playerBMarkColorFront;
        [JsonInclude]
        public string       playerBMarkURL;
        [JsonInclude]
        public string       TikTakToeURL;
        //EmptyField

        [JsonInclude]
        public ConsoleColor EmptyMarkColorBack;
        [JsonInclude]
        public ConsoleColor EmptyMarkColorFront;
        [JsonInclude]
        public string       EmptyMarkURL;

        [JsonInclude]
        public string       boarderURL;
        [JsonInclude]
        public ConsoleColor boardColorFront;
        [JsonInclude]
        public ConsoleColor boardColorBack;

        //---Menu
        [JsonInclude]
        public ConsoleColor menuColorFront;
        [JsonInclude]
        public ConsoleColor menuColorBack;
        [JsonInclude]
        public ConsoleColor menuColorActive;
        [JsonInclude]
        public string       MenuURL;
        //---GameOver
        [JsonInclude]
        public string       gameOverWinnerURL;
        [JsonInclude]
        public string       gameOverDrawURL;
        [JsonInclude]
        public string       gameOverWinAURL;
        [JsonInclude]
        public string       gameOverWinBURL;
        [JsonInclude]
        public string       gameOverWinDURL;
    }

    
}
