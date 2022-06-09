using System;
using System.Collections.Generic;
using System.Reflection;

namespace STROOP.Utilities
{
    public class Wrapper<T> where T : struct
    {
        public T value;
    }

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

        public static void ExecuteInitializers<T>(params object[] args) where T : InitializerAttribute
        {
            foreach (var type in typeof(T).Assembly.GetTypes())
                foreach (var m in type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic))
                    if (m.GetParameters().Length == 0 && m.GetCustomAttribute<T>() != null)
                        m.Invoke(null, args);
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

        public static List<TOut> ConvertAndRemoveNull<TOut>(this System.Collections.IEnumerable lstIn, Func<object, TOut> converter) where TOut : class
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

        public static IEnumerable<TOut> ConvertAll<TIn, TOut>(this IEnumerable<TIn> lstIn, Func<TIn, TOut> converter)
        {
            foreach (var obj in lstIn)
                yield return converter(obj);
        }
    }
}
