using System;
using System.Collections.Generic;
using System.Text;
using ClrTest.Reflection;

namespace ILReader.Base
{
    public interface IILFormatProvider
    {
        string Int32ToHex(int int32);
        string Int16ToHex(int int16);
        string Int8ToHex(int int8);
        string Argument(int ordinal);
        string EscapedString(string str);
        string Label(int offset);
        string MultipleLabels(int[] offsets);
        string SigByteArrayToString(byte[] sig);
        string Format(ILInstruction ilInstruction);
    }
}
