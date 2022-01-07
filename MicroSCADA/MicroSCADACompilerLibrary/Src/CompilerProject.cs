using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;
using MicroSCADACustomLibrary.Src.HMI;
namespace MicroSCADACompilerLibrary.Src
{
    public class CCompilerProject : CCompilerObject, ICustomProject
    {
        private string fileName;
        private string hashMD5;
        private CCompilerHMI hmi;
        private CCompilerNetwork network;
        private CCompilerBitArrayList bitArrayList;
        private CCompilerBitmapList bitmapList;
        private CCompilerProgramList programList;
        private CCompilerFontHMIList fontList;
        private CCompilerPasswordList passwordList;
        public CCompilerProject()
            : base(null)
        {
            hmi = new CCompilerHMI(this, this);
            network = new CCompilerNetwork(this, this);
            bitArrayList = new CCompilerBitArrayList(this, this);
            programList = new CCompilerProgramList(this, this);
            bitmapList = new CCompilerBitmapList(this, this);
            fontList = new CCompilerFontHMIList(this, this);
            passwordList = new CCompilerPasswordList(this, this);
        }
        public ICustomHMI HMI { get { return hmi; } }
        public String Comment { get; set; }
        public String FileName { get { return fileName; } }
        public ICustomDefaultScreenList Screens { get { return null; } }
        public ICustomScreenList PopupScreens { get { return null; } }
        public ICustomInternalTagList InternalTagList { get { return null; } }
        public ICustomPropertyTagList PropertyTagList { get { return null; } }
        public ICustomNetwork Network { get { return network; } }
        public ICustomAlarmsManager AlarmsManager { get { return null; } }
        public ICustomBitmapList BitmapList { get { return bitmapList; } }
        public ICustomActionList ActionList { get { return null; } }
        public ICustomBitArrayList BitArrayList { get { return bitArrayList; } }
        public ICustomProgramList ProgramList { get{ return programList; } }
        public ICustomFontHMIList FontList { get { return fontList; } }
        public ICustomPasswordList PasswordList { get { return passwordList; } }
        public String HashMD5 { get { return hashMD5; } }
        public void SetFileName(String Value)
        {
            fileName = Value;
        }
        public void LinkObjects() { }
        
        public void SetHashMD5(String Value) 
        {
            hashMD5 = Value;
        }
        public void Initialize() { }

        public CCompilerBitmapList GetBitmapList()
        {
            return bitmapList;
        }
    }
}
