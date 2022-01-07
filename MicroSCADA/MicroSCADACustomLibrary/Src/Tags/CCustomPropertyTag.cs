using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADACustomLibrary.Src
{
    public interface ICustomPropertyTag : ICustomTag
    {
        ICustomSystem Reference { get; }
        string PropertyName { get; set; }
        void SetGuidReference(Guid Value);
    }

    public interface ICustomPropertyTagList : ICustomSystem
    {
        ICustomPropertyTag AddTag();
        void LinkObjects();
    }
}
