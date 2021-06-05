using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole.Scenes
{
    class Options : Scene
    {
        private Config editConfig;
        private Label mPlayerAColorLabel;
        private ConsoleColor[] mForgroundColor = { ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Yellow,  ConsoleColor.Red, ConsoleColor.Magenta};
        private ConsoleColor[] mBackgroundColor = { ConsoleColor.DarkBlue, ConsoleColor.DarkCyan, ConsoleColor.DarkGreen, ConsoleColor.DarkYellow, ConsoleColor.DarkRed, ConsoleColor.DarkMagenta };
        private byte PlayerAColor;
        private byte PlayerBColor;
        
        public Options()
        {
            int _Y = 5;
            string dirIMG = "TTTIMG/";
            editConfig = MainResources.mainConfig;
            mLabelList.Add(new Label(2, Positioning.center, "____________MENU____________", MainResources.SystemColorFront, MainResources.SystemColorBack));
       
            //playerA
            mLabelList.Add(new Label(_Y, 10, "Player 1",MainResources.SystemColorFront, MainResources.SystemColorBack));
                mButtonList.Add(new Button(++_Y, 15, "Color:", () => { }));
                mPlayerAColorLabel = new Label(_Y, 25, "#####", editConfig.playerAMarkColorFront, editConfig.playerAMarkColorBack);
                mLabelList.Add(mPlayerAColorLabel);
            mLabelList.Add(new Label(_Y, 10, "Player 2", MainResources.SystemColorFront, MainResources.SystemColorBack));
                mButtonList.Add(new Button(++_Y, 15, "Color:", () => { }));
                mPlayerAColorLabel = new Label(_Y, 25, "#####", editConfig.playerAMarkColorFront, editConfig.playerAMarkColorBack);
                mLabelList.Add(mPlayerAColorLabel);



            editConfig.playerAMarkColorFront                 = ConsoleColor.Blue;
            editConfig.playerAMarkColorBack                     = ConsoleColor.DarkBlue;
            editConfig.playerAMarkURL                        = dirIMG + "FieldA.txt";
            //PlayerB
            editConfig.playerBMarkColorFront                 = ConsoleColor.Red;
            editConfig.playerBMarkColorBack                 = ConsoleColor.DarkRed;
            editConfig.playerBMarkURL                       = dirIMG + "FieldB.txt";
           
            editConfig.EmptyMarkColorFront              = ConsoleColor.Gray;
            editConfig.EmptyMarkColorBack                    = ConsoleColor.DarkGray;
            editConfig.EmptyMarkURL                     = dirIMG + "FieldE.txt";
          
            editConfig.boarderURL                   = dirIMG + "Boarder.txt"; ;
            editConfig.boardColorFront              = ConsoleColor.White;
            editConfig.boardColorBack               = ConsoleColor.DarkGray;
        
            editConfig.menuColorFront                   = ConsoleColor.White;
            editConfig.menuColorBack                = ConsoleColor.Black;
            editConfig.menuColorActive                  = ConsoleColor.DarkBlue;
          
            editConfig.gameOverWinnerURL                = dirIMG + "Winner.txt";
            editConfig.gameOverDrawURL                  = dirIMG + "Draw.txt";
            editConfig.gameOverWinAURL                   = dirIMG + "WinA.txt";
            editConfig.gameOverWinBURL                   = dirIMG + "WinB.txt";
            editConfig.gameOverWinDURL                  = dirIMG + "WinD.txt";

            //System
            editConfig.windowSizeY                  = 50;
            editConfig.windowSizeX                  = 90;
            editConfig.systemColorFront             = ConsoleColor.White;
            editConfig.systemColorAcent                 = ConsoleColor.Gray;
            editConfig.systemColorBack                   = ConsoleColor.Black;



            mButtonList.Add(new Button(10, Positioning.center, "SAVE AND QUIT", () => SceneManager.Instance.AddScene(new GameScreen(10))));
            //mButtonList.Add(new Button(12, Positioning.center, "Credits", () => SceneManager.Instance.RemoveScene(this)));
            mButtonList.Add(new Button(12, Positioning.center, "QUIT", () => SceneManager.Instance.RemoveScene(this)));

            mButtonList[mActiveButton].IsSelected = true;
        }

        private void ChangeColor() { 
        
        
        }

        public override void Update()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    mButtonList[mActiveButton].IsSelected = false;
                    mActiveButton = (byte)(mActiveButton == 0 ? mButtonList.Count - 1 : mActiveButton - 1);
                    mButtonList[mActiveButton].IsSelected = true;
                    break;
                case ConsoleKey.DownArrow:
                    mButtonList[mActiveButton].IsSelected = false;
                    mActiveButton = (byte)(mActiveButton == mButtonList.Count - 1 ? 0 : mActiveButton + 1);
                    mButtonList[mActiveButton].IsSelected = true;
                    break;
                case ConsoleKey.LeftArrow:
                    mButtonList[mActiveButton].IsSelected = false;
                    mActiveButton = (byte)(mActiveButton == mButtonList.Count - 1 ? 0 : mActiveButton + 1);
                    mButtonList[mActiveButton].IsSelected = true;
                    break;
                case ConsoleKey.RightArrow:
                    mButtonList[mActiveButton].IsSelected = false;
                    mActiveButton = (byte)(mActiveButton == mButtonList.Count - 1 ? 0 : mActiveButton + 1);
                    mButtonList[mActiveButton].IsSelected = true;
                    break;
                case ConsoleKey.Enter:
                    mButtonList[mActiveButton].Execute();
                    break;
            }
        }

    }
}
