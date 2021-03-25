using System;
using System.Collections.Generic;
using System.Reflection;

namespace STROOP.Structs
{
    public class InitializeSpecialAttribute : Attribute
    {
        static HashSet<Type> initializedTypes = new HashSet<Type>();
        public static void ExecuteInitializers()
        {
            foreach (var type in typeof(InitializeSpecialAttribute).Assembly.GetTypes())
                if (!initializedTypes.Contains(type))
                {
                    initializedTypes.Add(type);
                    foreach (var m in type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic))
                        if (m.GetParameters().Length == 0 && m.GetCustomAttribute<InitializeSpecialAttribute>() != null)
                            m.Invoke(null, new object[0]);
                }
        }
    }
}
