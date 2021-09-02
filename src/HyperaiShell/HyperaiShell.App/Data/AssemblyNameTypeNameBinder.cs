using System;
using LiteDB;

namespace HyperaiShell.App.Data
{
    public class AssemblyNameTypeNameBinder : ITypeNameBinder
    {
        public string GetName(Type type)
        {
            return type.AssemblyQualifiedName;
        }

        public Type GetType(string name)
        {
            return Type.GetType(name);
        }
    }
}
