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
        private readonly string[] winner = LoadAs.StringArr("TTTIMG/Winner.txt");
        private readonly string[] winnerX  = LoadAs.StringArr("TTTIMG/WinB.txt");
        private readonly string[] winnerO = LoadAs.StringArr("TTTIMG/WinA.txt");
        private readonly string[] line = LoadAs.StringArr("TTTIMG/WinD.txt");
        private readonly string[] draw = LoadAs.StringArr("TTTIMG/Draw.txt");

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
                    mLabelList.Add(new Label(1, Positioning.left, winnerX, ConsoleColor.Red, ConsoleColor.Black));
                    mLabelList.Add(new Label(1, Positioning.center, winner, ConsoleColor.Red, ConsoleColor.Black));
                    mLabelList.Add(new Label(1, Positioning.right, winnerX, ConsoleColor.Red, ConsoleColor.Black));
                    break;
                case (int)TTTLogic.TurnResult.WinO:
                    mLabelList.Add(new Label(1, Positioning.left, winnerO, ConsoleColor.Blue, ConsoleColor.Black));
                    mLabelList.Add(new Label(1, Positioning.center, winner, ConsoleColor.Blue, ConsoleColor.Black));
                    mLabelList.Add(new Label(1, Positioning.right, winnerO, ConsoleColor.Blue, ConsoleColor.Black));
                    break;
                case (int)TTTLogic.TurnResult.Draw:
                    mLabelList.Add(new Label(1, Positioning.left, line, ConsoleColor.White, ConsoleColor.Black));
                    mLabelList.Add(new Label(1, Positioning.center, draw, ConsoleColor.White, ConsoleColor.Black));
                    mLabelList.Add(new Label(1, Positioning.right, line, ConsoleColor.White, ConsoleColor.Black));
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
                        mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "GAME "+ (counter + 1) + "  -  Winner: X", ConsoleColor.Red , ConsoleColor.Black));
                        break;
                    case (int)TTTLogic.TurnResult.WinO:
                        mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "GAME "+ (counter + 1) + "  -  Winner: O", ConsoleColor.Blue, ConsoleColor.Black));
                        break;
                    case (int)TTTLogic.TurnResult.Draw:
                        mLabelList.Add(new Label(winnerCounterY++, Positioning.center, "GAME "+ (counter + 1) + "  -    DRAW   ", ConsoleColor.White, ConsoleColor.Black));
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
