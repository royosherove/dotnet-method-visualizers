//By Roy Osherove
//www.ISerializable.com
//Use freely!  (Roy@Osherove.com)
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Diagnostics;
using ReflectionDemos.ILParser.Resolvers;
using ILReader.Base;

namespace ClrTest.Reflection
{
   
    public class StaticScopeTokenResolver : TokenResolverBase
    {
        Type m_scope = null;

        public StaticScopeTokenResolver(Type scope)
        {
            m_scope = scope;
        }

       
        public override String AsString(int token)
        {
            return enumReferencedModules(
               delegate(Module mod)
               {
                   return mod.ResolveString(token);
               })
               as String;  
        }

        public override FieldInfo AsField(int token)
        {
            return enumReferencedModules(
               delegate(Module mod)
               {
                   return mod.ResolveField(token);
               })
               as FieldInfo;  
        }

        public override Type AsType(int token)
        {
            return enumReferencedModules(
               delegate(Module mod)
               {
                   return mod.ResolveType(token);
               })
               as Type;
        }


        delegate object ModuleTokenResolveDelegate(Module mod);

        public override MethodBase AsMethod(int token)
        {
            return enumReferencedModules(
                    delegate(Module mod)
                    {
                        return mod.ResolveMethod(token);
                    }
                )   
                    as MethodBase;            
        }


       
        private object enumReferencedModules(ModuleTokenResolveDelegate resolve)
        {
            Module[] mods = getScopeModules();
            foreach (Module mod in mods)
            {
                try
                {
                    object resolved = resolve(mod);
                    if (resolved != null)
                    {
                        return resolved;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception)
                { }

            }
            return null;
        }

        private Module[] mods = null;
        private Module[] getScopeModules()
        {
            if (mods == null)
            {
                mods = m_scope.Assembly.GetModules();
            }
            return mods;
        }



        public override byte[] AsSignature(int token)
        {
            return enumReferencedModules(
                delegate(Module mod)
                {
                    return mod.ResolveSignature(token);
                }
                ) as byte[];
        }
    }
}
