using System;
using System.Reflection.Emit;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Runtime.Serialization.Formatters.Binary;
using ClrTest.Reflection;
using ReflectionDemos.ILParser;

namespace DynamicMethodVisualizer
{
    public class MethodBodyObjectSource : VisualizerObjectSource
    {
        public override void GetData(object method, Stream outgoingData)
        {
            if (method != null)
            {
                try
                {
                    MethodBodyInfo info = new MethodBodyInfo();
                    info.TypeName = method.GetType().Name;
                    info.MethodToString = method.ToString();

                    IILReader reader = ILReaderFactory.GetReader(method);
                    foreach (ILInstruction instr in reader)
                        info.Instructions.Add(instr.ToString());

                    info.FixupSuccess = true;

                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(outgoingData, info);
                }
                catch (Exception) { }
            }
        }
    }

}
