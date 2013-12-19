using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetworkController.Plugin.Mouse
{
    public class MouseInterop
    {
        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(UInt16 virtualKeyCode);

        //http://msdn.microsoft.com/en-us/library/dd375731(v=vs.85).aspx
        private enum VirtualKey : ushort
        {
            MouseLeft = 0x01,
            MouseRight = 0x02,
            MouseMiddle = 0x04,
        }

        private static bool IsButtonPressed(VirtualKey button)
        {
            return GetAsyncKeyState((ushort)button) != 0;
        }


        
        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            return lpPoint;
        }

        public static bool IsLeftMousePressed()
        { return IsButtonPressed(VirtualKey.MouseLeft); }
        public static bool IsRightMousePressed()
        { return IsButtonPressed(VirtualKey.MouseRight); }
        public static bool IsMiddleMousePressed()
        { return IsButtonPressed(VirtualKey.MouseMiddle); }
    }
}
