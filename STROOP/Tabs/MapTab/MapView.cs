using System.Collections.Generic;
using STROOP.Structs;
using STROOP.Utilities;
using OpenTK;
using STROOP.Structs.Configurations;

namespace STROOP.Tabs.MapTab
{
    public class MapView
    {
        public enum ViewMode
        {
            TopDown,
            Orthogonal,
            ThreeDimensional
        }

        public enum Camera3DMode
        {
            InGame,
            FocusOnPositionAngle,
            Free,
        }

        public MapGraphics MapGraphics;
        public ViewMode mode = ViewMode.TopDown;
        public Camera3DMode camera3DMode = Camera3DMode.FocusOnPositionAngle;
        public PositionAngle focusPositionAngle = PositionAngle.Mario;
        public Vector2 orthoOffset = Vector2.Zero;
        public float orthoRelativeNearPlane = 0, orthoRelativeFarPlane = float.NaN;
        public bool displayOrthoLevelGeometry = true;
        public bool display3DLevelGeometry = true;
        public bool drawCylinderOutlines = false;

        public Vector3 position;
        public float yaw, pitch, camera3DDistanceController = 50;
        public float movementSpeed = 2000.0f;

        public Matrix4 ComputeViewOrientation() => Matrix4.CreateRotationX(pitch) * Matrix4.CreateRotationY(yaw);
        public Vector3 ComputeViewDirection() => Vector3.TransformPosition(new Vector3(0, 0, 1), ComputeViewOrientation());

        public bool TranslateMapCameraPosition(float xOffset, float yOffset, float zOffset, bool useRelative)
        {
            MapUtilities.MaybeChangeMapCameraMode();
            List<PositionAngle> posAngles = new List<PositionAngle> { PositionAngle.MapCamera };
            return ButtonUtilities.ChangeValues(posAngles, xOffset, yOffset, zOffset, ButtonUtilities.Change.ADD, useRelative);
        }

        public bool TranslateMapCameraSpherical(float radiusOffset, float thetaOffset, float phiOffset)
        {
            MapUtilities.MaybeChangeMapCameraMode();
            ButtonUtilities.HandleScaling(ref thetaOffset, ref phiOffset);

            (double newX, double newY, double newZ) =
                MoreMath.OffsetSphericallyAboutPivot(
                    SpecialConfig.Map3DCameraX, SpecialConfig.Map3DCameraY, SpecialConfig.Map3DCameraZ,
                    radiusOffset, thetaOffset, phiOffset,
                    SpecialConfig.Map3DFocusX, SpecialConfig.Map3DFocusY, SpecialConfig.Map3DFocusZ);

            SpecialConfig.Map3DCameraX = (float)newX;
            SpecialConfig.Map3DCameraY = (float)newY;
            SpecialConfig.Map3DCameraZ = (float)newZ;

            return true;
        }

        public bool TranslateMapFocusPosition(float xOffset, float yOffset, float zOffset, bool useRelative)
        {
            MapUtilities.MaybeChangeMapCameraMode();
            List<PositionAngle> posAngles = new List<PositionAngle> { PositionAngle.MapFocus };
            return ButtonUtilities.ChangeValues(posAngles, xOffset, yOffset, zOffset, ButtonUtilities.Change.ADD, useRelative);
        }

        public bool TranslateMapFocusSpherical(float radiusOffset, float thetaOffset, float phiOffset)
        {
            MapUtilities.MaybeChangeMapCameraMode();
            ButtonUtilities.HandleScaling(ref thetaOffset, ref phiOffset);

            if (SpecialConfig.Map3DMode == Map3DCameraMode.CameraPosAndAngle)
            {
                SpecialConfig.Map3DCameraYaw += thetaOffset;
                SpecialConfig.Map3DCameraPitch += phiOffset;
                return true;
            }

            (double newX, double newY, double newZ) =
                MoreMath.OffsetSphericallyAboutPivot(
                    SpecialConfig.Map3DFocusX, SpecialConfig.Map3DFocusY, SpecialConfig.Map3DFocusZ,
                    radiusOffset, thetaOffset, phiOffset,
                    SpecialConfig.Map3DCameraX, SpecialConfig.Map3DCameraY, SpecialConfig.Map3DCameraZ);

            SpecialConfig.Map3DFocusX = (float)newX;
            SpecialConfig.Map3DFocusY = (float)newY;
            SpecialConfig.Map3DFocusZ = (float)newZ;

            return true;
        }

        public bool TranslateMapCameraFocus(float xOffset, float yOffset, float zOffset, bool useRelative)
        {
            MapUtilities.MaybeChangeMapCameraMode();
            List<PositionAngle> posAngles = new List<PositionAngle> { PositionAngle.MapCamera, PositionAngle.MapFocus };
            return ButtonUtilities.ChangeValues(posAngles, xOffset, yOffset, zOffset, ButtonUtilities.Change.ADD, useRelative);
        }
    }
}
