using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public enum CCustomActionCode : uint
    {
        NextScreen = 1,
        PrevScreen = 2,
        ShowScreen = 3,    
        ShowPopup = 4               
    }

    public interface ICustomCodeLine
    {
        CCustomActionCode Opcode { get; }
        ArrayList ParamList { get; }
        void SetOpcode(CCustomActionCode Value);
        void SetGuidParam(Guid Value);
    }

    public class CCustomCodeLine
    {
        public CCustomActionCode opcode;//!< Codigo
        public List<int> indexOperand;//!< Lista de index para referencias
        public CCustomCodeLine()
        {
            opcode = 0;
            indexOperand = new List<int>();
        }
    }



    public interface ICustomAction : ICustomSystem
    {
        ICustomCodeLine NewCodeLine();
    }

    public interface ICustomActionList : ICustomSystem
    {
        ICustomAction NewAction();
    }
}
