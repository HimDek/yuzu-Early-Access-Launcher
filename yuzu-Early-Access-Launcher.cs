using System;
using System.IO;
using System.Drawing;
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
            String theme = "", version = "2.0.0-Preview-3", UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), path = Path.GetDirectoryName(Application.ExecutablePath);
            Directory.SetCurrentDirectory(path);

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Color Fore, Back, ConBack, ConBackBox;

                if (File.Exists("installed.ini"))
                {
                    File.Move("installed.ini", "launcher.ini");
                }
                if (File.Exists("launcher.ini"))
                {
                    Ini ini = new Ini("launcher.ini");
                    theme = ini.Read("theme", "settings");
                }

                if(theme == "Same as yuzu" || theme == "")
                {
                    if (File.Exists(UserProfile + "\\AppData\\Roaming\\yuzu\\config\\qt-config.ini"))
                    {
                        Ini yini = new Ini(UserProfile + "\\AppData\\Roaming\\yuzu\\config\\qt-config.ini");
                        theme = yini.Read("theme", "UI");
                    }
                    else
                    {
                        theme = "Light";
                    }
                }

                Fore = Color.Black;
                Back = Color.FromArgb(249, 249, 249);
                ConBack = Color.FromArgb(255, 255, 255);
                ConBackBox = ConBack;
                if (theme == "qdarkstyle" || theme == "colorful_dark" || theme == "Dark")
                {
                    Fore = Color.White;
                    Back = Color.FromArgb(49, 54, 59);
                    ConBack = Color.FromArgb(35, 38, 41);
                    ConBackBox = ConBack;
                    theme = "Dark";
                }
                if (theme == "qdarkstyle_midnight_blue" || theme == "colorful_midnight_blue" || theme == "Midnight Blue")
                {
                    Fore = Color.White;
                    Back = Color.FromArgb(25, 35, 45);
                    ConBack = Color.FromArgb(80, 95, 105);
                    ConBackBox = Color.FromArgb(15, 25, 34);
                    theme = "Midnight Blue";
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form yuzuEarlyAccessLauncher = new Form_yuzuEarlyAccessLauncher(version, theme, Back, Fore, ConBack, ConBackBox);
                yuzuEarlyAccessLauncher.Text = "yuzu Early Access Launcher Version " + version.Replace('-', ' ');
                yuzuEarlyAccessLauncher.BackColor = Back;
                yuzuEarlyAccessLauncher.ForeColor = Fore;
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
