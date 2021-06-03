using System;
using System.IO;
using TTTLogic;
namespace UIConsole
{
    class Program
    {   
        static void Main(string[] args)
        {
            FramesPerSecond FPS = new(0,0,ConsoleColor.White, ConsoleColor.Black);
            Console.CursorVisible = false;
            Console.SetWindowSize(1 , 1 );
            Console.SetBufferSize(90, 50);    
            Console.SetWindowSize(90, 50);
            SceneManager.Instance.AddScene(new Menu());
            while (!SceneManager.Instance.SceneListIsEmpty())
            {
                ///FPS.ShowFramesPerSecond();
                SceneManager.Instance.Draw();
                SceneManager.Instance.Update();               
            }
        }
    }
}
