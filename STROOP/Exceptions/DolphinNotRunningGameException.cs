using System;

namespace STROOP.Exceptions
{
    public class DolphinNotRunningGameException : Exception
    {
        public DolphinNotRunningGameException()
            : base("Dolphin running, but emulator hasn't started")
        {
        }
    }
}
