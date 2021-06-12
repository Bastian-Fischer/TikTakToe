using System;
using System.IO;
using System.Runtime.Versioning;

namespace UIConsole
{
    class Program
    {   
    private static bool showInfos = false;
        static void Main(string[] args)
        {
            
            
            if (!File.Exists("System/main.json"))
            {
                Resources.MainResources.SaveConfigJson(Resources.MainResources.CreateDefault(), "main");
            }
            else
            {
                Resources.MainResources.mainConfig = Resources.MainResources.LoadConfigJson("System/main.json");
            }

            if (args.Length > 0) 
            {
                foreach (var item in args)
                {
                    if (item == "showInfo") showInfos = true;
                }      
            }

            FramesPerSecond FPS = new(0, 0, ConsoleColor.White, ConsoleColor.Black);
            FramesPerSecond updateTime = new(0, 10, ConsoleColor.Yellow, ConsoleColor.Black, "Draw");
            FramesPerSecond drawTime = new(0, 40, ConsoleColor.Yellow, ConsoleColor.Black, "Update");
            Console.CursorVisible = false;
      
            if (OperatingSystem.IsWindows())WindowSize();
            SceneManager.Instance.AddScene(new Menu());

            while (!SceneManager.Instance.SceneListIsEmpty())
            {
                if(showInfos) FPS.ShowFramesPerSecond();
                if (showInfos) drawTime.HowLongStart();
                SceneManager.Instance.Draw();
                if (showInfos) drawTime.HowLongEnd();
                if (showInfos) updateTime.HowLongStart();
                SceneManager.Instance.Update();
                if (showInfos) updateTime.HowLongEnd();
            }
        }
        [SupportedOSPlatform("Windows")]
        private static void WindowSize()
        {
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(90, 50);
            Console.SetWindowSize(90, 50);
        }
    }
}
