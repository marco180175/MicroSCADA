using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MicroSCADACustomLibrary.Src.HMI;

namespace MicroSCADAStudioLibrary.Src.HMI
{
    class CDesignOperand : CDesignSystem, ICustomOperand
    {
        public CDesignOperand(Object AOwner, CDesignProject Project) :
            base(AOwner, Project)
        { }
        public object ValueObj { get; set; }
        public int ValueInt { get; set; }
        public string ValueStr { get; set; }
    }

    class CDesignFunction : CDesignSystem, ICustomFunction
    {
        public CDesignFunction(Object AOwner, CDesignProject Project) :
            base(AOwner, Project)
        { }
        public int OpCode { get; set; }
        public ICustomOperand NewOperand()
        {
            CDesignOperand operand = new CDesignOperand(this, project);
            ObjectList.Add(operand);
            return operand;
        }
    }

    class CDesignProgramItem : CDesignSystem, ICustomProgramItem
    {
        public CDesignProgramItem(Object AOwner, CDesignProject Project) :
            base(AOwner, Project)
        { }

        public ICustomFunction NewFunction()
        {
            CDesignFunction function = new CDesignFunction(this, project);
            ObjectList.Add(function);
            OnAddItem(new AddItemEventArgs(function, function.ImageIndex));
            return function;
        }
    }

    class CDesignProgramList : CDesignSystem, ICustomProgramList
    {
        public CDesignProgramList(Object AOwner, CDesignProject Project) :
            base(AOwner, Project)
        {
            this.SetGUID(Guid.NewGuid());
            this.SetOpenName("ProgramList");
            this.imageIndex = -1;
        }
        //!
        [Browsable(true), ReadOnly(true)]
        new public String Name
        {
            get { return this.customObject.name; }
            set { this.SetName(value); }
        }
        public ICustomProgramItem NewProgram()
        {
            CDesignProgramItem program = new CDesignProgramItem(this, project);
            ObjectList.Add(program);
            OnAddItem(new AddItemEventArgs(program, program.ImageIndex));
            return program;
        }
    }
}
