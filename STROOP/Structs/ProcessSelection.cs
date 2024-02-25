using System.Diagnostics;

namespace STROOP.Utilities
{
    public struct ProcessSelection
    {
        public Process Process;
        public int Index;

        public ProcessSelection(Process process, int index)
        {
            Process = process;
            Index = index;
        }

        public override string ToString()
        {
            return Index + ". " + this.Process.ProcessName;
        }
    }
}
