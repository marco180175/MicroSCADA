using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src.HMI;
namespace MicroSCADACompilerLibrary.Src
{
    class CCompilerOperand : CCompilerSystem, ICustomOperand
    {
        public CCompilerOperand(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        { }
        public object ValueObj { get; set; }
        public int ValueInt { get; set; }
        public string ValueStr { get; set; }
    }

    class CCompilerFunction : CCompilerSystem, ICustomFunction
    {
        public CCompilerFunction(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        { }
        public int OpCode { get; set; }
        public ICustomOperand NewOperand()
        {
            CCompilerOperand operand = new CCompilerOperand(this, project);
            ObjectList.Add(operand);
            return operand;
        }
    }

    class CCompilerProgramItem : CCompilerSystem, ICustomProgramItem
    {
        public CCompilerProgramItem(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        { }

        public ICustomFunction NewFunction()
        {
            CCompilerFunction function = new CCompilerFunction(this, project);
            ObjectList.Add(function);
            return function;
        }
    }

    class CCompilerProgramList : CCompilerSystem, ICustomProgramList
    {
        public CCompilerProgramList(Object AOwner, CCompilerProject Project) :
            base(AOwner, Project)
        { }

        public ICustomProgramItem NewProgram()
        {
            CCompilerProgramItem program = new CCompilerProgramItem(this,project);
            ObjectList.Add(program);
            return program;
        }
    }
}
