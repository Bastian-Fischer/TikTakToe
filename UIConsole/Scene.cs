using System.Collections.Generic;

namespace UIConsole
{
    abstract class Scene
    {
        protected List<Label> mLabelList;
        protected List<Button> mButtonList;
        protected byte mActiveButton;
        public Scene() {
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
    }
}
