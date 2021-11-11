using IWshRuntimeLibrary;
using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yuzu_Early_Access_Launcher
{
    public partial class Form_yuzuEarlyAccessLauncher : Form
    {
        static readonly String path = Path.GetDirectoryName(Application.ExecutablePath), UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        String version = "", VersionName = "", branch = "", theme= "", file = "", firfile = "", launcherlatest = "", lurl = "", lsize = "", latest = "", url = "", ysize = "", kurl = "", lfirm = "", furl = "", fsize = "", installed = "", firm = "", reg = "", xsdir = "";
        bool Internet, Downloading = false, DeleteyuzuArchive = false, DeleteFirmwareArchive = false, ExitLauncher = true;
        long xdsize = 0;
        Color Back, Fore;
        StreamWriter log;
        Ini ini = new Ini("launcher.ini");

        public Form_yuzuEarlyAccessLauncher(String ver, String Theme, Color BackColor, Color ForeColor, Color ConBack, Color ConBackBox)
        {
            InitializeComponent();

            version = ver;
            try
            {
                branch = version.Split('-')[1];
            }
            catch (Exception)
            {
                branch = "main";
            }

            if (branch != "Preview")
            {
                if (Application.ExecutablePath != UserProfile + "\\AppData\\Local\\yuzu\\yuzu Early Access Launcher.exe")
                {
                    if (path == UserProfile + "\\AppData\\Local\\Temp" || path.Split('\\').Last() == "Release" || !System.IO.File.Exists(UserProfile + "\\AppData\\Local\\yuzu\\yuzu Early Access Launcher.exe"))
                    {
                        if (!Directory.Exists(UserProfile + "\\AppData\\Local\\yuzu"))
                        {
                            Directory.CreateDirectory(UserProfile + "\\AppData\\Local\\yuzu");
                        }
                        if (System.IO.File.Exists(UserProfile + "\\AppData\\Local\\yuzu\\yuzu Early Access Launcher.exe"))
                        {
                            System.IO.File.Delete(UserProfile + "\\AppData\\Local\\yuzu\\yuzu Early Access Launcher.exe");
                        }
                        System.IO.File.Copy(Path.GetFullPath(Application.ExecutablePath), UserProfile + "\\AppData\\Local\\yuzu\\yuzu Early Access Launcher.exe");
                        Process.Start(UserProfile + "\\AppData\\Local\\yuzu\\yuzu Early Access Launcher.exe");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Process.Start(UserProfile + "\\AppData\\Local\\yuzu\\yuzu Early Access Launcher.exe");
                        Environment.Exit(0);
                    }
                }
                System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\yuzu Early Access Launcher.lnk");
                System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\Programs\\yuzu Early Access Launcher.lnk");
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\yuzu Early Access Launcher.lnk");
                shortcut.Description = "Launch yuzu Early Access";
                shortcut.TargetPath = UserProfile + "\\AppData\\Local\\yuzu\\yuzu Early Access Launcher.exe";
                shortcut.Save();
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\Programs"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\Programs");
                }
                System.IO.File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\yuzu Early Access Launcher.lnk", Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\Programs\\yuzu Early Access Launcher.lnk");
            }

            Back = BackColor;
            Fore = ForeColor;
            theme = Theme;

            groupBox_Progress.ForeColor = Fore;
            groupBox_Firmware.ForeColor = Fore;
            groupBox_SystemInfo.ForeColor = Fore;
            groupBox_Settings.ForeColor = Fore;
            groupBox_HelpandSupport.ForeColor = Fore;
            comboBox_Theme.ForeColor = Fore;
            button_Launch.BackColor = ConBack;
            button_Download.BackColor = ConBack;
            button_Cancel.BackColor = ConBack;
            button_Firmware.BackColor = ConBack;
            button_Video.BackColor = ConBack;
            button_Support.BackColor = ConBack;
            button_Report.BackColor = ConBack;
            button_Compatibility.BackColor = ConBack;
            button_FAQ.BackColor = ConBack;
            button_About.BackColor = ConBack;
            comboBox_Theme.BackColor = ConBackBox;

            toolTip_1.SetToolTip(button_Video, "https://www.youtube.com/watch?v=nxNkwPrHAlo");
            toolTip_1.SetToolTip(pictureBox_Refresh, "Refresh");
            toolTip_1.SetToolTip(pictureBox_Settings, "Settings");
            toolTip_1.SetToolTip(button_Report, "Report an Issue with this Launcher.");
            toolTip_1.SetToolTip(button_Compatibility, "Check the Compatibiliy of different Games in yuzu as reported by Players using different hardwares from all over the World.");
            toolTip_1.SetToolTip(button_FAQ, "Visit yuzu FAQ page to find the Solutions to Common Problems in yuzu.");
            toolTip_1.SetToolTip(button_About, "More Information about yuzu and this Launcher.");

            if (ini.Read("DeleteyuzuArchive", "settings") == "true" || ini.Read("DeleteyuzuArchive", "settings") == "True")
            {
                DeleteyuzuArchive = true;
            }
            else
            {
                ini.Write("DeleteyuzuArchive", DeleteyuzuArchive.ToString(), "settings");
            }

            if (ini.Read("DeleteFirmwareArchive", "settings") == "true" || ini.Read("DeleteFirmwareArchive", "settings") == "True")
            {
                DeleteFirmwareArchive = true;
            }
            else
            {
                ini.Write("DeleteFirmwareArchive", DeleteFirmwareArchive.ToString(), "settings");
            }

            if (ini.Read("ExitLauncher", "settings") == "false" || ini.Read("ExitLauncher", "settings") == "False")
            {
                ExitLauncher = false;
            }
            else
            {
                ini.Write("ExitLauncher", ExitLauncher.ToString(), "settings");
            }

            String themeini = ini.Read("theme", "settings");
            if (themeini != "Light" && themeini != "Dark" && themeini != "Midnight Blue")
            {
                themeini = "Same as yuzu";
            }
            comboBox_Theme.Text = themeini;
            label_ThemeInfo.Visible = false;
            checkBox_Delyuzu.Checked = DeleteyuzuArchive;
            checkBox_DelFirm.Checked = DeleteFirmwareArchive;
            checkBox_Exit.Checked = ExitLauncher;
            log = System.IO.File.CreateText("Launcher.log");

            if (System.IO.File.Exists(UserProfile + "\\AppData\\Roaming\\yuzu\\config\\qt-config.ini"))
            {
                Ini yini = new Ini(UserProfile + "\\AppData\\Roaming\\yuzu\\config\\qt-config.ini");
                reg = Path.Combine(yini.Read("nand_directory", "Data%20Storage"), "system\\Contents\\registered");
            }
            if (reg == "")
            {
                reg = UserProfile + "\\AppData\\Roaming\\yuzu\\nand\\system\\Contents\\registered";
            }

            backgroundWorker_SystemInfo.RunWorkerAsync();
            backgroundWorker_Check.RunWorkerAsync();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            bool top = TopMost;
            TopMost = true;
            TopMost = top;
        }

        private void Button_Launch_Click(object sender, EventArgs e)
        {
            Process.Start("yuzu-windows-msvc-early-access\\yuzu.exe");
            if (ExitLauncher)
            {
                Environment.Exit(0);
            }
        }

        private void Button_Download_Click(object sender, EventArgs e)
        {
            backgroundWorker_Download.RunWorkerAsync(2);
        }

        private void Button_Firmware_Click(object sender, EventArgs e)
        {
            backgroundWorker_Download.RunWorkerAsync(3);
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void checkBox_Exit_CheckedChanged(object sender, EventArgs e)
        {
            ExitLauncher = checkBox_Exit.Checked;
            ini.Write("ExitLauncher", ExitLauncher.ToString(), "settings");
        }

        private void checkBox_DelFirm_CheckedChanged(object sender, EventArgs e)
        {
            DeleteFirmwareArchive = checkBox_DelFirm.Checked;
            ini.Write("DeleteFirmwareArchive", DeleteFirmwareArchive.ToString(), "settings");
        }

        private void checkBox_Delyuzu_CheckedChanged(object sender, EventArgs e)
        {
            DeleteyuzuArchive = checkBox_Delyuzu.Checked;
            ini.Write("DeleteyuzuArchive", DeleteyuzuArchive.ToString(), "settings");
        }

        private void pictureBox_Refresh_Click(object sender, EventArgs e)
        {
            log.WriteLine("\nRefreshing");

            progressBar1.Value = 0;
            label_Progress.Text = progressBar1.Value.ToString() + "%";

            button_Launch.Visible = false;
            button_Download.Visible = false;
            button_Firmware.Visible = true;
            button_Cancel.Visible = true;

            button_Launch.Font = new Font(button_Launch.Font.Name, button_Launch.Font.Size, FontStyle.Regular);
            button_Download.Font = new Font(button_Download.Font.Name, button_Download.Font.Size, FontStyle.Regular);
            button_Firmware.Font = new Font(button_Firmware.Font.Name, button_Firmware.Font.Size, FontStyle.Regular);

            button_Launch.Size = new Size(350, 60);
            button_Download.Size = new Size(350, 60);
            button_Download.Location = new Point(368, 225);
            
            groupBox_Progress.Visible = false;
            groupBox_Firmware.Visible = false;

            label_Info.Text = "";
            label_Speed.Visible = true;
            label_Info.Visible = false;
            label_Info.ForeColor = Fore;
            label_Info.Font = new Font(label_Info.Font.Name, label_Info.Font.Size, FontStyle.Regular);
            label_Firmware.Font = new Font(label_Firmware.Font.Name, label_Firmware.Font.Size, FontStyle.Regular);

            pictureBox_Refresh.Visible = false;
            backgroundWorker_Check.RunWorkerAsync();
        }

        private void pictureBox_Settings_Click(object sender, EventArgs e)
        {
            groupBox_SystemInfo.Visible = groupBox_Settings.Visible;
            groupBox_Settings.Visible = !groupBox_Settings.Visible;
        }

        private void comboBox_Theme_SelectedIndexChanged(object sender, EventArgs e)
        {
            String newtheme = comboBox_Theme.Text;
            if (newtheme == "Same as yuzu")
            {
                if (System.IO.File.Exists(UserProfile + "\\AppData\\Roaming\\yuzu\\config\\qt-config.ini"))
                {
                    Ini yini = new Ini(UserProfile + "\\AppData\\Roaming\\yuzu\\config\\qt-config.ini");
                    newtheme = yini.Read("theme", "UI");
                }
                else
                {
                    newtheme = "Light";
                }
            }
            if (newtheme == "qdarkstyle" || newtheme == "colorful_dark")
            {
                newtheme = "Dark";
            }
            if (newtheme == "qdarkstyle_midnight_blue" || newtheme == "colorful_midnight_blue")
            {
                newtheme = "Midnight Blue";
            }
            
            if (theme != newtheme)
            {
                label_ThemeInfo.Visible = true;
                
            }
            else
            {
                label_ThemeInfo.Visible = false;
            }

            ini.Write("theme", comboBox_Theme.Text, "settings");
        }

        private void comboBox_Theme_DropDownClosed(object sender, EventArgs e)
        {
            pictureBox_Settings.Focus();
        }

        private void Button_Video_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=nxNkwPrHAlo");
        }

        private void Button_Support_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCy3fBVKd0RMY05CgiiuGqSA?sub_confirmation=1");
        }

        private void Button_Report_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/HiDe-Techno-Tips/yuzu-Early-Access-Launcher/issues/new");
        }

        private void Button_Compatibility_Click(object sender, EventArgs e)
        {
            Process.Start("https://yuzu-emu.org/game/");
        }

        private void Button_FAQ_Click(object sender, EventArgs e)
        {
            Process.Start("https://yuzu-emu.org/wiki/faq/");
        }

        private void Button_About_Click(object sender, EventArgs e)
        {
            var AboutBox = new Form_AboutBox(version)
            {
                BackColor = Back,
                ForeColor = Fore,
                Owner = this,
                Text = "About yuzu Early Access and yuzu Early Access Launcher Version " + version.Replace('-', ' ')
            };
            AboutBox.ShowDialog();
        }

        private void Timer_Refresh_Tick(object sender, EventArgs e)
        {
            timer_Refresh.Enabled = false;
            pictureBox_Refresh_Click(sender, e);
        }

        private void BackgroundWorker_SystemInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            double GHz = 0, RAM = 0;
            String CPU = "";

            ManagementClass mc = new ManagementClass("win32_processor");
            foreach (ManagementObject mo in mc.GetInstances())
            {
                CPU = (string)mo["Name"];
                CPU = CPU.Replace("(TM)", "™").Replace("(tm)", "™").Replace("(R)", "®").Replace("(r)", "®").Replace("(C)", "©").Replace("(c)", "©").Replace("    ", " ").Replace("  ", " ");
                GHz = 0.001 * (UInt32)mo.Properties["CurrentClockSpeed"].Value;
            }

            ManagementScope oMs = new ManagementScope();
            ObjectQuery oQuery = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            ManagementObjectCollection oCollection = oSearcher.Get();
            foreach (ManagementObject obj in oCollection)
            {
                long mCap = Convert.ToInt64(obj["Capacity"]);
                RAM += mCap;
            }

            e.Result = new Tuple<String, double, double>(CPU, GHz, RAM / 1024 / 1024);
        }

        private void BackgroundWorker_SystemInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Tuple<String, double, double> Result = e.Result as Tuple<String, double, double>;
            String CPU = Result.Item1;
            double GHz = Result.Item2, RAM = Result.Item3;
            label_CPUName.Text = "CPU: " + CPU.Split('@')[0] + "@ " + GHz.ToString("0.0") + " GHz";
            label_RAMCapacity.Text = "RAM: " + (RAM / 1024).ToString("0.0") + " GB";
            int NumberOfLogicalProcessors = Environment.ProcessorCount;
            double MaxClockSpeed = GHz * 1000;

            if (NumberOfLogicalProcessors < 2)
            {
                label_CPUInfo.Font = new Font(label_CPUInfo.Font.Name, label_CPUInfo.Font.Size, FontStyle.Bold);
                label_CPUInfo.ForeColor = Color.Red;
                label_CPUInfo.Text = "This CPU is not powerful enough to handle yuzu.";
            }
            if (NumberOfLogicalProcessors < 4)
            {
                if (MaxClockSpeed < 2000)
                {
                    label_CPUInfo.Font = new Font(label_CPUInfo.Font.Name, label_CPUInfo.Font.Size, FontStyle.Bold);
                    label_CPUInfo.ForeColor = Color.Red;
                    label_CPUInfo.Text = "This CPU is not powerful enough to handle yuzu.";
                }
                else
                {
                    label_CPUInfo.Font = new Font(label_CPUInfo.Font.Name, label_CPUInfo.Font.Size, FontStyle.Bold);
                    label_CPUInfo.Text = "This CPU might handle Single Core Emulation in yuzu.";
                    if (MaxClockSpeed > 2700)
                    {
                        label_CPUInfo.Font = new Font(label_CPUInfo.Font.Name, label_CPUInfo.Font.Size, FontStyle.Regular);
                        label_CPUInfo.Text = "This CPU should handle Single Core Emulation in yuzu.";
                        if (MaxClockSpeed > 3400)
                        {
                            label_CPUInfo.Text = "This CPU will handle Single Core Emulation in yuzu.";
                        }
                    }
                }
            }
            else
            {
                if (MaxClockSpeed < 2000)
                {
                    label_CPUInfo.Text = "This CPU might handle Multi Core Emulation in yuzu.";
                    if (MaxClockSpeed < 1500)
                    {
                        label_CPUInfo.Font = new Font(label_CPUInfo.Font.Name, label_CPUInfo.Font.Size, FontStyle.Bold);
                        label_CPUInfo.ForeColor = Color.Red;
                        label_CPUInfo.Text = "This CPU is not powerful enough to handle yuzu.";
                    }
                }
                else
                {
                    label_CPUInfo.Text = "This CPU might be powerful enough to handle yuzu.";
                    if (MaxClockSpeed > 2700)
                    {
                        label_CPUInfo.Text = "This CPU should be powerful enough to handle yuzu.";
                        if (MaxClockSpeed > 3400)
                        {
                            label_CPUInfo.Text = "This CPU is powerful enough to handle yuzu.";
                        }
                    }
                }
            }

            if (RAM < 8000)
            {
                label_RAMInfo.Font = new Font(label_RAMInfo.Font.Name, label_RAMInfo.Font.Size, FontStyle.Bold);
                label_RAMInfo.Text = "This amount of RAM is not enough for yuzu to load heavy games.";
                if (RAM < 6000)
                {
                    label_RAMInfo.Font = new Font(label_RAMInfo.Font.Name, label_RAMInfo.Font.Size, FontStyle.Regular);
                    label_RAMInfo.Text = "This amount of RAM is not enough for yuzu to load large games.";
                    if (RAM < 4000)
                    {
                        label_RAMInfo.Font = new Font(label_RAMInfo.Font.Name, label_RAMInfo.Font.Size, FontStyle.Bold);
                        label_RAMInfo.ForeColor = Color.Red;
                        label_RAMInfo.Text = "This amount of RAM is enough for yuzu to load only light or small games.";
                        if (RAM < 2000)
                        {
                            label_RAMInfo.Text = "This amount of RAM is not enough for yuzu to load any game.";
                        }
                    }
                }
            }
            else
            {
                label_RAMInfo.Text = "This amount of RAM is enough for yuzu to load most games.";
                if (RAM > 12000)
                {
                    label_RAMInfo.Text = "This amount of RAM is enough for yuzu to load even the heaviest game.";
                    if (RAM > 16000)
                    {
                        label_RAMInfo.Text = "This amount of RAM is more than enough for yuzu.";
                    }
                }
            }
        }

        private void BackgroundWorker_Check_DoWork(object sender, DoWorkEventArgs e)
        {
            log.AutoFlush = true;
            if (System.IO.File.Exists(UserProfile + "\\AppData\\Local\\Temp\\yuzu Early Access Launcher.exe"))
            {
                System.IO.File.Delete(UserProfile + "\\AppData\\Local\\Temp\\yuzu Early Access Launcher.exe");
            }

            backgroundWorker_Check.ReportProgress(1);
            String yuzulauncher = "", yuzu = "", firminf = "";
            try
            {
                WebClient wc = new WebClient();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                wc.Headers.Add("user-agent", "request");
                yuzulauncher = wc.DownloadString(new System.Uri("https://api.github.com/repos/HiDe-Techno-Tips/yuzu-Early-Access-Launcher/releases/latest"));
                wc.Headers.Add("user-agent", "request");
                yuzu = wc.DownloadString(new System.Uri("https://api.github.com/repos/pineappleEA/pineapple-src/releases/latest"));
                wc.Headers.Add("user-agent", "request");
                firminf = wc.DownloadString(new System.Uri("https://raw.githubusercontent.com/HiDe-Techno-Tips/Nintendo-Switch-Files/main/index.json"));
                Internet = true;
                log.WriteLine(" Internet Connection is available");
            }
            catch (Exception)
            {
                Internet = false;
                log.WriteLine(" Internet Connection is not available");
            }

            if (Internet)
            {

                lfirm = JsonDocument.Parse("[" + JsonDocument.Parse("[ " + firminf + " ]").RootElement[0].GetProperty("firmware").ToString() + "]").RootElement[0].GetProperty("ver").ToString();
                furl = JsonDocument.Parse("[" + JsonDocument.Parse("[ " + firminf + " ]").RootElement[0].GetProperty("firmware").ToString() + "]").RootElement[0].GetProperty("url").ToString();
                fsize = JsonDocument.Parse("[" + JsonDocument.Parse("[ " + firminf + " ]").RootElement[0].GetProperty("firmware").ToString() + "]").RootElement[0].GetProperty("size").ToString();
                kurl = JsonDocument.Parse("[" + JsonDocument.Parse("[ " + firminf + " ]").RootElement[0].GetProperty("keys").ToString() + "]").RootElement[0].GetProperty("url").ToString();

                if (System.IO.File.Exists("prod.keys") || System.IO.File.Exists(UserProfile + "\\AppData\\Roaming\\yuzu\\keys\\prod.keys"))
                {
                    backgroundWorker_Check.ReportProgress(2);
                }
                else
                {
                    backgroundWorker_Check.ReportProgress(3);
                }

                try
                {
                    WebClient wc = new WebClient();
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                    wc.Headers.Add("user-agent", "request");
                    wc.DownloadFile(new System.Uri(kurl), UserProfile + "\\AppData\\Local\\Temp\\prod.keys");
                    log.WriteLine(" Done");
                }
                catch (Exception)
                {
                    backgroundWorker_Check.ReportProgress(4);
                }

                if (System.IO.File.Exists(UserProfile + "\\AppData\\Local\\Temp\\prod.keys"))
                {
                    if (System.IO.File.Exists("prod.keys"))
                    {
                        System.IO.File.Delete("prod.keys");
                    }
                    log.WriteLine("Moving \"" + UserProfile + "\\AppData\\Local\\Temp\\prod.keys\" to \"" + path + "\\prod.keys\"");
                    System.IO.File.Move(UserProfile + "\\AppData\\Local\\Temp\\prod.keys", "prod.keys");
                    log.WriteLine(" Done");
                }

                backgroundWorker_Check.ReportProgress(5);

                launcherlatest = JsonDocument.Parse("[ " + yuzulauncher + " ]").RootElement[0].GetProperty("tag_name").ToString().Split('v')[1];
                if (branch == "Preview")
                {
                    launcherlatest = version;
                }
                VersionName = "Version " + version;

                lurl = JsonDocument.Parse(JsonDocument.Parse("[ " + yuzulauncher + " ]").RootElement[0].GetProperty("assets").ToString()).RootElement[0].GetProperty("browser_download_url").ToString();
                lsize = JsonDocument.Parse(JsonDocument.Parse("[ " + yuzulauncher + " ]").RootElement[0].GetProperty("assets").ToString()).RootElement[0].GetProperty("size").ToString();
                latest = JsonDocument.Parse("[ " + yuzu + " ]").RootElement[0].GetProperty("tag_name").ToString().Split('-').Last();
                url = JsonDocument.Parse(JsonDocument.Parse("[ " + yuzu + " ]").RootElement[0].GetProperty("assets").ToString()).RootElement[0].GetProperty("browser_download_url").ToString();
                ysize = JsonDocument.Parse(JsonDocument.Parse("[ " + yuzu + " ]").RootElement[0].GetProperty("assets").ToString()).RootElement[0].GetProperty("size").ToString();

                log.WriteLine(" yuzu Early Access " + latest + " is the latest available");
                log.WriteLine(" Switch Firmware " + lfirm + " is the latest available");
            }

            backgroundWorker_Check.ReportProgress(6);

            if (System.IO.File.Exists("prod.keys"))
            {
                if (!Directory.Exists(UserProfile + "\\AppData\\Roaming\\yuzu\\keys"))
                {
                    Directory.CreateDirectory(UserProfile + "\\AppData\\Roaming\\yuzu\\keys");
                }
                if (System.IO.File.Exists(UserProfile + "\\AppData\\Roaming\\yuzu\\keys\\prod.keys"))
                {
                    System.IO.File.Delete(UserProfile + "\\AppData\\Roaming\\yuzu\\keys\\prod.keys");
                }
                log.WriteLine("Copying \"" + path + "\\prod.keys\" to \"" + UserProfile + "\\AppData\\Roaming\\yuzu\\keys\\prod.keys\"");
                System.IO.File.Copy("prod.keys", UserProfile + "\\AppData\\Roaming\\yuzu\\keys\\prod.keys");
                log.WriteLine(" Done");
            }

            if (System.IO.File.Exists("launcher.ini"))
            {
                log.WriteLine("Reading \"" + path + "\\launcher.ini\"");
                installed = ini.Read("version", "installed");
                firm = ini.Read("firm", "installed");
                if (installed != "")
                {
                    log.WriteLine(" Detected yuzu Early Access " + installed + " from \"" + path + "\\launcher.ini\"");
                }
                else
                {
                    log.WriteLine(" Not detected yuzu Early Access from \"" + path + "\\launcher.ini\"");
                }
                if (firm != "")
                {
                    log.WriteLine(" Detected Switch Firmware " + firm + " from \"" + path + "\\launcher.ini\"");
                }
                else
                {
                    log.WriteLine(" Not detected Switch Firmware from \"" + path + "\\launcher.ini\"");
                }

                try
                {
                    if (Directory.GetFiles(reg, "*").Last().Split('.').Last() != "nca")
                    {
                        log.WriteLine(" But Switch Firmware is not installed in \"" + reg + "\"");
                        firm = "";
                    }
                }
                catch (Exception)
                {
                    log.WriteLine(" But Switch Firmware is not installed in \"" + reg + "\"");
                    firm = "";
                }
            }

            if (installed == "")
            {
                log.WriteLine("Checking for \"" + path + "\\yuzu-windows-msvc-early-access\\yuzu.exe\"");
                if (System.IO.File.Exists("yuzu-windows-msvc-early-access\\yuzu.exe"))
                {
                    log.WriteLine(" Preinstalled yuzu Early Access found in \"" + path + "\\yuzu-windows-msvc-early-access\"");
                    installed = "pre";
                }
                else
                {
                    log.WriteLine(" Not found");
                }
            }
            else
            {
                if (!System.IO.File.Exists("yuzu-windows-msvc-early-access\\yuzu.exe"))
                {
                    log.WriteLine(" But yuzu Early Access is not installed in \"" + path + "\\yuzu-windows-msvc-early-access\"");
                    installed = "";
                }
                else
                {
                    log.WriteLine(" yuzu Early Access is installed in \"" + path + "\\yuzu-windows-msvc-early-access\"");
                }
            }

            if (installed == "")
            {
                installed = "0";
            }
            if (installed == "pre")
            {
                installed = "1";
            }
            if (firm == "")
            {
                firm = "0.0.0";
            }
            if (latest == "")
            {
                latest = "0";
            }
            if (lfirm == "")
            {
                lfirm = "0.0.0";
            }

            String latestfileVer = installed;
            String latestfirfileVer = firm;

            log.WriteLine("Checking Files");
            for (int i = 0; i < 2; i++)
            {
                foreach (string f in Directory.EnumerateFiles(path, "Windows-Yuzu-EA-*.7z"))
                {
                    int fVer = int.Parse(f.Split('\\').Last().Split('-', '.')[3]);

                    if (fVer < int.Parse(latestfileVer))
                    {
                        System.IO.File.Delete(f);
                    }
                    else
                    {
                        latestfileVer = fVer.ToString();
                    }
                    
                    if (int.Parse(latestfileVer) > int.Parse(latest) || int.Parse(latestfileVer) > int.Parse(installed) || !Internet)
                    {
                        latest = latestfileVer;
                    }
                    
                    if (Internet)
                    {
                        if (DeleteyuzuArchive)
                        {
                            if ((fVer < int.Parse(latestfileVer) && fVer < int.Parse(latest)) || int.Parse(latestfileVer) == int.Parse(installed))
                            {
                                System.IO.File.Delete(f);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                foreach (string f in Directory.EnumerateFiles(path, "Switch-Firmware-*.zip"))
                {
                    Version fVer = Version.Parse(f.Split('\\').Last().Split('-', '.')[2] + "." + f.Split('\\').Last().Split('-', '.')[3] + "." + f.Split('\\').Last().Split('-', '.')[4]);

                    if (fVer < Version.Parse(latestfirfileVer))
                    {
                        System.IO.File.Delete(f);
                    }
                    else
                    {
                        latestfirfileVer = fVer.ToString();
                    }

                    if (Version.Parse(latestfirfileVer) > Version.Parse(lfirm) || Version.Parse(latestfirfileVer) > Version.Parse(firm) || !Internet)
                    {
                        lfirm = latestfirfileVer;
                    }

                    if (Internet)
                    {
                        if (DeleteFirmwareArchive)
                        {
                            if ((fVer < Version.Parse(latestfirfileVer) && fVer < Version.Parse(lfirm)) || Version.Parse(latestfirfileVer) == Version.Parse(firm))
                            {
                                System.IO.File.Delete(f);
                            }
                        }
                    }
                }
            }

            if (installed == "0")
            {
                installed = "";
            }
            if (installed == "1")
            {
                installed = "pre";
            }
            if (firm == "0.0.0")
            {
                firm = "";
            }
            if (latest == "0")
            {
                latest = "";
            }
            if (lfirm == "0.0.0")
            {
                lfirm = "";
            }

            file = "Windows-Yuzu-EA-" + latest + ".7z";
            firfile = "Switch-Firmware-" + lfirm + ".zip";

            if (System.IO.File.Exists(file))
            {
                log.WriteLine(" Latest found yuzu Early Access file: \"" + path + "\\Windows-Yuzu-EA-" + latest + ".7z\"");
            }
            else
            {
                log.WriteLine(" Not found file \"" + path + "\\Windows-Yuzu-EA-" + latest + ".7z\"");
            }
            if (System.IO.File.Exists(firfile))
            {
                log.WriteLine(" Latest found Switch Firmware file: \"" + path + "\\Switch-Firmware-" + lfirm + ".zip\"");
            }
            else
            {
                log.WriteLine(" Not found file \"" + path + "\\Switch-Firmware-" + lfirm + ".zip\"");
            }

            if (Internet && version != launcherlatest)
            {
                Downloading = true;
                backgroundWorker_Check.ReportProgress(7);
                while (Downloading)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        private void BackgroundWorker_Check_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label_Message.Size = new Size(728, 194);
            if (e.ProgressPercentage == 1)
            {
                log.WriteLine("Checking Internet Connection");
                label_Message.Text = "Checking Internet Connection!";
                label_Message.Visible = true;
            }
            if (e.ProgressPercentage == 2)
            {
                log.WriteLine("Retrieving latest \"prod.keys\" from \"" + kurl + "\" to \"" + UserProfile + "\\AppData\\Local\\Temp\\prod.keys\"");
                label_Message.Text = "Downloading prod.keys!";
                label_Message.Visible = true;
            }
            if (e.ProgressPercentage == 3)
            {
                log.WriteLine("Downloading \"" + UserProfile + "\\AppData\\Local\\Temp\\prod.keys\" from \"" + kurl + "\"");
                label_Message.Text = "Retrieving Latest prod.keys!";
                label_Message.Visible = true;
            }
            if (e.ProgressPercentage == 4)
            {
                log.WriteLine(" Failed Downloading \"" + path + "\\prod.keys\" from \"" + kurl + "\"");
                label_Info.Text = "Could not download latest prod.keys!";
            }
            if (e.ProgressPercentage == 5)
            {
                log.WriteLine("Checking for Updates");
                label_Message.Text = "Checking for Updates!";
                label_Message.Visible = true;
            }
            if (e.ProgressPercentage == 6)
            {
                label_Message.Text = "Reading Metadata!";
                label_Message.Visible = true;
            }
            if (e.ProgressPercentage == 7)
            {
                backgroundWorker_Download.RunWorkerAsync(1);
            }
        }

        private void BackgroundWorker_Check_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            log.WriteLine("Updating UI");
            label_Message.Visible = false;

            if (installed != "")
            {
                button_Launch.Font = new Font(button_Launch.Font.Name, button_Launch.Font.Size, FontStyle.Bold);
                button_Launch.Text = "Launch yuzu Early Access " + installed;
                toolTip_1.SetToolTip(button_Launch, path + "\\yuzu-windows-msvc-early-access\\yuzu.exe");
                if (installed == "pre")
                {
                    button_Launch.Text = "Launch yuzu Early Access";
                }
            }

            if (Internet)
            {
                if (installed == "")
                {
                    if (System.IO.File.Exists(file))
                    {
                        button_Download.Text = "Install yuzu Early Access " + latest;
                        toolTip_1.SetToolTip(button_Download, path + "\\" + file);
                    }
                    else
                    {
                        button_Download.Text = "Download yuzu Early Access " + latest;
                        toolTip_1.SetToolTip(button_Download, url);
                    }
                    button_Launch.Font = new Font(button_Launch.Font.Name, button_Launch.Font.Size, FontStyle.Regular);
                    button_Download.Font = new Font(button_Download.Font.Name, button_Download.Font.Size, FontStyle.Bold);
                    button_Download.Size = new Size(706, 60);
                    button_Download.Location = button_Launch.Location;
                }
                else
                {
                    if (installed == latest)
                    {
                        if (System.IO.File.Exists(file))
                        {
                            button_Download.Text = "Reinstall yuzu Early Access " + latest;
                            toolTip_1.SetToolTip(button_Download, path + "\\" + file);
                        }
                        else
                        {
                            button_Download.Text = "Redownload yuzu Early Access " + latest;
                            toolTip_1.SetToolTip(button_Download, url);
                        }
                    }
                    else
                    {
                        if (System.IO.File.Exists(file))
                        {
                            button_Download.Text = "Update to yuzu Early Access " + latest;
                            toolTip_1.SetToolTip(button_Download, path + "\\" + file);
                        }
                        else
                        {
                            button_Download.Text = "Download yuzu Early Access " + latest;
                            toolTip_1.SetToolTip(button_Download, url);
                        }
                        button_Download.Font = new Font(button_Download.Font.Name, button_Download.Font.Size, FontStyle.Bold);
                        button_Launch.Font = new Font(button_Launch.Font.Name, button_Launch.Font.Size, FontStyle.Regular);
                    }

                    button_Launch.Visible = true;
                }

                button_Download.Visible = true;

                if (firm == "")
                {
                    if (System.IO.File.Exists(firfile))
                    {
                        button_Firmware.Text = "Install Firmware " + lfirm;
                        toolTip_1.SetToolTip(button_Firmware, path + "\\" + firfile);
                    }
                    else
                    {
                        button_Firmware.Text = "Download Firmware " + lfirm;
                        toolTip_1.SetToolTip(button_Firmware, furl);
                    }
                    button_Firmware.Font = new Font(button_Firmware.Font.Name, button_Firmware.Font.Size, FontStyle.Bold);
                }
                else
                {
                    if (lfirm == firm)
                    {
                        if (System.IO.File.Exists(firfile))
                        {
                            button_Firmware.Text = "Reinstall Firmware " + lfirm;
                            toolTip_1.SetToolTip(button_Firmware, path + "\\" + firfile);
                        }
                        else
                        {
                            button_Firmware.Text = "Redownload Firmware " + lfirm;
                            toolTip_1.SetToolTip(button_Firmware, furl);
                        }
                    }
                    else
                    {
                        button_Firmware.Font = new Font(button_Firmware.Font.Name, button_Firmware.Font.Size, FontStyle.Bold);
                        if (System.IO.File.Exists(firfile))
                        {
                            button_Firmware.Text = "Update Firmware to " + lfirm;
                            toolTip_1.SetToolTip(button_Firmware, path + "\\" + firfile);
                        }
                        else
                        {
                            button_Firmware.Text = "Download Firmware " + lfirm;
                            toolTip_1.SetToolTip(button_Firmware, furl);
                        }
                    }
                }
            }

            groupBox_Firmware.Visible = true;

            if (!Internet)
            {
                toolTip_1.SetToolTip(button_Download, path + "\\" + file);

                if (installed == "")
                {
                    if (System.IO.File.Exists(file))
                    {
                        button_Download.Font = new Font(button_Download.Font.Name, button_Download.Font.Size, FontStyle.Bold);
                        button_Download.Text = "Install yuzu Early Access " + latest;
                        button_Download.Size = new Size(706, 60);
                        button_Download.Location = button_Launch.Location;
                        button_Download.Visible = true;
                    }
                    else
                    {
                        label_Message.Text = "yuzu Early Access is not Installed";
                        label_Message.Size = new Size(728, 91);
                        label_Message.Visible = true;
                    }
                    label_Info.Font = new Font(label_Info.Font.Name, label_Info.Font.Size, FontStyle.Bold);
                    label_Info.ForeColor = Color.Red;
                    label_Info.Text = "Error retrieving Updates! Check Your Internet and Refresh.";
                    label_Info.Visible = true;
                }
                else
                {
                    if (latest == installed)
                    {
                        button_Download.Text = "Reinstall yuzu Early Access " + latest;
                    }
                    else
                    {
                        button_Download.Font = new Font(button_Download.Font.Name, button_Download.Font.Size, FontStyle.Bold);
                        button_Launch.Font = new Font(button_Launch.Font.Name, button_Launch.Font.Size, FontStyle.Regular);
                        button_Download.Text = "Install yuzu Early Access " + latest;
                    }
                    
                    if (System.IO.File.Exists(file))
                    {
                        button_Download.Visible = true;
                    }
                    else
                    {
                        button_Launch.Size = new Size(706, 60);
                    }

                    if (!System.IO.File.Exists("prod.keys") && !System.IO.File.Exists(UserProfile + "\\AppData\\Roaming\\yuzu\\keys\\prod.keys"))
                    {
                        label_Info.Font = new Font(label_Info.Font.Name, label_Info.Font.Size, FontStyle.Bold);
                        label_Info.ForeColor = Color.Red;
                        label_Info.Text = "Could not find prod.keys! Check Your Internet and Refresh.";
                    }
                    else
                    {
                        label_Info.Font = new Font(label_Info.Font.Name, label_Info.Font.Size, FontStyle.Bold);
                        label_Info.Text = "prod.keys may be old! Check Your Internet and Refresh.";
                    }

                    button_Launch.Visible = true;
                    label_Info.Visible = true;
                }

                if (System.IO.File.Exists(firfile))
                {
                    toolTip_1.SetToolTip(button_Firmware, path + "\\" + firfile);

                    if (lfirm == firm)
                    {
                        button_Firmware.Text = "Reinstall Firmware " + lfirm;
                        
                    }
                    else
                    {
                        button_Firmware.Font = new Font(button_Firmware.Font.Name, button_Firmware.Font.Size, FontStyle.Bold);
                        button_Firmware.Text = "Install Firmware " + lfirm;
                    }
                }
                else
                {
                    button_Firmware.Visible = false;
                }
            }

            if (firm != "")
            {
                label_Firmware.Text = "Installed Version: " + firm;
            }
            else
            {
                label_Firmware.Font = new Font(label_Firmware.Font.Name, label_Firmware.Font.Size, FontStyle.Bold);
                label_Firmware.Text = "Firmware is not Installed. Some Games may not work.";
            }
            pictureBox_Refresh.Visible = true;
        }

        private void BackgroundWorker_Download_DoWork(object sender, DoWorkEventArgs e)
        {
            if ((int)e.Argument == 1)
            {
                backgroundWorker_Download.ReportProgress(1);
            }
            if ((int)e.Argument == 2)
            {
                backgroundWorker_Download.ReportProgress(2);
            }
            if ((int)e.Argument == 3)
            {
                backgroundWorker_Download.ReportProgress(3);
            }
        }

        private async void BackgroundWorker_Download_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = 0;
            label_Progress.Text = progressBar1.Value.ToString() + "%";
            if (e.ProgressPercentage == 1)
            {
                if (await Download(lurl, lsize, UserProfile + "\\AppData\\Local\\Temp\\yuzu Early Access Launcher.exe") == 0)
                {
                    Process.Start(UserProfile + "\\AppData\\Local\\Temp\\yuzu Early Access Launcher.exe");
                    Environment.Exit(0);
                }
            }
            if (e.ProgressPercentage == 2)
            {
                if (!System.IO.File.Exists(file))
                {
                    foreach (string f in Directory.EnumerateFiles(UserProfile + "\\AppData\\Local\\Temp\\", "Windows-Yuzu-EA-*.7z"))
                    {
                        System.IO.File.Delete(f);
                    }
                    if (await Download(url, ysize, UserProfile + "\\AppData\\Local\\Temp\\" + file) == 0)
                    {
                        foreach (string f in Directory.EnumerateFiles(path, "Windows-Yuzu-EA-*.7z"))
                        {
                            System.IO.File.Delete(f);
                        }
                        log.WriteLine("Moving \"" + UserProfile + "\\AppData\\Local\\Temp\\" + file + "\" to \"" + path + "\\" + file + "\"");
                        System.IO.File.Move(UserProfile + "\\AppData\\Local\\Temp\\" + file, file);
                        log.WriteLine(" Done");
                    }
                }
                if (System.IO.File.Exists(file))
                {
                    if (Directory.Exists("yuzu-windows-msvc-early-access"))
                    {
                        Directory.Delete("yuzu-windows-msvc-early-access", true);
                    }
                    if (Directory.Exists("Temp"))
                    {
                        Directory.Delete("Temp", true);
                    }
                    Directory.CreateDirectory("Temp");
                    await Extract(path + "\\" + file, path + "\\Temp");
                    log.WriteLine("Moving \"" + path + "\\Temp\\yuzu-windows-msvc-early-access\" to \"" + path + "\\yuzu-windows-msvc-early-access\"");
                    Directory.Move("Temp\\yuzu-windows-msvc-early-access", path + "\\yuzu-windows-msvc-early-access");
                    log.WriteLine(" Done");
                    Directory.Delete("Temp", true);
                    ini.Write("version", latest, "installed");
                }
            }
            if (e.ProgressPercentage == 3)
            {
                if (!System.IO.File.Exists(firfile))
                {
                    foreach (string f in Directory.EnumerateFiles(UserProfile + "\\AppData\\Local\\Temp\\", "Switch-Firmware-*.7z"))
                    {
                        System.IO.File.Delete(f);
                    }
                    if (await Download(furl, fsize, UserProfile + "\\AppData\\Local\\Temp\\" + firfile) == 0)
                    {
                        foreach (string f in Directory.EnumerateFiles(path, "Switch-Firmware-*.7z"))
                        {
                            System.IO.File.Delete(f);
                        }
                        log.WriteLine("Moving \"" + UserProfile + "\\AppData\\Local\\Temp\\" + firfile + "\" to \"" + path + "\\" + firfile + "\"");
                        System.IO.File.Move(UserProfile + "\\AppData\\Local\\Temp\\" + firfile, firfile);
                        log.WriteLine(" Done");
                    }
                }
                if (System.IO.File.Exists(firfile))
                {
                    if (reg != "")
                    {
                        if (Directory.Exists(reg))
                        {
                            try
                            {
                                Directory.Delete(reg, true);
                            }
                            catch (Exception)
                            {
                                ;
                            }
                        }
                        Directory.CreateDirectory(reg);
                        await Extract(path + "\\" + firfile, reg);
                        ini.Write("firm", lfirm, "installed");
                    }
                }
            }
            timer_Refresh.Enabled = true;
        }

        private async Task<int> Download(String durl, String dsize, String dsave)
        {
            try
            {
                log.WriteLine("Downloading \"" + dsave + "\" from \"" + durl + "\"");
                label_Message.Visible = false;
                button_Launch.Visible = false;
                button_Download.Visible = false;
                groupBox_Firmware.Visible = false;
                label_Info.Visible = false;
                pictureBox_Refresh.Visible = false;

                string[] sfile = dsave.Split('\\');
                groupBox_Progress.Text = "Downloading " + sfile[6];
                label_Time.Text = "This will take time depending on your network speed.";
                groupBox_Progress.Visible = true;

                Downloading = true;
                System.IO.File.Delete(dsave);
                WebClient wc = new WebClient();
                wc.DownloadProgressChanged += (sender, e) => DownloadProgressChanged(e, dsize);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                wc.Headers.Add("user-agent", "request");
                await wc.DownloadFileTaskAsync(new System.Uri(durl), dsave);
                progressBar1.Value = 100;
                label_Progress.Text = progressBar1.Value.ToString() + "%";
                log.WriteLine(" Done");
                Downloading = false;
                return 0;
            }
            catch (Exception)
            {
                Downloading = false;
                log.WriteLine(" Failed Downloading \"" + dsave + "\" from \"" + durl + "\"");
                return 1;
            }
        }

        private void DownloadProgressChanged(ProgressChangedEventArgs e, String dsize)
        {
            /*    
                float KiloBytesPerSecond = 0;

                var now = DateTime.Now;
                if (lastBytes != 0)
                {
                    var timeSpan = now - lastUpdate;
                    var bytesChange = bytes - lastBytes;
                    if (timeSpan.Milliseconds != 0)
                    {
                        var bytesPerSecond = bytesChange / timeSpan.Milliseconds;
                        KiloBytesPerSecond = bytesPerSecond * 1000 / 1024;
                        lastBytes = bytes;
                        lastUpdate = now;
                    }
                }
                else
                {
                    lastBytes = bytes;
                    lastUpdate = now;
                }

                String Speed = KiloBytesPerSecond.ToString("0") + " KB/s";
                if (KiloBytesPerSecond >= 1024)
                {
                    Speed = (KiloBytesPerSecond / 1024).ToString("0.00") + " MB/s";
                }
            */


            progressBar1.Value = e.ProgressPercentage;
            label_Progress.Text = e.ProgressPercentage.ToString() + "%";
            label_Speed.Text = (e.ProgressPercentage * float.Parse(dsize) / 100 / 1024 / 1024).ToString("0.00") + " MB of " + (float.Parse(dsize) / 1024 / 1024).ToString("0.00") + " MB Completed";
        }

        private async Task<int> Extract(String xfile, String xdir)
        {
            log.WriteLine("Extracting \"" + xfile + "\" to \"" + xdir + "\"");
            label_Message.Visible = false;
            button_Launch.Visible = false;
            button_Download.Visible = false;
            groupBox_Firmware.Visible = false;
            label_Info.Visible = false;
            pictureBox_Refresh.Visible = false;
            groupBox_Progress.Text = "Extracting " + xfile.Split('\\').Last();
            label_Time.Text = "This may take some time.";
            groupBox_Progress.Visible = true;
            label_Speed.Visible = false;
            button_Cancel.Visible = false;

            if (xfile.Split('.').Last() == "7z")
            {
                var archive = SharpCompress.Archives.SevenZip.SevenZipArchive.Open(xfile);
                timer_ExtractProgress.Enabled = true;
                xsdir = xdir;
                xdsize = archive.TotalUncompressSize;
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    await Task.Run(() => entry.WriteToDirectory(xdir, new ExtractionOptions()
                    {
                        ExtractFullPath = true,
                        Overwrite = true
                    }));
                }
            }
            if (xfile.Split('.').Last() == "zip")
            {
                var archive = SharpCompress.Archives.Zip.ZipArchive.Open(xfile);
                timer_ExtractProgress.Enabled = true;
                xsdir = xdir;
                xdsize = archive.TotalUncompressSize;
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    await Task.Run(() => entry.WriteToDirectory(xdir, new ExtractionOptions()
                    {
                        ExtractFullPath = true,
                        Overwrite = true
                    }));
                }
            }

            progressBar1.Value = 100;
            label_Progress.Text = progressBar1.Value.ToString() + "%";
            log.WriteLine(" Done");
            return 0;
        }

        private void Timer_ExtractProgress_Tick(object sender, EventArgs e)
        {
            if (Directory.Exists(xsdir))
            {
                int zprogress = int.Parse((DirSize(new DirectoryInfo(xsdir)) * 100 / xdsize).ToString("0"));
                if (zprogress > 100)
                {
                    zprogress = 100;
                }
                progressBar1.Value = zprogress;
                label_Progress.Text = progressBar1.Value.ToString() + "%";
            }
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
