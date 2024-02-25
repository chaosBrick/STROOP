using STROOP.Core.WatchVariables;
using STROOP.Structs.Configurations;
using System.Collections.Generic;

namespace STROOP.Structs
{
    public static class WatchVariableLockManager
    {
        private static HashSet<WatchVariable> _lockList = new HashSet<WatchVariable>();

        public static void AddLocks(WatchVariable variable)
        {
            _lockList.Add(variable);
        }

        public static void RemoveAllLocks()
        {
            foreach (var l in _lockList)
                l.ClearLocks();
            _lockList.Clear();
        }

        public static void Update()
        {
            if (LockConfig.LockingDisabled || _lockList.Count == 0) return;
            using (Config.Stream.Suspend())
            {
                var removeList = new List<WatchVariable>();
                foreach (var varLock in _lockList)
                {
                    if (!varLock.InvokeLocks())
                        removeList.Add(varLock);
                }
                foreach (var remove in removeList)
                    _lockList.Remove(remove);
            }
        }

        public static bool ContainsAnyLocksForObject(uint baseAddress)
        {
            return false;
        }

    };
}
