using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TextCopy;
using System.Threading;

namespace GetMousePos
{
    internal static class CursorPosition
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct PointInter
        {
            public int X;
            public int Y;
            public static explicit operator Point(PointInter point) => new Point(point.X, point.Y);
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out PointInter lpPoint);

        // For your convenience
        public static Point GetCursorPosition()
        {
            PointInter lpPoint;
            GetCursorPos(out lpPoint);
            return (Point)lpPoint;
        }
    }

    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("\rx {0} y {1}    ", CursorPosition.GetCursorPosition().X, CursorPosition.GetCursorPosition().Y);
                if(IsControlDown())
                {
                    ClipboardService.SetText(CursorPosition.GetCursorPosition().X + " " + CursorPosition.GetCursorPosition().Y);
                }
                Thread.Sleep(1);
            }
        }
        public static bool IsControlDown()
        {
            return Control.MouseButtons == MouseButtons.Middle;
        }
    }
}