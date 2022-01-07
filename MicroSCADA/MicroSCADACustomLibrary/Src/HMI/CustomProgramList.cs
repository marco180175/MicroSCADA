using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src.HMI
{
    public interface ICustomOperand : ICustomSystem
    {
        object ValueObj { get; set; }
        int ValueInt { get; set; }
        string ValueStr { get; set; }
    }

    public interface ICustomFunction : ICustomSystem
    {
        int OpCode { get; set; }
        ICustomOperand NewOperand();
    }

    public interface ICustomProgramItem: ICustomSystem
    {
        ICustomFunction NewFunction();
    }

    public interface ICustomProgramList:ICustomSystem
    {
        ICustomProgramItem NewProgram();
    }
}
