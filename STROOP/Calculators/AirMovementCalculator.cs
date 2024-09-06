using System;
using System.Collections.Generic;
using STROOP.Enums;
using STROOP.Models;
using STROOP.Utilities;

namespace STROOP.Calculators
{
    public class AirMovementCalculator
    {

        public MarioState ApplyInput(MarioState marioState, RelativeDirection direction, int numQSteps = 4)
        {
            var withHSpeed = ComputeAirHSpeed(marioState, direction);
            var moved = AirMove(withHSpeed, numQSteps);
            var withYSpeed = ComputeAirYSpeed(moved);
            return withYSpeed;
        }

        public MarioState ApplyInputRepeatedly(MarioState marioState, RelativeDirection direction, int numQSteps)
        {
            var numFrames = numQSteps / 4;
            var remainderQSteps = numQSteps % 4;
            for (var i = 0; i < numFrames; i++)
            {
                marioState = ApplyInput(marioState, direction);
            }
            return remainderQSteps == 0 ? marioState : ApplyInput(marioState, direction, remainderQSteps);
        }

        public MarioState AirMove(MarioState initialState, int numQSteps = 4, List<TriangleDataModel> wallTris = null, List<MarioState> quarterSteps = null, bool resetHSpeedOnWalls = false)
        {
            var resetHSpeed = false;

            var newX = initialState.X;
            var newY = initialState.Y;
            var newZ = initialState.Z;

            if (wallTris != null)
            {
                bool collidedWithWall;
                (newX, newZ, collidedWithWall) = WallDisplacementCalculator.HandleWallDisplacement2(newX, newY, newZ, wallTris, 50, 60);
                if (collidedWithWall && resetHSpeedOnWalls) resetHSpeed = true;
            }

            for (var i = 0; i < numQSteps; i++)
            {
                newX += initialState.XSpeed / 4;
                newY += initialState.YSpeed / 4;
                newZ += initialState.ZSpeed / 4;

                if (wallTris != null)
                {
                    bool collidedWithWall1;
                    bool collidedWithWall2;
                    (newX, newZ, collidedWithWall1) = WallDisplacementCalculator.HandleWallDisplacement2(newX, newY, newZ, wallTris, 50, 150);
                    (newX, newZ, collidedWithWall2) = WallDisplacementCalculator.HandleWallDisplacement2(newX, newY, newZ, wallTris, 50, 30);
                    if (collidedWithWall1 && resetHSpeedOnWalls) resetHSpeed = true;
                    if (collidedWithWall2 && resetHSpeedOnWalls) resetHSpeed = true;
                }

                quarterSteps?.Add(
                    new MarioState(
                        newX,
                        newY,
                        newZ,
                        initialState.XSpeed,
                        initialState.YSpeed,
                        initialState.ZSpeed,
                        initialState.HSpeed,
                        initialState.SlidingSpeedX,
                        initialState.SlidingSpeedZ,
                        initialState.SlidingAngle,
                        initialState.MarioAngle,
                        initialState.CameraAngle,
                        initialState.PreviousState,
                        initialState.LastInput,
                        initialState.Index));
            }

            return new MarioState(
                newX,
                newY,
                newZ,
                initialState.XSpeed,
                initialState.YSpeed,
                initialState.ZSpeed,
                resetHSpeed ? 0 : initialState.HSpeed,
                initialState.SlidingSpeedX,
                initialState.SlidingSpeedZ,
                initialState.SlidingAngle,
                initialState.MarioAngle,
                initialState.CameraAngle,
                initialState.PreviousState,
                initialState.LastInput,
                initialState.Index);
        }

        // update_air_without_turn
        private MarioState ComputeAirHSpeed(MarioState initialState, int angleDiff)
        {
            var longJump = false;
            var maxSpeed = longJump ? 48 : 32;

            var marioAngle = initialState.MarioAngle;
            var inputScaledMagnitude = 32;

            float perpSpeed = 0;
            var newHSpeed = ApproachHSpeed(initialState.HSpeed, 0, 0.35f, 0.35f);
            if (inputScaledMagnitude > 0)
            {
                newHSpeed += (inputScaledMagnitude / 32) * 1.5f * InGameTrigUtilities.InGameCosine(angleDiff);
                perpSpeed = InGameTrigUtilities.InGameSine(angleDiff) * (inputScaledMagnitude / 32) * 10;
            }

            if (newHSpeed > maxSpeed) newHSpeed -= 1;
            if (newHSpeed < -16) newHSpeed += 2;

            var newSlidingXSpeed = InGameTrigUtilities.InGameSine(marioAngle) * newHSpeed;
            var newSlidingZSpeed = InGameTrigUtilities.InGameCosine(marioAngle) * newHSpeed;
            newSlidingXSpeed += perpSpeed * InGameTrigUtilities.InGameSine(marioAngle + 0x4000);
            newSlidingZSpeed += perpSpeed * InGameTrigUtilities.InGameCosine(marioAngle + 0x4000);
            var newXSpeed = newSlidingXSpeed;
            var newZSpeed = newSlidingZSpeed;

            return new MarioState(
                initialState.X,
                initialState.Y,
                initialState.Z,
                newXSpeed,
                initialState.YSpeed,
                newZSpeed,
                newHSpeed,
                initialState.SlidingSpeedX,
                initialState.SlidingSpeedZ,
                initialState.SlidingAngle,
                initialState.MarioAngle,
                initialState.CameraAngle,
                initialState,
                new Input(angleDiff, 0),
                initialState.Index + 1);
        }

        // update_air_without_turn
        private MarioState ComputeAirHSpeed(MarioState initialState, Input input)
        {
            var longJump = false;
            var maxSpeed = longJump ? 48 : 32;

            var marioAngle = initialState.MarioAngle;
            var yawIntended = MoreMath.CalculateAngleFromInputs(input.X, input.Y, initialState.CameraAngle);
            var deltaAngleIntendedFacing = yawIntended - marioAngle;
            var inputScaledMagnitude = input.GetScaledMagnitude();

            float perpSpeed = 0;
            var newHSpeed = ApproachHSpeed(initialState.HSpeed, 0, 0.35f, 0.35f);
            if (inputScaledMagnitude > 0)
            {
                newHSpeed += (inputScaledMagnitude / 32) * 1.5f * InGameTrigUtilities.InGameCosine(deltaAngleIntendedFacing);
                perpSpeed = InGameTrigUtilities.InGameSine(deltaAngleIntendedFacing) * (inputScaledMagnitude / 32) * 10;
            }

            if (newHSpeed > maxSpeed) newHSpeed -= 1;
            if (newHSpeed < -16) newHSpeed += 2;

            var newSlidingXSpeed = InGameTrigUtilities.InGameSine(marioAngle) * newHSpeed;
            var newSlidingZSpeed = InGameTrigUtilities.InGameCosine(marioAngle) * newHSpeed;
            newSlidingXSpeed += perpSpeed * InGameTrigUtilities.InGameSine(marioAngle + 0x4000);
            newSlidingZSpeed += perpSpeed * InGameTrigUtilities.InGameCosine(marioAngle + 0x4000);
            var newXSpeed = newSlidingXSpeed;
            var newZSpeed = newSlidingZSpeed;

            return new MarioState(
                initialState.X,
                initialState.Y,
                initialState.Z,
                newXSpeed,
                initialState.YSpeed,
                newZSpeed,
                newHSpeed,
                initialState.SlidingSpeedX,
                initialState.SlidingSpeedZ,
                initialState.SlidingAngle,
                initialState.MarioAngle,
                initialState.CameraAngle,
                initialState,
                input,
                initialState.Index + 1);
        }

        // update_air_without_turn
        private MarioState ComputeAirHSpeed(MarioState initialState, RelativeDirection direction)
        {
            var longJump = false;
            var maxSpeed = longJump ? 48 : 32;

            var marioAngle = initialState.MarioAngle;
            int deltaAngleIntendedFacing;
            switch (direction)
            {
                case RelativeDirection.Forward:
                    deltaAngleIntendedFacing = 0;
                    break;
                case RelativeDirection.Backward:
                    deltaAngleIntendedFacing = 32768;
                    break;
                case RelativeDirection.Left:
                    deltaAngleIntendedFacing = 16384;
                    break;
                case RelativeDirection.Right:
                    deltaAngleIntendedFacing = 49152;
                    break;
                case RelativeDirection.Center:
                    deltaAngleIntendedFacing = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            float inputScaledMagnitude = direction == RelativeDirection.Center ? 0 : 32;

            float perpSpeed = 0;
            var newHSpeed = ApproachHSpeed(initialState.HSpeed, 0, 0.35f, 0.35f);
            if (inputScaledMagnitude > 0)
            {
                newHSpeed += (inputScaledMagnitude / 32) * 1.5f * InGameTrigUtilities.InGameCosine(deltaAngleIntendedFacing);
                perpSpeed = InGameTrigUtilities.InGameSine(deltaAngleIntendedFacing) * (inputScaledMagnitude / 32) * 10;
            }

            if (newHSpeed > maxSpeed) newHSpeed -= 1;
            if (newHSpeed < -16) newHSpeed += 2;

            var newSlidingXSpeed = InGameTrigUtilities.InGameSine(marioAngle) * newHSpeed;
            var newSlidingZSpeed = InGameTrigUtilities.InGameCosine(marioAngle) * newHSpeed;
            newSlidingXSpeed += perpSpeed * InGameTrigUtilities.InGameSine(marioAngle + 0x4000);
            newSlidingZSpeed += perpSpeed * InGameTrigUtilities.InGameCosine(marioAngle + 0x4000);
            var newXSpeed = newSlidingXSpeed;
            var newZSpeed = newSlidingZSpeed;

            return new MarioState(
                initialState.X,
                initialState.Y,
                initialState.Z,
                newXSpeed,
                initialState.YSpeed,
                newZSpeed,
                newHSpeed,
                initialState.SlidingSpeedX,
                initialState.SlidingSpeedZ,
                initialState.SlidingAngle,
                initialState.MarioAngle,
                initialState.CameraAngle,
                initialState,
                null,
                initialState.Index + 1);
        }

        private float ComputeAirHSpeed(float initialHSpeed)
        {
            const int maxSpeed = 32;
            var newHSpeed = ApproachHSpeed(initialHSpeed, 0, 0.35f, 0.35f);
            if (newHSpeed > maxSpeed) newHSpeed -= 1;
            if (newHSpeed < -16) newHSpeed += 2;
            return newHSpeed;
        }

        public float ComputePosition(float position, float hSpeed, int frames)
        {
            for (var i = 0; i < frames; i++)
            {
                hSpeed = ComputeAirHSpeed(hSpeed);
                position += hSpeed;
            }
            return position;
        }

        private MarioState ComputeAirYSpeed(MarioState initialState)
        {
            var newYSpeed = Math.Max(initialState.YSpeed - 4, -75);
            return new MarioState(
                initialState.X,
                initialState.Y,
                initialState.Z,
                initialState.XSpeed,
                newYSpeed,
                initialState.ZSpeed,
                initialState.HSpeed,
                initialState.SlidingSpeedX,
                initialState.SlidingSpeedZ,
                initialState.SlidingAngle,
                initialState.MarioAngle,
                initialState.CameraAngle,
                initialState.PreviousState,
                initialState.LastInput,
                initialState.Index);
        }

        private float ApproachHSpeed(float speed, float maxSpeed, float increase, float decrease)
        {
            return speed < maxSpeed ? Math.Min(maxSpeed, speed + increase) : Math.Max(maxSpeed, speed - decrease);
        }
    }
}
