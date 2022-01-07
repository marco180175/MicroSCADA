using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MicroSCADACustomLibrary.Src
{
    public sealed class CCustomTextProperties : Object, IDisposable
    {
        public String[] text;
        public StringAlignment alignment;
        public Font font;
        public Color fontColor;
        public Color backColor;
        /*!
         * Construtor         
         */
        public CCustomTextProperties()
            : base()
        {            
            this.font = new Font("Arial", 16, FontStyle.Bold);
            this.fontColor = Color.Black;            
            this.alignment = StringAlignment.Near;
            this.text = new String[0];
            Array.Resize(ref this.text, 1);
            this.text[0]="Text";
            this.backColor = Color.White;        
        }
        public void Dispose()
        {

        }
        public String TextToString()
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

        public void StringToText(String Value)
        {
            int index, i;
            
            index = Value.IndexOf('\n');
            i = 0;
            while(index != -1)
            {   
                if(i > 0)
                    Array.Resize(ref text, text.Length + 1);                    
                text[i++] = Value.Substring(0, index);
                Value = Value.Remove(0, index+1);
                index = Value.IndexOf('\n');                
            }
            if (i > 0)
                Array.Resize(ref text, text.Length + 1);
            text[i] = Value;           
        }
    }
}
