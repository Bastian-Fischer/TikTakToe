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
                for (int x = 0; x < width; x++)
                    _return[y,x] = ' ';

            for (int counterY = 0; counterY < stringARR.Length; counterY++)
                for (int counterX = 0; counterX < stringARR[counterY].Length; counterX++)
                    _return[counterY, counterX] = stringARR[counterY][counterX];

            return _return;
        }
        public static char[] CharArrFirstOneFromLine(string _url) 
        {
            char[] _return;
            string[] stringARR = File.ReadAllLines(_url);
            _return = new char[stringARR.Length];

            for  (int counter = 0; counter < stringARR.Length; counter++)
                _return[counter] = stringARR[counter][0];
 
            return _return;
        }
    }
}
