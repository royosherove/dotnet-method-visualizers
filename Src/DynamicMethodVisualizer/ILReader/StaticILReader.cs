//By Roy Osherove
//www.ISerializable.com
//Use freely!  (Roy@Osherove.com)
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


    public class StaticILReader : ILReaderBase
    {
        public StaticILReader(MethodBase mb)
            :base(mb)
        {
        }
        
        protected override byte[] getMethodBodyAsByteArray(object target)
        {
            MethodBase mb = target as MethodBase;
            return mb.GetMethodBody().GetILAsByteArray();
        }

        protected override ITokenResolver getTokenResolver(object target)
        {
            MethodBase mb = target as MethodBase;
            return new StaticScopeTokenResolver(mb.DeclaringType);
        }

    }
}


