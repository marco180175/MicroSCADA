using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace MicroSCADACompiler.Src
{
    static class CLZW
    {
        private const int TABLE_SIZE = 18042;
        private const int STACK_SIZE = 4000;
        private static int[] m_Code = new int[TABLE_SIZE];
        private static int[] m_Prefix = new int[TABLE_SIZE];
        private static byte[] m_Append = new byte[TABLE_SIZE];
        private static byte[] m_Stack = new byte[STACK_SIZE];
        private static int m_IdxStack;
        private static int m_NumBits;
        private static int m_fError;
        private static int m_BytesIn;
        private static int m_BytesOut;
        private static int m_CheckPoint;
        private static int m_OutBitCont;
        private static int m_BitCont;
        private static int m_OutBitBuffer;
        private static int m_BitBuffer;
        private static int m_MaxCode;
        /*!
         * 
         */
        private static void ClearTables()
        {
            for (int i=0; i < TABLE_SIZE; i++)
            {
                m_Code[i] = -1;
                m_Prefix[i] = 0;
                m_Append[i] = 0;
            }
        }
        /*!
         * 
         */
        private static int FindMatch(int hPrefix, byte hCharacter)
        {             
            int index;
            int offset;

            index = (hCharacter << 6) ^ hPrefix;
            if (index == 0)
            {
                offset = 1;
            }
            else
            {
                offset = TABLE_SIZE - index-1;
            }

            while(true)
            {
                if(m_Code[index] == -1)
                {
                    return index;                   
                }
                if( (m_Prefix[index] == hPrefix) && (m_Append[index] == hCharacter))
                {
                    return index;                 
                }
                index = index - offset;
                if( index < 0)
                    index = index + TABLE_SIZE-1;
            }
        }
        /*!
         * 
         */
        private static void OutputCode(Stream Output,int Code)
        {
            byte Byt ;
            
            m_OutBitBuffer = m_OutBitBuffer | Code << (32 - m_NumBits - m_OutBitCont);
            m_OutBitCont = m_OutBitCont + m_NumBits;
            while( m_OutBitCont >= 8)
            {
                Byt = (byte)(m_OutBitBuffer >> 24);
                Output.WriteByte(Byt);
                m_OutBitBuffer = m_OutBitBuffer << 8;
                m_OutBitCont = m_OutBitCont - 8;
                m_BytesOut++;
            }
        }
        /*!
         * 
         */
        private static int InputCode(Stream Input)
        {
            int value;
            byte Byt ;
            while( m_BitCont <= 24)
            {
                Byt = (byte)Input.ReadByte();
                m_BitBuffer = m_BitBuffer | Byt << (24 - m_BitCont);
                m_BitCont = m_BitCont + 8;
            }
            value = m_BitBuffer >> (32 - m_NumBits);
            m_BitBuffer = m_BitBuffer << m_NumBits;
            m_BitCont = m_BitCont - m_NumBits;
            return value;
        }
        /*!
         * 
         */
        private static int DecodeString(int Index, int Code)
        {
            int I = 0;                    

            while((Code > 255)||(m_fError == 1))
            {
                m_Stack[Index++] = m_Append[Code];                  
                Code = m_Prefix[Code];
                I++;
                if (I >= STACK_SIZE)
                    m_fError = 1;
            }
            m_Stack[Index] = (byte)Code;
            return Index;
        }
        /*!
         * 
         */
        private static int MaxVal(int n)
        {
            return (( 1 << ( n )) - 1);
        }
        /*!
         * 
         */
        public static Stream Compress(Stream Input)
        {
            MemoryStream output = new MemoryStream();
            int NextCode;
            byte Character  ;
            int StringCode ;
            int Index      ;            
            int RatioNew ;
            int RatioOld;
            byte Byt;

            m_NumBits = 9;
            m_MaxCode = MaxVal(m_NumBits);

            m_OutBitCont = 0;
            m_OutBitBuffer = 0;
            NextCode = 258;
            RatioOld = 100;

            //LIMPA TABELAS
            ClearTables();

            Input.Position = 0;
            
            Byt = (byte)Input.ReadByte();
            StringCode = Byt;

            while( Input.Position < Input.Length)
            {
                Byt = (byte)Input.ReadByte();
                Character = Byt;
                m_BytesIn++;

                Index = FindMatch(StringCode,Character);

                if( m_Code[Index] != -1)
                StringCode = m_Code[Index];
                else
                {
                    if( NextCode <= m_MaxCode)
                    {
                        m_Code[Index]  =NextCode;
                        NextCode++;
                        m_Prefix[Index] =StringCode;
                        m_Append[Index] =Character;
                    }
                    OutputCode(output,StringCode);
                    StringCode =Character;

                    if( NextCode > m_MaxCode)
                    {
                        if( m_NumBits < 14)
                        {
                            m_NumBits++;
                            m_MaxCode = MaxVal(m_NumBits);
                        }
                        else
                        {
                            if( m_BytesIn > m_CheckPoint )
                            {
                                if( m_NumBits == 14)
                                {
                                    RatioNew = (m_BytesOut * 100) / m_BytesIn;
                                    if (RatioNew > RatioOld)
                                    {
                                        OutputCode(output,256);
                                        m_NumBits   =9;
                                        NextCode    =258;
                                        m_MaxCode   =MaxVal(m_NumBits);
                                        m_BytesIn   =0;
                                        m_BytesOut  =0;
                                        RatioOld    =100;
                                        for(int I=0;I< TABLE_SIZE;I++)
                                            m_Code[I] =-1;
                                    }
                                    else
                                        RatioOld = RatioNew;
                                }
                                m_CheckPoint = m_BytesIn+100;
                            }
                        }
                    }
                }
            } //FIM WHILE
            OutputCode(output,StringCode);

            if( NextCode == m_MaxCode)
                m_NumBits++;
            OutputCode(output,257);
            OutputCode(output,0);
            OutputCode(output,0);
            OutputCode(output,0);
            return output;
        }
        public static Stream Expand(Stream Input)
        {
            MemoryStream output = new MemoryStream();

            return output;
        }
    }
}
