using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using ReflectionDemos.ILParser.Resolvers;
using ReflectionDemos.ILParser;
using ILReader;

namespace ClrTest.Reflection
{
    // 
    // part of the following code is based on internal DynamicMethod implementation. 
    // We do not recommend use such approach in any production code; and 
    // it will most likley be broken in the future CLR release.
    // 

    public class DynamicILReader : ILReaderBase
    {
        static FieldInfo s_fiLen = typeof(ILGenerator).GetField("m_length", BindingFlags.NonPublic | BindingFlags.Instance);
        static FieldInfo s_fiStream = typeof(ILGenerator).GetField("m_ILStream", BindingFlags.NonPublic | BindingFlags.Instance);
        static MethodInfo s_miBakeByteArray = typeof(ILGenerator).GetMethod("BakeByteArray", BindingFlags.NonPublic | BindingFlags.Instance);
        bool m_fixupSuccess;


        public DynamicILReader(DynamicMethod dynamicMethod)
            :base(dynamicMethod)
        {
        }

        protected override byte[] getMethodBodyAsByteArray(object target)
        {
            DynamicMethod dm = target as DynamicMethod;
            ILGenerator ilgen = dm.GetILGenerator();

            this.m_fixupSuccess = false;
            Byte[] bytes = null;
            try
            {
                bytes = (byte[])s_miBakeByteArray.Invoke(ilgen, null);
                if (bytes == null) bytes = new byte[0];
                m_fixupSuccess = true;
            }
            catch (TargetInvocationException)
            {
                int length = (int)s_fiLen.GetValue(ilgen);
                bytes = new byte[length];
                Array.Copy((byte[])s_fiStream.GetValue(ilgen), bytes, length);
            }
            return bytes;
        }

        protected override ITokenResolver getTokenResolver(object target)
        {
            return new DynamicScopeTokenResolver(target as DynamicMethod);
        }



        public bool FixupSuccess 
        {
            get { return this.m_fixupSuccess; }
        }

        

        
    }
}


