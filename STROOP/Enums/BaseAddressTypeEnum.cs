using STROOP.Managers;
using STROOP.Structs.Configurations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STROOP.Structs
{
    public static class BaseAddressType
    {
        static BaseAddressType() => Utilities.StringUtilities.InitializeDeclaredStrings(typeof(BaseAddressType));

        [Utilities.DeclaredString]
        public static string
        None,

        Absolute,
        Relative,

        Mario,
        MarioObj,
        Camera,
        CameraStruct,
        LakituStruct,
        CameraModeInfo,
        CameraModeTransition,
        CameraSettings,
        File,
        MainSave,
        Object,
        Triangle,
        Area;
    };
}
