using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    class LoadAs
    {
        public static string[] StringArr(string _url)
        {
            string[] _return;
            _return = File.ReadAllLines(_url);
            return _return;
        }
        public static char[,] CharMultidimensionalArr(string _url)
        {
            char[,] _return;
            string[] stringARR = File.ReadAllLines(_url);
            int width = 0;
            int height = 0;
            foreach (var item in stringARR)
            {
                height++;
               int lengthX = item.Length;
                if (width < lengthX) width = lengthX;
            }
            _return =new char[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _return[y,x] = ' ';
                }
            }
            int countY = 0;
            foreach (var item in stringARR)
            {  
                for (int i = 0; i < item.Length; i++)
                {
                    _return[countY, i] = item[i];
                }
                countY++;
            }
            return _return;
        }
        public static char[] CharArrFirstOneFromLine(string _url) 
        {
            char[] _return;
            string[] stringARR = File.ReadAllLines(_url);
            _return = new char[stringARR.Length];
            int counter = 0;
            foreach (var item in stringARR)
            {
                _return[counter] = item[0];
                counter++;
            }
                
            return _return;
        }
    }
}
