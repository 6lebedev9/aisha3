using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace aisha3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            WindowLocSaveLoad("Load");
            WindowBorderColorLoad();
            CheckConnectAndVersion();
        }
        public static Dictionary<int, Device> Devices = new Dictionary<int, Device>();
        public static Device DeviceChosen;
        public void CheckConnectAndVersion()
        {
            int verInDB = Mssql.GetLastVersion();
            int verCurrent = Int32.Parse(System.Windows.Forms.Application.CompanyName);
            if (verInDB > 0)
            {
                if (verCurrent < verInDB)
                {
                    DBState.BackgroundImage = global::aisha3.Properties.Resources.database3;
                }
                else
                {
                    DBState.BackgroundImage = global::aisha3.Properties.Resources.database1;
                }
            }
            else
            {
                DBState.BackgroundImage = global::aisha3.Properties.Resources.database2;
            }
        }
        public void CreateBtnSearchHelper(int i)
        {
            if (Devices.ContainsKey(i))
            {
                Button BtnSearchHelper = new Button();
                this.Controls.Add(BtnSearchHelper);
                BtnSearchHelper.BackColor = Color.Black;
                BtnSearchHelper.ForeColor = Color.WhiteSmoke;
                BtnSearchHelper.Text = Devices[i].KvfNumber + " - " + Devices[i].KvfModelCommon;
                BtnSearchHelper.Location = new Point(540, i * 32 + 66);
                BtnSearchHelper.Size = new Size(250, 30);
                BtnSearchHelper.BringToFront();
                BtnSearchHelper.Tag = "dynamic";
            }
        }
        public void ClearBtnSearchHelper()
        {
            var toDelete = Controls.OfType<Button>()
              .Where(c => (c.Tag ?? "").ToString() == "dynamic")
              .ToList();
            foreach (var ctrl in toDelete)
            {
                Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            toDelete.Clear();
        }
        public void SetMainPanel()
        {
            if (Devices.ContainsKey(0))
            {
                DeviceChosen = Devices[0];
                BtnClipGK.Text = DeviceChosen.GKCommon;
                BtnClipDeviceType.Text = DeviceChosen.DeviceType;
                BtnClipKvfModel.Text = DeviceChosen.KvfModel;
                BtnClipKvfNumber.Text = DeviceChosen.KvfNumber;
                if(LblOfAddress.Text.ToString() == "Адрес РГИС:") { BtnClipAddress.Text = DeviceChosen.AddressRGIS; }
                else { BtnClipAddress.Text = DeviceChosen.AddressDoc; }
                BtnClipPoput.Text = DeviceChosen.Poput;
                BtnClipVstrech.Text = DeviceChosen.Vstrech;
                BtnClipNCode.Text = DeviceChosen.NCode;
                BtnClipGps.Text = DeviceChosen.Gps;
                BtnClipDeviceIP.Text = DeviceChosen.DeviceIP;
                BtnToWebDeviceIPTech.Text = DeviceChosen.DeviceIPTech;
                BtnClipIrzIp.Text = DeviceChosen.IrzIp;
                BtnToWebShinobiIp.Text = DeviceChosen.ShinobiIp;
                BtnClipCamQuant.Text = DeviceChosen.CamQuant;
                BtnClipPodrOrg1Common.Text = DeviceChosen.PodrOrg1Common;
                BtnClipPodrOrg2Common.Text = DeviceChosen.PodrOrg2Common;
            }
        }

        public static string todayDateTime = DateTime.Now.ToString("HH:mm dd.MM.yy");
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
            MapPanelOuter.BackColor= borderColor;
        }
        //Settings save/load

        //Control panel buttons
        private void DBState_Click(object sender, EventArgs e)
        {
            CheckConnectAndVersion();
        }
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
            bool SettingsOpened = System.Windows.Forms.Application.OpenForms.Count > 1;
            if (!SettingsOpened)
            {
                Form Settings = new Settings();
                Settings.Show();
            }
        }
        //Control panel buttons
        //Main panel btns
        private void BtnClipGK_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipGK.Text);
        }
        private void BtnClipDeviceType_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipDeviceType.Text);
        }
        private void BtnClipKvfModel_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipKvfModel.Text);
        }
        private void BtnClipKvfNumber_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipKvfNumber.Text);
        }
        private void DEBUG_Btn_Click(object sender, EventArgs e) //DEBUG BTN
        {
            //try
            //{
            //    Console.WriteLine(Devices[1].KvfNumber);
            //    Console.WriteLine(Devices[2].KvfNumber);
            //    Console.WriteLine(Devices[3].KvfNumber);
            //    Console.WriteLine(Devices[4].KvfNumber);
            //    Console.WriteLine(Devices[5].KvfNumber);
            //    Console.WriteLine("COUNT" + Devices.Count.ToString());
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}
            var toDelete = Controls.OfType<Button>()
              .Where(c => (c.Tag ?? "").ToString() == "dynamic")
              .ToList();
            foreach (var ctrl in toDelete)
            {
                Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            toDelete.Clear();

        }
        private void BtnClipAddress_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipAddress.Text.ToString().Replace("\n", ""));
        }
        private void BtnClipAddress_TextChanged(object sender, EventArgs e)
        {
            BtnClipAddress.Text = Regex.Replace(BtnClipAddress.Text.ToString(), "(?<=\\G.{39})(?=.)", "\n");
        }
        private void BtnClipPoput_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipPoput.Text.ToString().Replace("\n", ""));
        }
        private void BtnClipPoput_TextChanged(object sender, EventArgs e)
        {
            BtnClipPoput.Text = Regex.Replace(BtnClipPoput.Text.ToString(), "(?<=\\G.{39})(?=.)", "\n");
        }
        private void BtnClipVstrech_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipVstrech.Text.ToString().Replace("\n", ""));
        }
        private void BtnClipVstrech_TextChanged(object sender, EventArgs e)
        {
            BtnClipVstrech.Text = Regex.Replace(BtnClipVstrech.Text.ToString(), "(?<=\\G.{39})(?=.)", "\n");
        }

        private void BtnClipNCode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipNCode.Text.ToString().Replace("\n", ""));
        }

        private void BtnClipNCode_TextChanged(object sender, EventArgs e)
        {
            BtnClipNCode.Text = Regex.Replace(BtnClipNCode.Text.ToString(), "(?<=\\G.{39})(?=.)", "\n");
        }

        private void BtnClipGpsN_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipGpsN.Text.ToString());
        }

        private void BtnClipGpsE_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipGpsE.Text.ToString());
        }

        private void BtnClipGps_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipGpsN.Text.ToString() + " " + BtnClipGpsE.Text.ToString());
        }

        private void BtnClipGps_TextChanged(object sender, EventArgs e)
        {
            string[] coords = BtnClipGps.Text.ToString().Split(" ".ToCharArray());
            string coordN = coords[0];
            string coordE = coords[1];
            BtnClipGpsN.Text = coordN;
            BtnClipGpsE.Text = coordE;
        }

        private void BtnToWebYaMaps_Click(object sender, EventArgs e)
        {
            string curYaMapHttp = Properties.Settings.Default.YaMapHttp;
            try
            {
                string curYaMapHttpWithPoint = curYaMapHttp.Replace("NNNNNN", BtnClipGpsN.Text.ToString())
                                                       .Replace("EEEEEE", BtnClipGpsE.Text.ToString());
                System.Diagnostics.Process.Start(curYaMapHttpWithPoint);
            }
            catch (Exception)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void BtnClipDeviceIP_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipDeviceIP.Text.ToString());
        }

        private void BtnToWebDeviceIPTech_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://" + BtnToWebDeviceIPTech.Text.ToString());
            }
            catch(Exception) { }
        }

        private void BtnClipDeviceIPTech_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnToWebDeviceIPTech.Text.ToString());
        }

        private void BtnClipEtherProvider_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipEtherProvider.Text.ToString());
        }

        private void BtnClipIrzHttp_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipIrzIp.Text.ToString());
        }

        private void BtnClipIrzIp_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipIrzIp.Text.ToString());
        }

        private void BtnToWebShinobiIp_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://" + BtnToWebShinobiIp.Text.ToString());
            }
            catch (Exception) { }
        }

        private void BtnClipShinobiIp_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnToWebShinobiIp.Text.ToString());
        }

        private void BtnClipRtsp_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipRtsp.Text.ToString());
        }

        private void BtnClipCamQuant_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipCamQuant.Text.ToString());
        }

        private void BtnClipPodrOrg1Common_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipPodrOrg1Common.Text.ToString());
        }

        private void BtnClipPodrOrg2Common_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipPodrOrg2Common.Text.ToString());
        }

        private void BtnClipR1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime 
            + " - результат диагностики подрядной организации (МКЛ) - отсутствует ЭП в ШУ;/отсутствует КС;");
        }

        private void BtnClipR2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
            + " - результат диагностики подрядной организации (Нетлайн) - отсутствует ЭП в ШУ;/отсутствует КС;");
        }

        private void BtnClipR3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
           + " - результат диагностики АВР бригады РЦР - отсутствует ЭП в ШУ;/отсутствует КС;");
        }

        private void BtnClipR4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
           + " - направлена заявка подрядной организации (ПТС/ТАКТ) на восстановление ЭП/КС;");
        }

        private void BtnClipR5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
           + " - отсутствие фиксаций, сбита ориентация прибора, исправлена, комплекс работает в штатном режиме.");
        }

        private void BtnClipR6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
           + " - отсутствие фиксаций, подрядная организация (Нетлайн/МКЛ)/АВР бригада РЦР выполнила очистку оптики КВФ.");
        }

        private void BtnClipR7_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
           + " - направлена заявка в ТП РТК на отсутствие ЭП;/на отсутствие канала связи;/на нестабильность канала связи;");
        }

        private void BtnClipR8_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
           + " - обновление статуса заявки от ТП РТК - принято в работу;");
        }

        private void BtnClipR9_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
           + " - направлена заявка в ТП ЭРТХ на отсутствие канала связи;/на нестабильность канала связи;");
        }

        private void BtnClipR10_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
           + " - обновление статуса заявки от ТП ЭРТХ - принято в работу за № ____ ;");
        }

        private void BtnClipR11_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
           + " - отсутствие фиксаций и видеопотока, работоспособность комплекса восстановлена.");
        }

        private void BtnClipRCustom_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(todayDateTime
           + Properties.Settings.Default.CustomRiba.ToString());
        }

        private void BtnStatusIP_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo
            {
                FileName = @"C:\windows\system32\cmd.exe",
                Arguments = "/k ping " + BtnClipDeviceIP.Text.ToString() + " -n 6 "
            };
            System.Diagnostics.Process.Start(proc);
        }

        private void BtnStatusIPTech_Click(object sender, EventArgs e)
        {
            if(BtnToWebDeviceIPTech.Text.Length > 0)
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = @"C:\windows\system32\cmd.exe",
                        Arguments = "/k ping " + BtnToWebDeviceIPTech.Text.ToString() + " -n 6 "
                    };
                    System.Diagnostics.Process.Start(proc);
                }
                catch(Exception) { }
            }
        }

        private void BtnStatusGW_Click(object sender, EventArgs e)
        {
            if (DeviceChosen != null)
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = @"C:\windows\system32\cmd.exe",
                        Arguments = "/k ping " + DeviceChosen.DeviceGWIP.ToString() + " -n 6 "
                    };
                    System.Diagnostics.Process.Start(proc);
                }
                catch (Exception) { }
            }
        }

        private void BtnToHttpKvf_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://" + BtnClipDeviceIP.Text.ToString());
            }
            catch (Exception) { }
        }

        private void BtnToHttpIRZ_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://" + BtnClipIrzIp.Text.ToString());
            }
            catch (Exception) { }
        }
        public static int PrevTBoxLength = 0;
        private void TBox_TextChanged(object sender, EventArgs e)
        {
            Devices?.Clear();
            if (TBox.Text.Length >= 3)
            {
                Devices = Mssql.DevicesbyString(TBox.Text.ToString());
                SetMainPanel();
                if(Devices.Count > 1)
                {
                    switch(Devices.Count)
                    {
                        case 1:
                            ClearBtnSearchHelper();
                            break;
                        case 2:
                            ClearBtnSearchHelper();
                            CreateBtnSearchHelper(0);
                            CreateBtnSearchHelper(1);
                            break;
                        case 3:
                            ClearBtnSearchHelper();
                            CreateBtnSearchHelper(0);
                            CreateBtnSearchHelper(1);
                            CreateBtnSearchHelper(2);
                            break;
                        case 4:
                            ClearBtnSearchHelper();
                            CreateBtnSearchHelper(0);
                            CreateBtnSearchHelper(1);
                            CreateBtnSearchHelper(2);
                            CreateBtnSearchHelper(3);
                            break;
                        default:
                            ClearBtnSearchHelper();
                            CreateBtnSearchHelper(0);
                            CreateBtnSearchHelper(1);
                            CreateBtnSearchHelper(2);
                            CreateBtnSearchHelper(3);
                            CreateBtnSearchHelper(4);
                            break;
                    }
                }
                else
                {
                    ClearBtnSearchHelper();
                }
            }
            else { ClearBtnSearchHelper(); }
        }

        private void BtnRevertAddress_Click(object sender, EventArgs e)
        {
            if(LblOfAddress.Text == "Адрес РГИС:")
            {
                LblOfAddress.Text = "Адрес по ГК:";
                if (Devices.Count > 0)
                {
                    BtnClipAddress.Text = DeviceChosen.AddressDoc;
                }
            }
            else
            {
                LblOfAddress.Text = "Адрес РГИС:";
                if (Devices.Count > 0)
                {
                    BtnClipAddress.Text = DeviceChosen.AddressRGIS;
                }
            }
        }

        //Main panel btns
    }
}
