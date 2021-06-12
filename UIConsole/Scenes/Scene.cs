using System;
using System.Collections.Generic;
using System.Runtime.Versioning;

namespace UIConsole
{
    abstract class Scene
    {
        protected int mWindowSizeX;
        protected int mWindowSizeY;
        protected int mBufferSizeX;
        protected int mBufferSizeY;
        protected Resources MainResources = Resources.MainResources;
        protected List<Label> mLabelList;
        protected List<Button> mButtonList;
        protected byte mActiveButton;
        public Scene(int wx = 90, int wy = 50)
        {
            mWindowSizeX = wx;
            mWindowSizeY = wy;
            mBufferSizeX = wx;
            mBufferSizeY = wy;
            if (OperatingSystem.IsWindows()) WindowSize();
            mLabelList = new();
            mButtonList = new();
            mActiveButton = 0;
        }
        public Scene(int wx , int wy,int bx, int by)
        {
            mWindowSizeX = wx;
            mWindowSizeY = wy;
            mBufferSizeX = bx;
            mBufferSizeY = by;
            if (OperatingSystem.IsWindows()) WindowSize();
            mLabelList = new();
            mButtonList = new();
            mActiveButton = 0;
        }

        public abstract void Update();
        public virtual void Draw()
        { 
            foreach (var item in mLabelList)
            {
                item.Draw();
            }
            foreach (var item in mButtonList)
            {
                item.Draw();
            }
        }
        [SupportedOSPlatform("Windows")]
        protected void WindowSize()
        {
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(mBufferSizeX, mBufferSizeY);
            Console.SetWindowSize(mWindowSizeX, mWindowSizeY);
        }
    }
}
