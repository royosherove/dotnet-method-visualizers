//By Roy Osherove
//www.ISerializable.com
//Use freely!  (Roy@Osherove.com)
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using ClrTest.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace ReflectionDemos.ILParser
{
    public static class ILReaderFactory
    {
        static Type s_executionScope;
        static FieldInfo s_lamdaField, s_lamdaMethodField;

        static ILReaderFactory()
        {
            s_executionScope = typeof(ExecutionScope);
            s_lamdaField = s_executionScope.GetField("Lambda", BindingFlags.Instance | BindingFlags.NonPublic);
            s_lamdaMethodField = s_lamdaField.FieldType.GetField("Method", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public static IILReader GetReader(object target)
        {
            MethodBase method = target as MethodBase;
            if (method == null)
            {
                if (target is MulticastDelegate)
                {
                    // if this is an ExecutionScope then we probably have a lamda expression.
                    ExecutionScope scope = (((Delegate)target).Target as ExecutionScope);
                    if (scope == null)
                        return GetReader(method);

                    // if we get this far we DO have a lamda expression.
                    object lamdaValue = s_lamdaField.GetValue(scope);
                    if (lamdaValue == null)
                        return GetReader(method);

                    method = (s_lamdaMethodField.GetValue(lamdaValue) as MethodBase);
                }
            }
            return GetReader(method);
        }

        public static IILReader GetReader(MethodBase method) 
        {
            if (method ==null)
            {
                return null;
            }

            if (method is DynamicMethod)
            {
                return new DynamicILReader(method as DynamicMethod);
            }
            else
            {
                return new StaticILReader(method);
            }
        }
    }
}
