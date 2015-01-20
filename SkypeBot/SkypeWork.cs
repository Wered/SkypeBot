using System;
using System.Runtime.InteropServices;

namespace SkypeBot
{
 public   class SkypeWork
 {
     #region Объявлеие Win Api Функций
[DllImport("user32.dll", CharSet = CharSet.Auto)]
static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);
[DllImport("user32.dll", SetLastError = true)]
static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
[DllImport("user32.dll", SetLastError = true)]
static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
[DllImport("user32.dll", CharSet = CharSet.Auto)]
static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

     #endregion
public void Work(string str)
        {
            IntPtr Window = FindWindow("TConversationForm", null);
            IntPtr editor = FindWindowEx(Window, IntPtr.Zero, "TChatEntryControl", null);
            IntPtr edit = FindWindowEx(editor, IntPtr.Zero, "TChatRichEdit", null);
            SendMessage(edit, 0x000C, IntPtr.Zero, str);
            SendMessage(edit, 0x0100, (IntPtr)0x0D, IntPtr.Zero);      
            SendMessage(Window, 0x0010, IntPtr.Zero, IntPtr.Zero);        
        }

    }
}
