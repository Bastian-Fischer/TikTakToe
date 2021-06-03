﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    class SceneManager
    { 
        private static SceneManager mInstance;
        private LinkedList<Scene> mSceneList; //X
       


        public static SceneManager Instance
        {
            get
            {
                if (mInstance is null) // wenn noch kein Objekt erstellt wurde holen wir das nach. "Lazy Initialization"
                    mInstance = new(); // das einzige new für den SceneManager das es je geben wird
                return mInstance;
            }
        }

        private SceneManager()
        {
            // niemand darf einen SceneManager erstellen, darum Private
            mSceneList = new();
        }



        public void Update()//X
        {
            mSceneList.Last.Value.Update();
        }

        public void Draw()//X
        {
            //TODO implement
            mSceneList.Last.Value.Draw();
        }

        public void AddScene(Scene SceneToAdd)//X
        {
            mSceneList.AddLast(SceneToAdd);
        }

        public void RemoveScene(Scene SceneToRemove)//X
        {
            //TODO implement
            mSceneList.Remove(SceneToRemove);
        }

        public bool SceneListIsEmpty() {
            if(mSceneList.Count == 0) return true;
            else return false;
        }
    }
}