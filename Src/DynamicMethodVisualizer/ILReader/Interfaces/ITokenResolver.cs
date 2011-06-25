using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ReflectionDemos.ILParser.Resolvers
{
    public interface ITokenResolver
    {
        MethodBase AsMethod(int token);
        FieldInfo AsField(int token);
        Type AsType(int token);
        String AsString(int token);
        MemberInfo AsMember(int token);
        byte[] AsSignature(int token);
    }

}
