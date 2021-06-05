using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    class GameOver : Scene
    {
        protected readonly GameScreen mGame;
        protected readonly TTTLogic.Logic GameLogic;
        protected TTTLogic.TurnResult mTurnWinner;

        public GameOver(TTTLogic.Logic _GameLogic, TTTLogic.TurnResult _mTurnWinner, GameScreen _GameScreen ) 
        {
            Console.Clear();
            mActiveButton = 1;         
            mButtonList.Add(new Button(10 , Positioning.center, "BACK TO MENU", ()=>ExitToMenu()));
            mButtonList.Add(new Button(11, Positioning.center, "NEXT GAME", () => ResetGame()));
            mButtonList[mActiveButton].IsSelected = true;
            mTurnWinner = _mTurnWinner;
            mGame = _GameScreen;
            GameLogic = _GameLogic;
            int listLength = GameLogic.GetScoreList().Count;
            switch (mTurnWinner)
            {
                case TTTLogic.TurnResult.WinX:
                    mLabelList.Add(new Label(1, Positioning.left, MainResources.winX, MainResources.PlayerBColorBack, MainResources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.center, MainResources.winner, MainResources.PlayerBColorFront, MainResources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.right, MainResources.winX, MainResources.PlayerBColorBack, MainResources.SystemColorBack));
                    break;
                case TTTLogic.TurnResult.WinO:
                    mLabelList.Add(new Label(1, Positioning.left, MainResources.winO, MainResources.PlayerAColorBack, MainResources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.center, MainResources.winner, MainResources.PlayerAColorFront, MainResources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.right, MainResources.winO, MainResources.PlayerAColorBack, MainResources.SystemColorBack));
                    break;
                case TTTLogic.TurnResult.Draw:
                    mLabelList.Add(new Label(1, Positioning.left, MainResources.winD, MainResources.SystemColorAcent, MainResources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.center, MainResources.draw, MainResources.SystemColorFront, MainResources.SystemColorBack));
                    mLabelList.Add(new Label(1, Positioning.right, MainResources.winD, MainResources.SystemColorAcent, MainResources.SystemColorBack));
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
                        mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "GAME "+ (counter + 1) + "  -  Winner: X", MainResources.PlayerBColorFront, MainResources.SystemColorBack));
                        break;
                    case (int)TTTLogic.TurnResult.WinO:
                        mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "GAME "+ (counter + 1) + "  -  Winner: O", MainResources.PlayerAColorFront, MainResources.SystemColorBack));
                        break;
                    case (int)TTTLogic.TurnResult.Draw:
                        mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "GAME "+ (counter + 1) + "  -       DRAW", MainResources.SystemColorFront, MainResources.SystemColorBack));
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
