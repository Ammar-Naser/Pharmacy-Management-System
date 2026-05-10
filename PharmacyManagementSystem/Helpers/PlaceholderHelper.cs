using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Helpers
{
    public static class PlaceholderHelper
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        public static void Set(TextBox tb, string text)
        {
            if (tb.IsHandleCreated)
            {
                SendMessage(tb.Handle, EM_SETCUEBANNER, (IntPtr)0, text);
            }
            else
            {
                tb.HandleCreated += (s, e) =>
                {
                    SendMessage(tb.Handle, EM_SETCUEBANNER, (IntPtr)0, text);
                };
            }
        }
    }
}