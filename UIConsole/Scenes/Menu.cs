
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{ 
    class Menu : Scene
    {

        public Menu()
        {
            mLabelList.Clear();
            mButtonList.Clear();
            Console.Clear();
            mLabelList.Add(new Label( MainResources.Menu ,1, Positioning.center, MainResources.SystemColorFront, MainResources.SystemColorBack));
            mButtonList.Add(new Button(10, Positioning.center, "Start New Game",() => SceneManager.Instance.AddScene(new GameScreen(10))));
            //mButtonList.Add(new Button(12, Positioning.center, "Credits", () => SceneManager.Instance.RemoveScene(this)));
            mButtonList.Add(new Button(12, Positioning.center, "Quit", () => SceneManager.Instance.RemoveScene(this)));

            mButtonList[mActiveButton].IsSelected = true;
        }

        public override void Update()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    mButtonList[mActiveButton].IsSelected = false;
                    mActiveButton = (byte)(mActiveButton == 0 ? mButtonList.Count - 1 : mActiveButton - 1); //TODO, was wenn wir schon ganz oben sind?
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
