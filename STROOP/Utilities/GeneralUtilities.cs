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
        public static IEnumerable<Type> EnumerateTypes(Func<Type, bool> filter) {
            foreach (var t in stroopTypes)
                if (filter(t))
                    yield return t;
        }
        static GeneralUtilities()
        {
            stroopTypes = typeof(GeneralUtilities).Assembly.GetTypes();
        }
    }
}
