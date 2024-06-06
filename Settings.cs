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

        // AES
        private string CollectSettingsData()
        {
            StringBuilder settingsData = new StringBuilder();

            settingsData.AppendLine("FormBorderColorR=" + FormBorderColorNumR.Value);
            settingsData.AppendLine("FormBorderColorG=" + FormBorderColorNumG.Value);
            settingsData.AppendLine("FormBorderColorB=" + FormBorderColorNumB.Value);
            settingsData.AppendLine("YaMapHttp=" + TBoxYaMaps.Text);
            settingsData.AppendLine("CustomRiba=" + TBoxCustomRiba.Text);
            settingsData.AppendLine("mssqldb=" + TBoxSqlDb.Text);
            settingsData.AppendLine("mssqlcatalog=" + TBoxSqlCatalog.Text);
            settingsData.AppendLine("mssqluser=" + TBoxSqlUser.Text);
            settingsData.AppendLine("mssqlpass=" + TBoxSqlPass.Text);
            settingsData.AppendLine("mssqlintegrated=" + CheckBoxIS.Checked);

            return settingsData.ToString();
        }

        private void SaveEncryptedSettings()
        {
            string collectedData = CollectSettingsData();
            string encryptedSettingsData = EncryptSettingsData(collectedData);
            FileHelper.SaveEncryptedSettings(encryptedSettingsData); // Предполагается, что у вас есть этот метод
        }

        private string EncryptSettingsData(string data)
        {
            return EncryptionHelper.Encrypt(data);
        }

        private void SaveFormBorderColor(string RGB, byte Value)
        {
            Color borderColor = Properties.Settings.Default.FormBorderColor;
            switch (RGB)
            {
                case "R":
                    borderColor = Color.FromArgb(Value, borderColor.G, borderColor.B);
                    break;
                case "G":
                    borderColor = Color.FromArgb(borderColor.R, Value, borderColor.B);
                    break;
                case "B":
                    borderColor = Color.FromArgb(borderColor.R, borderColor.G, Value);
                    break;
                default:
                    break;
            }

            Properties.Settings.Default.FormBorderColor = borderColor;
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
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
            Properties.Settings.Default.YaMapHttp = TBoxYaMaps.Text;
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }

        private void TBoxCustomRiba_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CustomRiba = TBoxCustomRiba.Text;
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }

        private void TBoxSqlDb_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqldb = TBoxSqlDb.Text;
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }

        private void TBoxSqlCatalog_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqlcatalog = TBoxSqlCatalog.Text;
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }

        private void TBoxSqlUser_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqluser = TBoxSqlUser.Text;
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }

        private void TBoxSqlPass_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqlpass = TBoxSqlPass.Text;
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }

        private void CheckBoxIS_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqlintegrated = CheckBoxIS.Checked;
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }
    }
}
