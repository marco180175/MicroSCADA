using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroSCADARuntimeLibrary.Src.Visuals
{
    class CQueueScroll
    {
        private float[] buffer;
        private int last;
        public CQueueScroll(int Size)
        {
            this.buffer = new float[Size];
            this.last = Size - 1;
        }
        public int Size
        {
            get { return this.buffer.Length; }
        }        
        public void Add(float Value)
        {
            if (last < buffer.Length - 1)
                last++;
            else
                last = 0;
            buffer[last] = Value;
        }
        public float Item(int Index)
        {
            if (Index < buffer.Length)
            {
                if (Index > last)
                    Index = last - Index + buffer.Length;
                else
                    Index = last - Index;
                return buffer[Index];
            }
            else
                return 0;
        }
    }
}
