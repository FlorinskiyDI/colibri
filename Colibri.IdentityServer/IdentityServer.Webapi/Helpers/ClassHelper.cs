using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Helpers
{
    public  class ClassHelper
    {
        public static List<string> GetConstantValues<T>() where T: class
        {
            var values = new List<string>();
            Type t = typeof(T);
            var fields = t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (FieldInfo fi in fields)
            {
                values.Add(fi.GetValue(null).ToString());
            }
            return values;
        }

    }
}
