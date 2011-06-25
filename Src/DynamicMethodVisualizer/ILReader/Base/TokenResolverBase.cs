using System;
using System.Collections.Generic;
using System.Text;
using ReflectionDemos.ILParser.Resolvers;
using System.Reflection;
using System.Diagnostics;

namespace ILReader.Base
{
    /// <summary>
    /// Base class for Token Resolvers.
    /// You still need to implement everything except for the 'AsMember' method
    /// which simply calls the appropriate Type Method or Field getters based onthe member type.
    /// </summary>
    public abstract class TokenResolverBase:ITokenResolver
    {
        public abstract MethodBase AsMethod(int token);

        public abstract FieldInfo AsField(int token);

        public abstract Type AsType(int token);

        public abstract string AsString(int token);
        
        public abstract byte[] AsSignature(int token);
        
        public MemberInfo AsMember(int token)
        {
            if (isTypeMemberToken(token))
                return this.AsType(token);

            if (isMethodMemberToken(token))
                return this.AsMethod(token);

            if (isFieldMemberToken(token))
                return this.AsField(token);

            Debug.Assert(false, string.Format("unexpected token type: {0:x8}", token));
            return null;
        }

        protected static bool isFieldMemberToken(int token)
        {
            return (token & 0x04000000) == 0x04000000;
        }

        protected static bool isMethodMemberToken(int token)
        {
            return (token & 0x06000000) == 0x06000000;
        }

        protected static bool isTypeMemberToken(int token)
        {
            return (token & 0x02000000) == 0x02000000;
        }


     

    }
}
