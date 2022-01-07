using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.HMI;
//using MicroSCADARuntimeLibrary.Src.Threads;

//using MicroSCADARuntimeLibrary.Src.VirtualMicroProcessor;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeProject : CRuntimeObject, ICustomProject
    {
        protected CCustomProject customProject;
        //protected CCommunicationManager commManager;
        private CRuntimeAlarmsManager alarmsManager;
        private CRuntimeScreenList screens;
        private CRuntimePopupScreenList popupScreens;
        private CRuntimeInternalTagList internalTagList;
        private CRuntimePropertyTagList propertyTagList;
        private CRuntimeBitmapList bitmapList;
        private CRuntimeNetwork network;
        private CRuntimeActionList actionList;
        //public CVirtualMCU MCU;
        //private String fileName;
        //private String comment;
        //private static CRuntimeProject instance = null;
        /*!
         * Construtor
         */
        public CRuntimeProject(Object AOwner, Control Parent)
            : base(AOwner)
        {
            this.customProject = new CCustomProject(AOwner);            
            this.Name ="Project";
            
            //
            this.screens = new CRuntimeScreenList(this, this, Parent);
            this.customProject.screens = this.screens;
            this.ObjectList.Add(this.screens);
            //
            this.popupScreens = new CRuntimePopupScreenList(this, this, Parent);
            //this.customProject.screens = this.screens;
            this.ObjectList.Add(this.popupScreens);
            //
            this.internalTagList = new CRuntimeInternalTagList(this, this);
            this.customProject.internalTagList = this.internalTagList;
            this.ObjectList.Add(this.internalTagList);
            //
            this.propertyTagList = new CRuntimePropertyTagList(this, this);
            //this.customProject.propertyTagList = this.internalTagList;
            this.ObjectList.Add(this.propertyTagList);
            //
            this.network = new CRuntimeNetwork(this, this);
            this.customProject.network = this.network;
            this.ObjectList.Add(this.network);
            //
            this.actionList = new CRuntimeActionList(this, this);
            this.ObjectList.Add(this.actionList);
            //
            this.bitmapList = new CRuntimeBitmapList(this, this);
            this.customProject.bitmapList = this.bitmapList;
            this.ObjectList.Add(this.bitmapList);
            //
            this.alarmsManager = new CRuntimeAlarmsManager(this,this);
            this.ObjectList.Add(this.alarmsManager);
        }
        public ICustomAlarmsManager AlarmsManager
        {
            get { return this.alarmsManager; }
        }
        /*!
         * Destrutor
         */
        public override void Dispose()
        {
            //this.bitmapList.Dispose();
            //this.screens.Dispose();
            base.Dispose();
        }
        //public Guid GUID
        //{
        //    get { return this.customProject.guid; }
        //}
        //public void SetGUID(Guid Value)
        //{
        //    this.customProject.guid = Value;
        //}
        /*!
         * Limpa objetos e configuraçoes
         */
        public void Clear()
        {
            //this.comment = string.Empty;
            //this.fileName = string.Empty;
            //this.screens.Clear();
            //this.bitmapList.Close();
        }
        //Propriedades
        public String Comment
        {
            get { return this.customProject.comment; }
            set { this.customProject.comment = value; }
        }
        public String FileName
        {
            get { return this.customProject.fileName; }
        }
        public void SetFileName(String Value) { customProject.fileName = Value; }
        //!
        public ICustomHMI HMI
        {
            get { return null; }
        }
        //!
        public ICustomDefaultScreenList Screens
        {
            get { return this.screens; }
        }
        //!
        public ICustomScreenList PopupScreens
        {
            get { return this.popupScreens; }
        } 
        //!
        public ICustomNetwork Network
        {
            get { return this.network; } 
        }
        public ICustomInternalTagList InternalTagList
        {
            get { return this.internalTagList; }
        }
        public ICustomPropertyTagList PropertyTagList
        {
            get { return this.propertyTagList; }
        }
        public ICustomBitmapList BitmapList
        {
            get { return this.bitmapList; }
        }
        public ICustomActionList ActionList
        {
            get { return this.actionList; }
        }
        public ICustomBitArrayList BitArrayList { get { return null; } }
        public void Initialize() { }
        /*!
         * 
         */
        public void LinkObjects()
        {
            screens.LinkObjects();
            //popupScreens.LinkObjects();
            propertyTagList.LinkObjects();
            actionList.LinkObjects();
        }
        private String hashMD5;
        public String HashMD5
        {
            get { return this.hashMD5; }
        }
        public void SetHashMD5(String Value)
        {
            this.hashMD5 = Value;
        }
        
    }
}
