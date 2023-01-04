using System;
using System.Linq;

namespace Wayway.Engine
{
    public static class EnumExtension
    {
        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var arr = (T[])Enum.GetValues(src.GetType());
            var j = Array.IndexOf(arr, src) + 1;
            return (arr.Length==j) ? arr[0] : arr[j];
        }
        
        public static Enum GetRandomEnumValue(this Type t)
        {
            return Enum.GetValues(t)            
                .OfType<Enum>()                 
                .OrderBy(_ => Guid.NewGuid())   
                .FirstOrDefault();              
        }
    }
}
