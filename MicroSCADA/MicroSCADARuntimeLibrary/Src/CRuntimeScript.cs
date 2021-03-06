using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroSCADACustomLibrary.Src;

namespace MicroSCADARuntimeLibrary.Src
{
    public class CRuntimeScript : CRuntimeSystem, ICustomScript
    {
        protected String[] text;
        /*!
         * Construtor
         */
        public CRuntimeScript(Object AOwner, CRuntimeProject Project)
            : base(AOwner, Project)
        {
            this.text = new string[1];
            this.text[0] = String.Empty;
        }
        public String Script
        {
            get { return this.TextToString(); }
            set { this.StringToText(value); }
        }

        protected String TextToString()
        {
            String str = String.Empty;
            for (int i = 0; i < text.Length; i++)
            {
                if (i < text.Length - 1)
                    str += text[i] + '\n';
                else
                    str += text[i];
            }
            return str;
        }

        protected void StringToText(String Value)
        {
            int index, i;

            index = Value.IndexOf('\n');
            i = 0;
            while (index != -1)
            {
                if (i > 0)
                    Array.Resize(ref text, text.Length + 1);
                text[i++] = Value.Substring(0, index);
                Value = Value.Remove(0, index + 1);
                index = Value.IndexOf('\n');
            }
            if (i > 0)
                Array.Resize(ref text, text.Length + 1);
            text[i] = Value;
        }
    }

}
