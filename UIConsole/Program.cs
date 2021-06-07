using System;
using System.IO;
using System.Threading.Tasks;
using TTTLogic;
namespace UIConsole
{
    class Program
    {   
        static void Main(string[] args) //todo remove unused parameter
        {

            //Resources.SaveConfigJson(Resources.mainConfig, "main");
            //Resources.mainConfig.
            FramesPerSecond FPS = new(0,0,ConsoleColor.White, ConsoleColor.Black);
            Console.CursorVisible = false;
            Console.SetWindowSize(1 , 1 );
            Console.SetBufferSize(90, 50);    
            Console.SetWindowSize(90, 50);
            SceneManager.Instance.AddScene(new Menu());
            ////So nicht xD
            //void MainDraw()
            //{
            //    while (!SceneManager.Instance.SceneListIsEmpty())
            //    {
            //        SceneManager.Instance.Draw();
            //    }
            //}
            //void MainUpdate()
            //{
            //    while (!SceneManager.Instance.SceneListIsEmpty())
            //    {
            //        SceneManager.Instance.Update();
            //    }
            //}

            //Parallel.Invoke(() => MainDraw(), () => MainUpdate());



            while (!SceneManager.Instance.SceneListIsEmpty())
            {
                FPS.ShowFramesPerSecond();
                
                SceneManager.Instance.Draw();
                FPS.HowLongStart();
                SceneManager.Instance.Update();
                FPS.HowLongEnd();              
            }

        }
    }
}
