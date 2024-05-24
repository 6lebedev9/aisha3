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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void SaveFormBorderColor(string RGB, byte Value)
        {
            System.Drawing.Color borderColor = Properties.Settings.Default.FormBorderColor;
            switch (RGB)
            {
                case "R":
                    byte RchangedColorR = Value;
                    byte RchangedColorG = borderColor.G;
                    byte RchangedColorB = borderColor.B;
                    System.Drawing.Color RchangedColor = Color.FromArgb(RchangedColorR, RchangedColorG, RchangedColorB);
                    Properties.Settings.Default.FormBorderColor = RchangedColor;
                    Properties.Settings.Default.Save();
                    break;
                case "G":
                    byte GchangedColorR = borderColor.R;
                    byte GchangedColorG = Value;
                    byte GchangedColorB = borderColor.B;
                    System.Drawing.Color GchangedColor = Color.FromArgb(GchangedColorR, GchangedColorG, GchangedColorB);
                    Properties.Settings.Default.FormBorderColor = GchangedColor;
                    Properties.Settings.Default.Save();
                    break;
                case "B":
                    byte BchangedColorR = borderColor.R;
                    byte BchangedColorG = borderColor.G;
                    byte BchangedColorB = Value;
                    System.Drawing.Color BchangedColor = Color.FromArgb(BchangedColorR, BchangedColorG, BchangedColorB);
                    Properties.Settings.Default.FormBorderColor = BchangedColor;
                    Properties.Settings.Default.Save();
                    break;
                default:
                    break;
            }
        }

        private void FormBorderColorNumR_ValueChanged(object sender, EventArgs e)
        {
            SaveFormBorderColor("R", Convert.ToByte(FormBorderColorNumR.Value));
        }

        private void FormBorderColorNumG_ValueChanged(object sender, EventArgs e)
        {
            SaveFormBorderColor("G", Convert.ToByte(FormBorderColorNumG.Value));
        }

        private void FormBorderColorNumB_ValueChanged(object sender, EventArgs e)
        {
            SaveFormBorderColor("B", Convert.ToByte(FormBorderColorNumB.Value));
        }

        private void TBoxYaMaps_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.YaMapHttp = TBoxYaMaps.Text.ToString();
            Properties.Settings.Default.Save();
        }

        private void TBoxCustomRiba_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CustomRiba = TBoxCustomRiba.Text.ToString();
            Properties.Settings.Default.Save();
        }

        private void TBoxSqlDb_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqldb = TBoxSqlDb.Text.ToString();
            Properties.Settings.Default.Save();
        }

        private void TBoxSqlCatalog_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqlcatalog = TBoxSqlCatalog.Text.ToString();
            Properties.Settings.Default.Save();
        }

        private void TBoxSqlUser_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqluser = TBoxSqlUser.Text.ToString();
            Properties.Settings.Default.Save();
        }

        private void TBoxSqlPass_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqlpass = TBoxSqlPass.Text.ToString();
            Properties.Settings.Default.Save();
        }
    }
}
