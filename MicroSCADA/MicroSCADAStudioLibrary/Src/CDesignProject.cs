using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.HMI;
using MicroSCADAStudioLibrary.Src.TypeConverter;
using MicroSCADAStudioLibrary.Src;
using MicroSCADAStudioLibrary.Src.HMI;

namespace MicroSCADAStudioLibrary.Src
{    

    public class CDesignProject : CDesignObject, ICustomProject, ITreeAdapter
    {
        private CCustomProject customProject;
        private CDesignHMI hmi;
        private CDesignScreenList screens;
        private CDesignPopupScreenList popupScreens;
        private CDesignInternalTagList internalTagList;
        private CDesignPropertyTagList propertyTagList;
        private CDesignNetwork network;
        private CDesignBitmapList bitmapList;
        private CDesignAlarmsManager alarmsManager;
        private CDesignActionList actionList;
        private CDesignPasswordList passwordList;
        private CDesignBitArrayList bitArrayList;
        private CDesignProgramList programList;
        private CDesignFontHMIList fontList;
        public MouseEventHandler MouseMove;//!< Ponteiro para evento OnMouseMove
        public KeyEventHandler KeyDown;//!< Ponteiro para evento OnKeyDown
        public EventHandler DoubleClick;//!< Ponteiro para evento OnDoubleClick
        private static CDesignProject instance = null;        
        /*!
         * Construtor
         */
        private CDesignProject(Object AOwner)
            : base(AOwner)
        {
            this.customProject = new CCustomProject(AOwner);
            this.customProject.guid = Guid.Empty;
            this.Name = "Project";
            this.imageIndex = 8;
            //
            hmi = new CDesignHMI(this, this);
            passwordList = new CDesignPasswordList(this, this);
            bitArrayList = new CDesignBitArrayList(this, this);
            programList = new CDesignProgramList(this, this);
            fontList = new CDesignFontHMIList(this, this);
            //
            this.screens = new CDesignScreenList(this, this);            
            //
            this.popupScreens = new CDesignPopupScreenList(this, this);            
            //
            this.internalTagList = new CDesignInternalTagList(this, this); 
            //
            this.propertyTagList = new CDesignPropertyTagList(this, this);
            //
            this.network = new CDesignNetwork(this, this);                       
            //
            this.alarmsManager = new CDesignAlarmsManager(this, this);            
            //
            this.bitmapList = new CDesignBitmapList(this, this);
            //
            this.actionList = CDesignActionList.getInstance(this, this);
        }
        /*!
         * Destrutor
         */
        public override void Dispose()
        {
            Clear();
            OnDelItem(EventArgs.Empty);
            base.Dispose();
        }
                
        public static CDesignProject getInstance()
        {
            if(instance == null)
                instance = new CDesignProject(null);
            return instance;
        }
        
        /*!
         * Limpa objetos e configuraçoes
         */
        public void Clear()
        {
            Name = "Project";
            hashMD5 = "";
            customProject.fileName = "";
            Description = "";
            customProject.guid = Guid.Empty;
            screens.Clear();
            popupScreens.Clear();
            internalTagList.Clear();
            propertyTagList.Clear();
            network.Clear();            
            //alarmsManager.Clear();
            bitmapList.Clear();
        }
        new public Guid GUID 
        {
            get { return this.customProject.guid; }
        }
        public override void SetGUID(Guid Value)
        {
            this.customProject.guid = Value;
        }
        //!
        [Category("Accessibility")]
        [Editor(typeof(CTextEditorPreset), typeof(UITypeEditor))]
        public string Comment
        {
            get { return this.customProject.comment; }
            set { this.customProject.comment = value; }
        }
        //!
        public String FileName
        {
            get { return customProject.fileName; }
        }
        
        public void SetFileName(String Value) { customProject.fileName = Value; }
        public CDesignCustomScreenList GetScreensObject()
        {
            return this.screens;
        }
        public CDesignCustomScreenList GetPopupScreensObject()
        {
            return this.popupScreens;
        }
        [Browsable(false)]
        public ICustomHMI HMI
        {
            get { return hmi; }
        }
        [Browsable(false)]
        public ICustomBitArrayList BitArrayList
        {
            get { return bitArrayList; }
        }
        public ICustomPasswordList PasswordList { get { return passwordList; } }
        public ICustomProgramList ProgramList { get { return programList; } }
        public ICustomFontHMIList FontList { get { return fontList; } }
        [Browsable(false)]
        public ICustomDefaultScreenList Screens
        {
            get { return this.screens; }
        }
        
        [Browsable(false)]
        public ICustomScreenList PopupScreens
        {
            get { return this.popupScreens; }
        }
        [Browsable(false)]
        public ICustomAlarmsManager AlarmsManager
        {
            get { return this.alarmsManager; }
        }
        [Browsable(false)]
        public ICustomNetwork Network
        {
            get { return this.network; }
        }
        [Browsable(false)]
        public ICustomInternalTagList InternalTagList
        {
            get { return internalTagList; }
        }
        [Browsable(false)]
        public ICustomPropertyTagList PropertyTagList
        {
            get { return propertyTagList; }
        }
        [Browsable(false)]
        public ICustomBitmapList BitmapList
        {
            get { return bitmapList; }
        }
        [Browsable(false)]
        public ICustomActionList ActionList
        {
            get { return actionList; }
        }
        
        /*!
         * Cria novo projeto.
         */
        public void New(String fileName) 
        {
            this.customProject.guid = Guid.NewGuid();
            this.customProject.SetFileName(fileName);
            this.bitmapList.New(fileName);
        }

        //public void Initialize() { }
        /*!
         * 
         */
        public override void LinkObjects()
        {
            screens.LinkObjects();
            popupScreens.LinkObjects();
            propertyTagList.LinkObjects();
            actionList.LinkObjects();
        }
        
        [Category("Accessibility")]
        new public String Name
        {
            get { return this.customObject.name; }
            set
            {
                this.customObject.name = value;
                OnSetObjectName(new SetNameEventArgs(this.customObject.name));
            }
        }

        public event SetNameEventHandler SetObjectName;
        protected void OnSetObjectName(SetNameEventArgs e)
        {
            if (this.SetObjectName != null)
                this.SetObjectName(this, e);
        }
        
        public event AddItemEventHandler AddItem;
        protected void OnAddItem(AddItemEventArgs e)
        {
            if (this.AddItem != null)
                this.AddItem(this, e);
        }
        public event DelItemEventHandler DelItem;
        private void OnDelItem(EventArgs e)
        {
            if (this.DelItem != null)
                this.DelItem(this, e);
        }
        public event AddItemEventHandler SetObjectIcon;
        protected void OnSetObjectIcon(AddItemEventArgs e)
        {
            if (this.SetObjectIcon != null)
                this.SetObjectIcon(this, e);
        }
        public event ExchangeEventHandler ExchangeObject;
        public event SelectedObjectEventHandler SelectedObject;
        /*!
         * 
         */
        public void Initialize(Control Parent) { }
        public void Initialize()         
        {
            this.ObjectList.Add(this.hmi);
            this.OnAddItem(new AddItemEventArgs(hmi, hmi.ImageIndex));
            //
            this.ObjectList.Add(this.screens);
            this.OnAddItem(new AddItemEventArgs(screens, screens.ImageIndex));
            //            
            this.ObjectList.Add(this.popupScreens);
            this.OnAddItem(new AddItemEventArgs(popupScreens, popupScreens.ImageIndex));
            //
            this.ObjectList.Add(this.passwordList);
            this.OnAddItem(new AddItemEventArgs(passwordList, passwordList.ImageIndex));
            //                        
            this.ObjectList.Add(this.internalTagList);
            this.OnAddItem(new AddItemEventArgs(internalTagList, internalTagList.ImageIndex));
            //                        
            this.ObjectList.Add(this.propertyTagList);
            this.OnAddItem(new AddItemEventArgs(propertyTagList, propertyTagList.ImageIndex));
            //
            this.ObjectList.Add(this.network);
            this.OnAddItem(new AddItemEventArgs(network, network.ImageIndex));
            //            
            this.ObjectList.Add(this.bitArrayList);
            this.OnAddItem(new AddItemEventArgs(bitArrayList, bitArrayList.ImageIndex));
            //
            this.ObjectList.Add(this.alarmsManager);
            this.OnAddItem(new AddItemEventArgs(alarmsManager, alarmsManager.ImageIndex));
            //
            this.ObjectList.Add(this.programList);
            this.OnAddItem(new AddItemEventArgs(programList, programList.ImageIndex));
            //
            this.ObjectList.Add(this.actionList);
            this.OnAddItem(new AddItemEventArgs(actionList, actionList.ImageIndex));
            //
            this.ObjectList.Add(this.bitmapList);
            this.OnAddItem(new AddItemEventArgs(bitmapList, bitmapList.ImageIndex));
            //
            this.ObjectList.Add(this.fontList);
            this.OnAddItem(new AddItemEventArgs(fontList, fontList.ImageIndex));
        }
        private String hashMD5;
        [Browsable(false)]
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
