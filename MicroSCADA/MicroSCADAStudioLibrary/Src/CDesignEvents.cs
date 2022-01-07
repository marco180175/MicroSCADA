using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MicroSCADAStudioLibrary.Src
{
    
    
    public class SetNameEventArgs : EventArgs
    {
        private String name;
        public SetNameEventArgs(String Name)
        {
            this.name = Name;
        }
        public String Name { get { return this.name; } }
    }
    
    public delegate void SetNameEventHandler(Object sender, SetNameEventArgs e);    

    public class AddItemEventArgs : EventArgs
    {
        private ITreeAdapter objectAdapter;
        private int imageIndex;
        public AddItemEventArgs(ITreeAdapter objectAdapter, int imageIndex)
        {
            this.objectAdapter = objectAdapter;
            this.imageIndex = imageIndex;
        }
        public ITreeAdapter ObjectAdapter { get { return this.objectAdapter; } }
        public int ImageIndex { get { return this.imageIndex; } }
    }

    public class ShowItemEventArgs : EventArgs
    {
        private Form m_form;
        public ShowItemEventArgs(Form form)
        {
            m_form = form;
        }
        public Form FormItem { get { return this.m_form; } }
    }

    public class ExchangeEventArgs : EventArgs
    {
        private int index1;
        private int index2;
        public ExchangeEventArgs(int Index1, int Index2)
        {
            index1 = Index1;
            index2 = Index2;
        }
        public int Index1 { get { return this.index1; } }
        public int Index2 { get { return this.index2; } }
    }

    public delegate void AddItemEventHandler(Object sender, AddItemEventArgs e);

    public delegate void DelItemEventHandler(Object sender, EventArgs e);

    public delegate void ShowItemEventHandler(Object sender, ShowItemEventArgs e);

    public delegate void ExchangeEventHandler(Object sender, ExchangeEventArgs e);

    public class SelectedObjectEventArgs : EventArgs
    {
        private bool value;
        
        public SelectedObjectEventArgs(bool Value)
        {
            this.value = Value;
            
        }
        public bool Value
        {
            get { return this.value; }
        }
        
    }
    public delegate void SelectedObjectEventHandler(object sender, SelectedObjectEventArgs e);

    public interface ITreeAdapter
    {
        String Name { get; set; }
        event SetNameEventHandler SetObjectName;
        event AddItemEventHandler SetObjectIcon;
        event AddItemEventHandler AddItem;
        event DelItemEventHandler DelItem;
        event ExchangeEventHandler ExchangeObject;
        event SelectedObjectEventHandler SelectedObject;
        void Initialize(Control Parent);
        void Initialize();
    }

    public interface IPageAdapter : ITreeAdapter
    {
        event ShowItemEventHandler ShowItem;
    }

    #region suporte a propriedades modificadas em runtime
    //! ( ActionProperty ) TODO:MARCA PROPRIEDADE QUE PODERA SER USADA EM RUNTIME

    /*!
     * Extende classe Attribute e cria automaticamente [ActionProperty]
     */
    public class ActionPropertyAttribute : Attribute
    { }

    /*!
     * Extende Type para mostrar propriedades apenas com atributos = [ActionProperty]
     */
    public static class TypeExtensions
    {
        public static PropertyInfo[] GetFilteredProperties(this Type type)
        {
            return type.GetProperties().Where(pi => Attribute.IsDefined(
                                              pi, typeof(ActionPropertyAttribute))).ToArray();
        }
    }
    #endregion
}
