using System;
using System.Linq;
using LiteDB;

namespace HyperaiShell.App.Data
{
    public class SearchingTypeNameBinder : ITypeNameBinder
    {
        public string GetName(Type type)
        {
            return $"{type.FullName},{type.Assembly.GetName().Name}";
        }

        public Type GetType(string name)
        {
            var typeName = name.Substring(0, name.IndexOf(','));
            var assName = name.Substring(typeName.Length + 1);
            return AppDomain.CurrentDomain.GetAssemblies().First(x => x.GetName().Name == assName).GetType(typeName);
        }
    }
}
