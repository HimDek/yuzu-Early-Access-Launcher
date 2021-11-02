using System;
using System.Threading;
using System.Windows.Forms;

namespace yuzu_Early_Access_Launcher
{
    static class yuzu_Early_Access_Launcher
    {
        static readonly Mutex mutex = new Mutex(true, "{29a9daa1-8bad-45c8-a05c-0d6724836e59}");
        [STAThread]
        static void Main()
        {
            String version = "2.0.0-Preview-1";
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form yuzuEarlyAccessLauncher = new Form_yuzuEarlyAccessLauncher(version);
                yuzuEarlyAccessLauncher.Text = "yuzu Early Access Launcher Version " + version.Replace('-', ' ');
                Application.Run(yuzuEarlyAccessLauncher);
                mutex.ReleaseMutex();
            }
            else
            {
                // send our Win32 message to make the currently running instanc jump on top of all the other windows
                NativeMethods.PostMessage(
                    (IntPtr)NativeMethods.HWND_BROADCAST,
                    NativeMethods.WM_SHOWME,
                    IntPtr.Zero,
                    IntPtr.Zero);
            }
        }
    }
}
