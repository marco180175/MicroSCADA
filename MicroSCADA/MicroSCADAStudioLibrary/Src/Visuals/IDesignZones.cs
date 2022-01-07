using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    /*!
     * Interface para mostrar zonas dos objetos DinamicText Animation e Button e futuros
     */
    public interface IDesignZones
    {
        Bitmap GetZone(int index);
        int ZoneCount { get; }
        int Width { get; set; }
        int Height { get; set; }
        void Exchange(int Index1, int Index2);
        //CDesignSystem GetThis();//???
    }
}
