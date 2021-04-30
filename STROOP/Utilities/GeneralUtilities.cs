using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STROOP.Utilities
{
    public static class GeneralUtilities
    {
        static Type[] stroopTypes;
        public static IEnumerable<Type> EnumerateTypes(Func<Type, bool> filter)
        {
            foreach (var t in stroopTypes)
                if (filter(t))
                    yield return t;
        }
        static GeneralUtilities()
        {
            stroopTypes = typeof(GeneralUtilities).Assembly.GetTypes();
        }

        public static List<TOut> ConvertAndRemoveNull<TIn, TOut>(this IEnumerable<TIn> lstIn, Func<TIn, TOut> converter) where TOut : class
        {
            var lstOut = new List<TOut>();
            foreach (var obj in lstIn)
            {
                var convertedObj = converter(obj);
                if (convertedObj != null)
                    lstOut.Add(convertedObj);
            }
            return lstOut;
        }
    }
}
