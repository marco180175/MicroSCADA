using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public static class CSysUtils
    {
        public static string RemoveDirectory(string Path, int Count)
        {
            string temp = Path;
            int index, count = Count;
            while ((count--) > 0)
            {
                index = temp.LastIndexOf("\\");
                temp = temp.Remove(index);
                index = temp.LastIndexOf("\\");
                temp = temp.Remove(index + 1);
            }
            return temp;
        }
        /*!
         * Converte escalas com função 'y=ax+b'
         * @param Value Valor a ser convertido de x para y
         * @param X0 Valor minimo de entada
         * @param X1 Valor maximo de entada
         * @param Y0 Valor minimo de saida
         * @param Y1 Valor maximo de saida
         * @return Valor saida convertido
         */
        public static double ConvertScale(double Value, double X0, double X1, double Y0, double Y1)
        {
            double a, b, x, y;
            a = (Y1 - Y0) / (X1 - X0);
            b = Y1 - a * X1;
            x = Value;
            y = (a * x + b);
            return y;
        }
    }
}
