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

        //AES

        private string CollectSettingsData()
        {
            // Создайте строку, в которой будете хранить все данные настроек
            StringBuilder settingsData = new StringBuilder();

            // Добавьте данные каждого элемента управления
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

            // Возвращаем собранные данные в виде строки
            return settingsData.ToString();
        }


        private void SaveEncryptedSettings()
        {
            string collectedData = CollectSettingsData();
            string encryptedSettingsData = EncryptSettingsData(collectedData);
            FileHelper.SaveEncryptedSettings(encryptedSettingsData);
        }

        private string EncryptSettingsData(string data)
        {
            return EncryptionHelper.Encrypt(data);
        }

        //AES
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
                    SaveEncryptedSettings();
                    break;
                case "G":
                    byte GchangedColorR = borderColor.R;
                    byte GchangedColorG = Value;
                    byte GchangedColorB = borderColor.B;
                    System.Drawing.Color GchangedColor = Color.FromArgb(GchangedColorR, GchangedColorG, GchangedColorB);
                    Properties.Settings.Default.FormBorderColor = GchangedColor;
                    Properties.Settings.Default.Save();
                    SaveEncryptedSettings();
                    break;
                case "B":
                    byte BchangedColorR = borderColor.R;
                    byte BchangedColorG = borderColor.G;
                    byte BchangedColorB = Value;
                    System.Drawing.Color BchangedColor = Color.FromArgb(BchangedColorR, BchangedColorG, BchangedColorB);
                    Properties.Settings.Default.FormBorderColor = BchangedColor;
                    Properties.Settings.Default.Save();
                    SaveEncryptedSettings();
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
            SaveEncryptedSettings();
        }

        private void TBoxCustomRiba_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CustomRiba = TBoxCustomRiba.Text.ToString();
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }

        private void TBoxSqlDb_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqldb = TBoxSqlDb.Text.ToString();
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }

        private void TBoxSqlCatalog_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqlcatalog = TBoxSqlCatalog.Text.ToString();
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }

        private void TBoxSqlUser_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqluser = TBoxSqlUser.Text.ToString();
            Properties.Settings.Default.Save();
            SaveEncryptedSettings();
        }

        private void TBoxSqlPass_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mssqlpass = TBoxSqlPass.Text.ToString();
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
