using System;
using Microsoft.Kinect;

namespace NetworkController.Plugin.Kinect
{
    public class Constants
    {
        public const string ProviderName = "Kinect";
        public const string BodyName = "Body";
        public class Gestures
        {
            public const string PunchForward = "PunchForward";
            public const string PullBack = "PullBack";
            public const string Gesturing = "Gesturing";
            public const string SwipeLeft = "SwipeLeft";
            public const string SwipeRight = "SwipeRight";
            public const string SwipeUp = "SwipeUp";
            public const string SwipeDown = "SwipeDown";
            public const string DodgeForward = "DodgeForward";
            public const string DodgeBack = "DodgeBack";
            public const string DodgeLeft = "DodgeLeft";
            public const string DodgeRight = "DodgeRight";
            public const string Jump = "Jump";
            public const string Duck = "Duck";
        }
        
    }
    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;
    }
    public static class Extensions
    {
        public const float Rad2Deg = 360.0f/((float)Math.PI*2.0f);
        public static Vector3 ToEuler(this Vector4 q1)
        {
            float sqw = q1.W * q1.W;
            float sqx = q1.X * q1.X;
            float sqy = q1.Y * q1.Y;
            float sqz = q1.Z * q1.Z;
            float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            float test = q1.X * q1.W - q1.Y * q1.Z;
            Vector3 v = new Vector3();

            if (test > 0.4995f * unit)
            { // singularity at north pole
                v.Y = 2f * (float)Math.Atan2(q1.Y, q1.X) * Rad2Deg;
                v.X = (float)Math.PI / 2 * Rad2Deg;
                v.Z = 0;
                return NormalizeAngles(v);
            }
            if (test < -0.4995f * unit)
            { // singularity at south pole
                v.Y = -2f * (float)Math.Atan2(q1.Y, q1.X) * Rad2Deg;
                v.X = -(float)Math.PI / 2 * Rad2Deg;
                v.Z = 0;
                return NormalizeAngles(v);
            }
            var q = new Vector4
            {
                W = q1.W, 
                Z = q1.Z, 
                X = q1.X, 
                Y = q1.Y
            };

            v.Y = (float)Math.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1 - 2f * (q.Z * q.Z + q.W * q.W)) * Rad2Deg;     // Yaw
            v.X = (float)Math.Asin(2f * (q.X * q.Z - q.W * q.Y)) * Rad2Deg;                             // Pitch
            v.Z = (float)Math.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1 - 2f * (q.Y * q.Y + q.Z * q.Z)) * Rad2Deg;      // Roll
            return NormalizeAngles(v);
        }

        static Vector3 NormalizeAngles(Vector3 angles)
        {
            angles.X = NormalizeAngle(angles.X);
            angles.Y = NormalizeAngle(angles.Y);
            angles.Z = NormalizeAngle(angles.Z);
            return angles;
        }

        static float NormalizeAngle(float angle)
        {
            while (angle > 360)
                angle -= 360;
            while (angle < 0)
                angle += 360;
            return angle;
        }
    }
}
