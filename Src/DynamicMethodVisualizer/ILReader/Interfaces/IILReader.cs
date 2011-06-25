using System;
using System.Collections.Generic;
using System.Text;
using ClrTest.Reflection;

namespace ReflectionDemos.ILParser
{
    public interface IILReader
        :IEnumerable<ILInstruction>
    {
    }
}
