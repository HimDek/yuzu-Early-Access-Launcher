using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace yuzu_Early_Access_Launcher
{
    public partial class Form_AboutBox : Form
    {
        public Form_AboutBox(String ver)
        {
            InitializeComponent();
            label_LauncherVersion.Text = "Version " + ver.Replace('-', ' ');
        }

        private void link_yuzuSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://yuzu-emu.org/");
        }

        private void link_yuzuSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/yuzu-emu");
        }

        private void link_yuzuContrib_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/yuzu-emu/yuzu/graphs/contributors");
        }

        private void link_yuzuLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/yuzu-emu/yuzu/blob/master/license.txt");
        }

        private void linkLabel_LauncherSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/HimDek/yuzu-Early-Access-Launcher");
        }

        private void linkLabel_EARepo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/pineappleEA/pineapple-src/releases");
        }

        private void linkLabel_prodRepo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://archive.org/download/prod.keys/");
        }

        private void linkLabel_FirmwareRepo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://archive.org/download/nintendo-switch-global-firmwares/");
        }
    }
}
