using STROOP.Controls;
using STROOP.Managers;
using STROOP.Structs.Configurations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (LockConfig.LockingDisabled) return;
            bool shouldSuspend = _lockList.Count >= 2;
            if (shouldSuspend) Config.Stream.Suspend();
            var removeList = new List<WatchVariable>();
            foreach (var varLock in _lockList)
            {
                if (!varLock.InvokeLocks())
                    removeList.Add(varLock);
            }
            foreach (var remove in removeList)
                _lockList.Remove(remove);
            if (shouldSuspend) Config.Stream.Resume();
        }

        public static bool ContainsAnyLocksForObject(uint baseAddress)
        {
            return false;
        }

    };
}
