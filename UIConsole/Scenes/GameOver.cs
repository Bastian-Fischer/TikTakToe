using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    class GameOver : Scene
    {
        
        private readonly GameScreen mGame;
        private readonly TTTLogic.Logic GameLogic;


        public GameOver(TTTLogic.Logic _GameLogic, GameScreen _GameScreen ) 
        {
            Console.Clear();
            mActiveButton = 1;         
            mButtonList.Add(new Button(10 , Positioning.center, "BACK TO MENU", ()=>ExitToMenu()));
            mButtonList.Add(new Button(11, Positioning.center, "NEXT GAME", () => ResetGame()));
            mButtonList[mActiveButton].IsSelected = true;
            mGame = _GameScreen;
            GameLogic = _GameLogic;
            switch ((int)GameLogic.GameState)
            {
                case (int)TTTLogic.TurnResult.WinX:
                    mLabelList.Add(new Label(1, Positioning.left, Resources.winX, Resources.PlayerBColorBack, Resources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.center, Resources.winner, Resources.PlayerBColorFront, Resources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.right, Resources.winX, Resources.PlayerBColorBack, Resources.SystemColorBack));
                    break;
                case (int)TTTLogic.TurnResult.WinO:
                    mLabelList.Add(new Label(1, Positioning.left, Resources.winO, Resources.PlayerAColorBack, Resources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.center, Resources.winner, Resources.PlayerAColorFront, Resources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.right, Resources.winO, Resources.PlayerAColorBack, Resources.SystemColorBack));
                    break;
                case (int)TTTLogic.TurnResult.Draw:
                    mLabelList.Add(new Label(1, Positioning.left, Resources.winD, Resources.SystemColorAcent, Resources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.center, Resources.draw, Resources.SystemColorFront, Resources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.right, Resources.winD, Resources.SystemColorAcent, Resources.SystemColorBack));
                    break;               
            }
            int winnerCounterY = 15;
            mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "SCORE LIST", ConsoleColor.White, ConsoleColor.Black));
            mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "═════════════════════════════════════════════════════════════", ConsoleColor.Gray, ConsoleColor.Black));
            for (int counter = GameLogic.GetScoreList().Count-1; counter >= 0; counter--)
            {

                switch ((int)GameLogic.GetScoreList()[counter]) 
                {
                    case (int)TTTLogic.TurnResult.WinX:
                        mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "GAME "+ (counter + 1) + "  -  Winner: X", Resources.PlayerBColorFront, Resources.SystemColorBack));
                        break;
                    case (int)TTTLogic.TurnResult.WinO:
                        mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "GAME "+ (counter + 1) + "  -  Winner: O", Resources.PlayerAColorFront, Resources.SystemColorBack));
                        break;
                    case (int)TTTLogic.TurnResult.Draw:
                        mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "GAME "+ (counter + 1) + "  -       DRAW", Resources.SystemColorFront, Resources.SystemColorBack));
                        break;
                }
            }
        }
        private void ResetGame() 
        {
            Console.Clear();
            GameLogic.Reset();
            mGame.DrawBoarder();
            SceneManager.Instance.RemoveScene(this);
        }
        private void ExitToMenu() 
        {
            Console.Clear();
            SceneManager.Instance.RemoveScene(mGame);
            SceneManager.Instance.RemoveScene(this);
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
                case ConsoleKey.Enter:
                    mButtonList[mActiveButton].Execute();
                    break;
            }
        }
    }
}
