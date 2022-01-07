using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using MicroSCADAStudioLibrary.Src.Tags;
using MicroSCADACustomLibrary.Src.Visuals;

namespace MicroSCADAStudioLibrary.Src.Visuals
{
    /*!
     *  
     */
    public interface IDesignCollectionItem 
    {
        float MaxValue { get; set; }
        float MinValue { get; set; }
    }
    /*!
     *  
     */
    public interface IDesignCollection
    {
        ArrayList ObjectList { get; }
        void Exchange(int Index1, int Index2);
        IDesignCollectionItem NewItem();
    }
    /*!
     *  
     */    
    interface IDesignZoneCollection : IDesignZones
    {
        CDinamicType DinamicType { get; set; }        
    }    
}
