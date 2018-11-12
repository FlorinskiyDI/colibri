using System.ComponentModel;

namespace IdentityServer.Webapi.Helpers
{
    public class TypeConverter
    {
        public static T GetTfromString<T>(string mystring)
        {
            var foo = TypeDescriptor.GetConverter(typeof(T));
            return (T)(foo.ConvertFromInvariantString(mystring));
        }
    }
}
