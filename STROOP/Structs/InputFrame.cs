using STROOP.Structs.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STROOP.Structs
{
    public class InputFrame
    {
        public sbyte ControlStickH { get; set; }
        public sbyte ControlStickV { get; set; }

        ushort buttonMask;
        public bool IsButtonPressed(InputConfig.ButtonMask button) => (buttonMask & (ushort)button) != 0;

        public static InputFrame GetCurrent()
        {
            uint inputAddress = InputConfig.CurrentInputAddress;

            return new InputFrame()
            {
                buttonMask = Config.Stream.GetUInt16(inputAddress),
                ControlStickH = (sbyte)Config.Stream.GetByte(inputAddress + InputConfig.ControlStickXOffset),
                ControlStickV = (sbyte)Config.Stream.GetByte(inputAddress + InputConfig.ControlStickYOffset)
            };
        }

        public override bool Equals(object obj) =>
            (obj is InputFrame other && ControlStickH == other.ControlStickH && ControlStickV == other.ControlStickV && buttonMask == other.buttonMask);

        public override int GetHashCode() => base.GetHashCode();
    }
}
