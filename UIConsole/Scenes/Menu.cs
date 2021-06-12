using System;

namespace UIConsole
{ 
    class Menu : Scene
    {
        private int mStartLine = 10;
        public Menu()
        {
            
            mLabelList.Clear();
            mButtonList.Clear();
            Console.Clear();

            mLabelList.Add(new Label( MainResources.Menu ,1, Positioning.center, MainResources.SystemColorFront, MainResources.SystemColorBack));
            mButtonList.Add(new Button(mStartLine, Positioning.center, "Start New Game 3X3",() => SceneManager.Instance.AddScene(new GameScreen(10,3,3,3))));
            mButtonList.Add(new Button(mStartLine += 2, Positioning.center, "Start New Game 5x5", () => SceneManager.Instance.AddScene(new GameScreen(10,5,5,4))));
            mButtonList.Add(new Button(mStartLine += 2, Positioning.center, "Start New Game 5x9", () => SceneManager.Instance.AddScene(new GameScreen(10, 5, 9, 4))));
            //mButtonList.Add(new Button(12, Positioning.center, "Credits", () => SceneManager.Instance.RemoveScene(this)));
            mButtonList.Add(new Button(mStartLine += 2, Positioning.center, "Quit", () => SceneManager.Instance.RemoveScene(this)));

            mButtonList[mActiveButton].IsSelected = true;
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

                case ConsoleKey.Enter: //todo change to default, let ui element handle input (button tut button dinge)
                    mButtonList[mActiveButton].Execute();
                    break;
            }
        }
    }
}
