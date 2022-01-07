using System;
using System.Collections.Generic;
using System.Text;

namespace MicroSCADACustomLibrary.Src.Visuals
{
    public interface ICustomLocation
    {
        int Left { get; set; }
        int Top { get; set; }
    }

    public interface ICustomSize 
    {
        int Width { get; set; }
        int Height { get; set; }
    }

    public interface ICustomView : ICustomLocation, ICustomSize //: ICustomObject
    {
        
    }

    public abstract class CCustomView
    {        
        protected bool selected;
        //Construtor
        public CCustomView()
            : base()
        {
            this.selected = false;            
        }
        //Destrutor
        ~CCustomView()
        {
        }
        
        /*!
         * 
         * @param value         
         */
        protected virtual void SetSelected(bool value)
        {
            selected = value;
            if (selected)
            {
                //if(treeNode != null)
                 //   treeNode.TreeView.SelectedNode = treeNode;
            }            
        }
    }
}
