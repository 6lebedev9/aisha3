using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aisha3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            WindowLocSaveLoad("Load");
            WindowBorderColorLoad();
        }
        //Settings save/load
        private void WindowLocSaveLoad(string LoadOrSave)
        {
            if (LoadOrSave == "Load")
            {
                this.Location = Properties.Settings.Default.LastWinLocation;
            }
            else
            {
                Properties.Settings.Default.LastWinLocation = RestoreBounds.Location;
                Properties.Settings.Default.Save();
            }
        }
        private void WindowBorderColorLoad()
        {
            System.Drawing.Color borderColor = Properties.Settings.Default.FormBorderColor;
            ControlPanelOuter.BackColor = borderColor;
            SortPanelOuter.BackColor = borderColor;
            SortPrefPanelOuter.BackColor = borderColor;
            MainPanelOuter.BackColor = borderColor;
            CommentPanelOuter.BackColor = borderColor;
            MapPanelOuter.BackColor= borderColor;
        }
        //Settings save/load

        //Control panel buttons
        private void BtnClose_Click(object sender, EventArgs e)
        {
            WindowLocSaveLoad("Save");
            this.Close();
        }
        private void BtnCollapse_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        Point lastLocFormPoint;
        private void ControlPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastLocFormPoint.X;
                this.Top += e.Y - lastLocFormPoint.Y;
            }
        }
        private void ControlPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastLocFormPoint = new Point(e.X, e.Y);
        }
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            bool SettingsOpened = Application.OpenForms.Count > 1;
            if (!SettingsOpened)
            {
                Form Settings = new Settings();
                Settings.Show();
            }
        }

        private void BtnStatusLAN_Click(object sender, EventArgs e)
        {

        }
        //Control panel buttons


    }
}
