using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;
namespace MicroSCADACustomLibrary.Src
{
    public interface ICustomProject : ICustomObject
    {
        String Comment { get; set; }
        String FileName { get; }
        void SetFileName(String Value);
        ICustomHMI HMI { get; }
        ICustomDefaultScreenList Screens { get; }
        ICustomScreenList PopupScreens { get; }
        ICustomInternalTagList InternalTagList { get; }
        ICustomPropertyTagList PropertyTagList { get; }
        ICustomNetwork Network { get; }
        ICustomAlarmsManager AlarmsManager { get; }
        ICustomBitmapList BitmapList { get; }
        ICustomActionList ActionList { get; }
        ICustomBitArrayList BitArrayList { get; }
        ICustomProgramList ProgramList { get; }
        ICustomFontHMIList FontList { get; }
        ICustomPasswordList PasswordList { get; }
        void LinkObjects();          
        String HashMD5 { get; }
        void SetHashMD5(String Value);
        void Initialize();
    }

    public sealed class CCustomProject : Object       
    {
        public ICustomDefaultScreenList screens;
        public ICustomInternalTagList internalTagList;
        public ICustomNetwork network;
        public ICustomBitmapList bitmapList;
        public String fileName;
        public String comment;
        public Guid guid;
        public CCustomProject(Object AOwner)
            : base()
        {
            /*
            this.screens = new CCustomScreens(this,this);
            this.objectList.Add(this.screens);
            this.internalTagList = new CCustomInternalTagList(this,this);
            this.objectList.Add(this.internalTagList);
            this.network = new CCustomNetwork(this,this);
            this.objectList.Add(this.network);
            this.bitmaps = new CCustomBitmapList(this,this);
            this.objectList.Add(this.bitmaps);
            this.comment = string.Empty;
            this.fileName = string.Empty;
             */
        }
        
        public void SetFileName(String fileName) { this.fileName = fileName; }
        //public CCustomScreens getScreens() { return screens; }
        //public CCustomInternalTagList getInternalTagList() { return internalTagList; }
        //public CCustomNetwork getNetwork() { return network; }
        //public CCustomBitmapList getBitmaps() { return bitmaps; }
        
    }
}
