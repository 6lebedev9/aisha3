﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using DragEventArgs = System.Windows.Forms.DragEventArgs;
using DataFormats = System.Windows.Forms.DataFormats;
using DragDropEffects = System.Windows.Forms.DragDropEffects;
using Panel = System.Windows.Forms.Panel;
using Button = System.Windows.Forms.Button;
using Size = System.Drawing.Size;
using Clipboard = System.Windows.Clipboard;
using Point = System.Drawing.Point;
using MessageBox = System.Windows.Forms.MessageBox;
using CheckBox = System.Windows.Forms.CheckBox;
using Label = System.Windows.Forms.Label;
using Control = System.Windows.Forms.Control;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Net;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using Newtonsoft.Json.Linq;
using Microsoft.Identity.Client;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;


namespace aisha3
{
    public partial class MainForm : Form
    {
        private DataGridView DGVxls;
        public MainForm()
        {
            InitializeComponent();
            WindowLocSaveLoad("Load");
            LoadEncryptedSettings();
            WindowBorderColorLoad();
            CheckConnectAndVersion();
        }
        //AES

        private void LoadEncryptedSettings()
        {
            string encryptedSettingsData = FileHelper.LoadEncryptedSettings();
            if (!string.IsNullOrEmpty(encryptedSettingsData))
            {
                DecryptSettingsData(encryptedSettingsData);
            }
        }
        private string DecryptSettingsData(string encryptedData)
        {
            string decryptedData = EncryptionHelper.Decrypt(encryptedData);

            string[] settings = decryptedData.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string setting in settings)
            {
                string[] parts = setting.Split('=');
                string settingName = parts[0];
                string settingValue = parts[1];

                if (settingName == "YaMapHttp")
                {
                    Properties.Settings.Default.YaMapHttp = settingValue;
                    Properties.Settings.Default.Save();
                }
                else if (settingName == "CustomRiba")
                {
                    Properties.Settings.Default.CustomRiba = settingValue;
                    Properties.Settings.Default.Save();
                }
                else if (settingName == "mssqldb")
                {
                    Properties.Settings.Default.mssqldb = settingValue;
                    Properties.Settings.Default.Save();
                }
                else if (settingName == "mssqluser")
                {
                    Properties.Settings.Default.mssqluser = settingValue;
                    Properties.Settings.Default.Save();
                }
                else if (settingName == "mssqlpass")
                {
                    Properties.Settings.Default.mssqlpass = settingValue;
                    Properties.Settings.Default.Save();
                }
                else if (settingName == "mssqlcatalog")
                {
                    Properties.Settings.Default.mssqlcatalog = settingValue;
                    Properties.Settings.Default.Save();
                }
                else
                {
                }
            }

            return decryptedData;
        }


        //AES


        //XLS EDIT

        private void DGV_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        private void DGV_DragDrop(object sender, DragEventArgs e)
        {
            Form dataGridViewForm = new Form
            {
                Text = "Статистика видео. Добавление информации по первому столбцу.",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 800,
                Height = 600,
                Icon = new Icon(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icon_x256.ico"))
            };

            DGVxls = new DataGridView
            {
                Name = "DGVxls",
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,  // Автоматическое изменение размера столбцов
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,  // Автоматическая настройка высоты заголовков столбцов
                AllowUserToOrderColumns = true
            };

            dataGridViewForm.Controls.Add(DGVxls);
            dataGridViewForm.Show();

            Panel panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40
            };

            Button btnToAddGKCommon = new Button
            {
                Text = "ГК",
                Dock = DockStyle.Left
            };
            Button btnToAddKvfModelCommon = new Button
            {
                Text = "Модель",
                Dock = DockStyle.Left
            };
            Button btnToAddCamFixType = new Button
            {
                Text = "тип к.",
                Dock = DockStyle.Left
            };
            Button btnToAddAddressRGIS = new Button
            {
                Text = "Адрес РГИС",
                Dock = DockStyle.Left
            };
            Button btnToAddPodrOrg1Common = new Button
            {
                Text = "Подр.",
                Dock = DockStyle.Left
            };
            Button btnToAddEtherProvider = new Button
            {
                Text = "КС",
                Dock = DockStyle.Left
            };
            Button btnToAddDeviceIP = new Button
            {
                Text = "IP",
                Dock = DockStyle.Left
            };
            Button btnToClipBoard = new Button
            {
                Text = "Копировать в буфер для вставки в MS.Excel",
                Dock = DockStyle.Right
            };

            dataGridViewForm.Controls.Add(panel);
            panel.Controls.Add(btnToAddGKCommon);
            panel.Controls.Add(btnToAddKvfModelCommon);
            panel.Controls.Add(btnToAddCamFixType);
            panel.Controls.Add(btnToAddAddressRGIS);
            panel.Controls.Add(btnToAddPodrOrg1Common);
            panel.Controls.Add(btnToAddEtherProvider);
            panel.Controls.Add(btnToAddDeviceIP);
            panel.Controls.Add(btnToClipBoard);
            btnToAddGKCommon.Click += InsertColumnWithGKCommon;
            btnToAddKvfModelCommon.Click += InsertColumnWithKvfModelCommon;
            btnToAddCamFixType.Click += InsertColumnWithCamFixType;
            btnToAddAddressRGIS.Click += InsertColumnWithAddressRGIS;
            btnToAddPodrOrg1Common.Click += InsertColumnWithPodrOrg1Common;
            btnToAddEtherProvider.Click += InsertColumnWithEtherProvider;
            btnToAddDeviceIP.Click += InsertColumnWithDeviceIP;
            btnToClipBoard.Click += CopyButton_Click;
            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string file = files[0];

                // Убедитесь, что файл существует
                if (!File.Exists(file))
                {
                    throw new FileNotFoundException("Файл не найден", file);
                }

                IWorkbook workbook;
                // Определяем формат файла (xls или xlsx) и загружаем его
                using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    if (file.EndsWith(".xls"))
                    {
                        workbook = new HSSFWorkbook(fileStream);
                    }
                    else if (file.EndsWith(".xlsx"))
                    {
                        workbook = new XSSFWorkbook(fileStream);
                    }
                    else
                    {
                        throw new NotSupportedException("Unsupported file extension");
                    }
                }

                // Получаем первый лист из книги
                ISheet sheet = workbook.GetSheetAt(0) ?? throw new Exception("Нет доступных листов в файле Excel.");

                // Очищаем DataGridView перед заполнением
                DGVxls.Rows.Clear();
                DGVxls.Columns.Clear();

                // Добавляем колонки в DataGridView
                IRow headerRow = sheet.GetRow(sheet.FirstRowNum);
                foreach (ICell headerCell in headerRow.Cells)
                {
                    DGVxls.Columns.Add(headerCell.ToString(), headerCell.ToString());
                }

                // Регулярное выражение для извлечения значения внутри функции TIMEVALUE
                Regex timeValueRegex = new Regex(@"TIMEVALUE\(""([^""]+)""\)");

                // Перебираем все строки и столбцы на листе и добавляем данные в DataGridView
                for (int row = sheet.FirstRowNum + 1; row <= sheet.LastRowNum; row++)
                {
                    IRow currentRow = sheet.GetRow(row);
                    if (currentRow == null) continue;

                    var fields = new List<string>();
                    foreach (ICell cell in currentRow.Cells)
                    {
                        string cellValue = cell.ToString();
                        Match match = timeValueRegex.Match(cellValue);
                        if (match.Success)
                        {
                            fields.Add(match.Groups[1].Value); // извлекаем значение внутри TIMEVALUE
                        }
                        else
                        {
                            fields.Add(cellValue);
                        }
                    }

                    DGVxls.Rows.Add(fields.ToArray());
                    //Console.WriteLine(string.Join("\t", fields)); //FOR DBG
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void CopyButton_Click(object sender, EventArgs e)
        {
            DataGridViewExtensions.CopyDataGridViewToClipboard(DGVxls);
        }
        private void InsertColumnWithGKCommon(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы один столбец в DataGridView
            if (DGVxls.Columns.Count == 0)
            {
                MessageBox.Show("DataGridView пуст.");
                return;
            }

            // Вставляем новый столбец между первым и вторым столбцом
            DGVxls.Columns.Insert(1, new DataGridViewTextBoxColumn() { Name = "GKCommon", HeaderText = "ГК" });

            // Перебираем все строки DataGridView
            foreach (DataGridViewRow row in DGVxls.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string firstColumnValue = row.Cells[0].Value.ToString();
                    
                        row.Cells[1].Value = GetDevicePropertyByXlsName(firstColumnValue.Trim(), device => device.GKCommon);
                    
                }
                else
                {
                    row.Cells[1].Value = "нет данных";
                }
            }
        }
        private void InsertColumnWithKvfModelCommon(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы один столбец в DataGridView
            if (DGVxls.Columns.Count == 0)
            {
                MessageBox.Show("DataGridView пуст.");
                return;
            }

            // Вставляем новый столбец между первым и вторым столбцом
            DGVxls.Columns.Insert(1, new DataGridViewTextBoxColumn() { Name = "KvfModelCommon", HeaderText = "Модель" });

            // Перебираем все строки DataGridView
            foreach (DataGridViewRow row in DGVxls.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string firstColumnValue = row.Cells[0].Value.ToString();

                    row.Cells[1].Value = GetDevicePropertyByXlsName(firstColumnValue.Trim(), device => device.KvfModelCommon);

                }
                else
                {
                    row.Cells[1].Value = "нет данных";
                }
            }
        }
        private void InsertColumnWithCamFixType(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы один столбец в DataGridView
            if (DGVxls.Columns.Count == 0)
            {
                MessageBox.Show("DataGridView пуст.");
                return;
            }

            // Вставляем новый столбец между первым и вторым столбцом
            DGVxls.Columns.Insert(1, new DataGridViewTextBoxColumn() { Name = "CamFixType", HeaderText = "тип к." });

            // Перебираем все строки DataGridView
            foreach (DataGridViewRow row in DGVxls.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string firstColumnValue = row.Cells[0].Value.ToString();

                    row.Cells[1].Value = GetDevicePropertyByXlsName(firstColumnValue.Trim(), device => device.CamFixType);

                }
                else
                {
                    row.Cells[1].Value = "нет данных";
                }
            }
        }
        private void InsertColumnWithAddressRGIS(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы один столбец в DataGridView
            if (DGVxls.Columns.Count == 0)
            {
                MessageBox.Show("DataGridView пуст.");
                return;
            }

            // Вставляем новый столбец между первым и вторым столбцом
            DGVxls.Columns.Insert(1, new DataGridViewTextBoxColumn() { Name = "AddressRGIS", HeaderText = "Адрес РГИС" });

            // Перебираем все строки DataGridView
            foreach (DataGridViewRow row in DGVxls.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string firstColumnValue = row.Cells[0].Value.ToString();

                    row.Cells[1].Value = GetDevicePropertyByXlsName(firstColumnValue.Trim(), device => device.AddressRGIS);

                }
                else
                {
                    row.Cells[1].Value = "нет данных";
                }
            }
        }
        private void InsertColumnWithPodrOrg1Common(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы один столбец в DataGridView
            if (DGVxls.Columns.Count == 0)
            {
                MessageBox.Show("DataGridView пуст.");
                return;
            }

            // Вставляем новый столбец между первым и вторым столбцом
            DGVxls.Columns.Insert(1, new DataGridViewTextBoxColumn() { Name = "PodrOrg1Common", HeaderText = "Подр." });

            // Перебираем все строки DataGridView
            foreach (DataGridViewRow row in DGVxls.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string firstColumnValue = row.Cells[0].Value.ToString();

                    row.Cells[1].Value = GetDevicePropertyByXlsName(firstColumnValue.Trim(), device => device.PodrOrg1Common);

                }
                else
                {
                    row.Cells[1].Value = "нет данных";
                }
            }
        }
        private void InsertColumnWithEtherProvider(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы один столбец в DataGridView
            if (DGVxls.Columns.Count == 0)
            {
                MessageBox.Show("DataGridView пуст.");
                return;
            }

            // Вставляем новый столбец между первым и вторым столбцом
            DGVxls.Columns.Insert(1, new DataGridViewTextBoxColumn() { Name = "EtherProvider", HeaderText = "КС" });

            // Перебираем все строки DataGridView
            foreach (DataGridViewRow row in DGVxls.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string firstColumnValue = row.Cells[0].Value.ToString();

                    row.Cells[1].Value = GetDevicePropertyByXlsName(firstColumnValue.Trim(), device => device.EtherProvider);

                }
                else
                {
                    row.Cells[1].Value = "нет данных";
                }
            }
        }
        private void InsertColumnWithDeviceIP(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы один столбец в DataGridView
            if (DGVxls.Columns.Count == 0)
            {
                MessageBox.Show("DataGridView пуст.");
                return;
            }

            // Вставляем новый столбец между первым и вторым столбцом
            DGVxls.Columns.Insert(1, new DataGridViewTextBoxColumn() { Name = "DeviceIP", HeaderText = "IP" });

            // Перебираем все строки DataGridView
            foreach (DataGridViewRow row in DGVxls.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string firstColumnValue = row.Cells[0].Value.ToString();

                    row.Cells[1].Value = GetDevicePropertyByXlsName(firstColumnValue.Trim(), device => device.DeviceIP);

                }
                else
                {
                    row.Cells[1].Value = "нет данных";
                }
            }
        }
        // XLS EDIT

        public static bool Connected = false;
        public static Dictionary<int, Device> Devices = new Dictionary<int, Device>();
        public static Dictionary<int, Device> Cams = new Dictionary<int, Device>();
        public static Dictionary<int, Device> Extras = new Dictionary<int, Device>();
        public static Dictionary<int, string> GKCommonUniqs = new Dictionary<int, string>();
        public static Dictionary<int, string> KvfModelUniqs = new Dictionary<int, string>();
        public static Dictionary<int, string> DeviceTypeUniqs = new Dictionary<int, string>();
        public static Dictionary<int, string> CamFixTypeUniqs = new Dictionary<int, string>();
        public static Dictionary<int, string> EtherProviderUniqs = new Dictionary<int, string>();
        public static Dictionary<int, string> PodrOrg1CommonUniqs = new Dictionary<int, string>();
        public static Dictionary<int, string> PodrOrg2CommonUniqs = new Dictionary<int, string>();
        public static Dictionary<int, string> NCodeUniqs = new Dictionary<int, string>();
        public static Dictionary<int, string> SpeedUniqs = new Dictionary<int, string>();
        public static Dictionary<int, string> DistUniqs = new Dictionary<int, string>();
        public static Dictionary<int, string> OrgOwnerUniqs = new Dictionary<int, string>();
        public static Dictionary<int, SortVar> SortVars = new Dictionary<int, SortVar>();
        public string[] UniqVarNames = new string[11]
            {
                "GKCommon", "KvfModel", "DeviceType", "CamFixType", "EtherProvider", "PodrOrg1Common",
                "PodrOrg2Common", "NCode", "Speed", "Dist", "OrgOwner"
            };
        public static Dictionary<int, Device> DevicesSort = new Dictionary<int, Device>();
        public static Dictionary<int, Device> DevicesALL = new Dictionary<int, Device>();
        public static Device DeviceChosen;
        public static int ChosenIssueTheme = 0;
        public static bool MapOpen = false;
        public static bool CamsOpen = false;
        public static bool SortOpen = false;
        public static bool IssuesOpen = false;

        public string GetDevicePropertyByXlsName(string xlsName, Func<Device, string> propertySelector)
        {
            foreach (var device in DevicesALL.Values)
            {
                if (device.XlsName == xlsName)
                {
                    return propertySelector(device);
                }
            }
            return null;
        }

        public async void CheckConnectAndVersion()
        {
            try
            {
                int verInDB = Mssql.GetLastVersion();
                GKCommonUniqs = Mssql.Uniqs("GKCommon");
                KvfModelUniqs = Mssql.Uniqs("KvfModel");
                DeviceTypeUniqs = Mssql.Uniqs("DeviceType");
                CamFixTypeUniqs = Mssql.Uniqs("CamFixType");
                EtherProviderUniqs = Mssql.Uniqs("EtherProvider");
                PodrOrg1CommonUniqs = Mssql.Uniqs("PodrOrg1Common");
                PodrOrg2CommonUniqs = Mssql.Uniqs("PodrOrg2Common");
                NCodeUniqs = Mssql.Uniqs("NCode");
                SpeedUniqs = Mssql.Uniqs("Speed");
                DistUniqs = Mssql.Uniqs("Dist");
                OrgOwnerUniqs = Mssql.Uniqs("OrgOwner");
                SetSortVars();
                CreateSortPrefPanel();
                DevicesALL = Mssql.DevicesAll();
                int verCurrent = Int32.Parse(System.Windows.Forms.Application.CompanyName);
                if (verInDB > 0)
                {
                    TBox.Enabled = true;
                    CommentTBox.Enabled = true;
                    if (verCurrent < verInDB)
                    {
                        DBState.BackgroundImage = global::aisha3.Properties.Resources.database3;
                        bool downloadSuccess = await Mssql.DownloadNewVersionAsync();
                        if (downloadSuccess)
                        {
                            //string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            //string updaterAppPath = Path.Combine(appDirectory, "updates", "Updater.exe");
                            //string arguments = "replace";
                            //Process.Start(updaterAppPath, arguments);

                            string appDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                            string updaterAppPath = Path.Combine(appDirectory, "updates", "Updater.exe");

                            string arguments = "-replace";
                            ProcessStartInfo startInfo = new ProcessStartInfo
                            {
                                FileName = updaterAppPath,
                                Arguments = arguments,
                                UseShellExecute = true
                            };
                            Process.Start(startInfo);

                            Application.Exit();
                        }
                        else
                        {
                            Console.WriteLine("Error 22 - Failed to download the new version.");
                        }

                    }
                    else
                    {
                        DBState.BackgroundImage = global::aisha3.Properties.Resources.database1;
                    }
                }
                else
                {
                    TBox.Text = "НЕТ СОЕДИНЕНИЯ С БАЗОЙ";
                    DBState.BackgroundImage = global::aisha3.Properties.Resources.database2;
                    TBox.Enabled = false;
                    CommentTBox.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public void SetSortVars()
        {
            if(Mssql.Connected)
            {
                Dictionary<int, string>[] DicsUniq = new Dictionary<int, string>[11]
            {
                GKCommonUniqs, KvfModelUniqs, DeviceTypeUniqs, CamFixTypeUniqs, EtherProviderUniqs, PodrOrg1CommonUniqs,
                PodrOrg2CommonUniqs, NCodeUniqs, SpeedUniqs, DistUniqs, OrgOwnerUniqs
            };

                Dictionary<int, SortVar> SortVarsDic = new Dictionary<int, SortVar>();
                int i = 0;
                int j = 0;
                foreach (var dic in DicsUniq)
                {
                    foreach (var item in dic)
                    {
                        SortVar sortVar = new SortVar
                        {
                            VarName = item.Value,
                            Chosen = true,
                            Group = j
                        };
                        SortVarsDic.Add(i, sortVar);
                        i++;
                    }
                    j++;
                }
                SortVars = SortVarsDic;
            }
        }

        private void CreateSortPrefPanel()
        {
            if (SortVars != null)
            {
                var groupedVars = SortVars.Values.GroupBy(sv => sv.Group);

                foreach (var group in groupedVars)
                {
                    Panel groupPanel = new Panel
                    {
                        Size = new Size(110, group.Count() * 20 + 20),
                        BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                        BackColor = Color.Black,
                        Margin = new Padding(0)
                    };

                    CheckBox groupCheckBox = new CheckBox
                    {
                        Location = new Point(0, 0),
                        Size = new Size(15, 15),
                        Checked = true,
                        BackColor = Color.Black,
                        Margin = new Padding(0)
                    };
                    Label groupLabel = new Label
                    {
                        Location = new Point(20, 0),
                        Size = new Size(95, 15),
                        Text = $"{UniqVarNames[group.Key]}",
                        BackColor = Color.Black,
                        ForeColor = Color.White,
                        Font = new Font("Arial", 8.0f),
                        Margin = new Padding(0)
                    };

                    groupPanel.Controls.Add(groupCheckBox);
                    groupPanel.Controls.Add(groupLabel);

                    int itemYPos = 20;

                    foreach (var item in group)
                    {
                        CheckBox itemCheckBox = new CheckBox
                        {
                            Location = new Point(0, itemYPos),
                            Size = new Size(15, 15),
                            Checked = item.Chosen,
                            Margin = new Padding(0)
                        };
                        itemCheckBox.CheckedChanged += (sender, e) =>
                        {
                            item.Chosen = itemCheckBox.Checked;
                        };

                        Label itemLabel = new Label
                        {
                            Location = new Point(15, itemYPos),
                            Size = new Size(95, 15),
                            Text = item.VarName,
                            BackColor = Color.Black,
                            ForeColor = Color.White,
                            Font = new Font("Arial", 7.0f),
                            Margin = new Padding(0)
                        };

                        groupPanel.Controls.Add(itemCheckBox);
                        groupPanel.Controls.Add(itemLabel);

                        itemYPos += 20;
                    }

                    groupCheckBox.CheckedChanged += (sender, e) =>
                    {
                        foreach (var item in group)
                        {
                            item.Chosen = groupCheckBox.Checked;
                        }

                        foreach (Control control in groupPanel.Controls)
                        {
                            if (control is CheckBox checkBoxx && control != groupCheckBox)
                            {
                                checkBoxx.Checked = groupCheckBox.Checked;
                            }
                        }
                    };

                    SortPrefPanelFlow.Controls.Add(groupPanel);
                }
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
                BtnSearchHelper.Tag = i;
                BtnSearchHelper.Click += new System.EventHandler(SetChosenDevice_Click);
                void SetChosenDevice_Click(object sender, EventArgs e)
                {
                    DeviceChosen = Devices[i];
                    ClearBtnSearchHelper();
                    SetMainPanel(i);
                }
                
            }
        }
        public void ClearBtnSearchHelper()
        {
            var toDelete = Controls.OfType<Button>()
              .Where(c => (c.Tag ?? "").ToString() != "")
              .ToList();
            foreach (var ctrl in toDelete)
            {
                Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            toDelete.Clear();
        }

        public void ClearAllCamBtns()
        {
            var toRemove1 = CamPanelCol1.Controls.OfType<Panel>().ToList();
            var toRemove2 = CamPanelCol2.Controls.OfType<Panel>().ToList();
            var toRemove3 = CamPanelCol3.Controls.OfType<Panel>().ToList();
            foreach(var ctrl in toRemove1)
            {
                Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            toRemove1.Clear();
            foreach (var ctrl in toRemove2)
            {
                Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            toRemove2.Clear();
            foreach (var ctrl in toRemove3)
            {
                Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            toRemove3.Clear();
        }

        public void CreateCamBtn(Panel parent, int i)
        {
            Panel camMiniPanel = new FlowLayoutPanel();
            parent.Controls.Add(camMiniPanel);
            camMiniPanel.BackColor = Color.White;
            camMiniPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            camMiniPanel.AutoSize = false;
            camMiniPanel.Size = new Size(130, 45);
            camMiniPanel.Tag = i;
            camMiniPanel.Margin = new Padding(0);

            Button camNumberBtn = new Button();
            camMiniPanel.Controls.Add(camNumberBtn);
            camNumberBtn.Size = new Size(130, 23);
            camNumberBtn.AutoSize = false;
            camNumberBtn.Text = Cams[i].KvfNumber.ToString();
            camNumberBtn.Font = new Font("Tw Cen MT Condensed", (float)11);
            camNumberBtn.FlatStyle = FlatStyle.Popup;
            camNumberBtn.Margin = new Padding(0);
            camNumberBtn.Tag = i;
            camNumberBtn.BackColor = Color.Black;
            camNumberBtn.ForeColor = Color.White;
            camNumberBtn.Dock = DockStyle.Top;
            camNumberBtn.Click += new System.EventHandler(BtnClipCamNumber_Click);
            void BtnClipCamNumber_Click(object sender, EventArgs e)
            {
                Clipboard.SetText(Cams[i].KvfNumber);
            }

            Button camToKsmBtn = new Button();
            camMiniPanel.Controls.Add(camToKsmBtn);
            camToKsmBtn.Size = new Size(65, 22);
            camToKsmBtn.Location = new Point(0, 23);
            camToKsmBtn.AutoSize = true;
            camToKsmBtn.Text = "КСМ";
            camToKsmBtn.Font = new Font("Tw Cen MT Condensed", (float)10);
            camToKsmBtn.FlatStyle = FlatStyle.Popup;
            camToKsmBtn.Margin = new Padding(0);
            camToKsmBtn.Tag = i;
            camToKsmBtn.BackColor = Color.Black;
            camToKsmBtn.ForeColor = Color.White;
            camToKsmBtn.Dock = DockStyle.Left;
            camToKsmBtn.Click += new System.EventHandler(CamToKsmBtn_Click);
            void CamToKsmBtn_Click(object sender, EventArgs e)
            {
                try
                {
                    System.Diagnostics.Process.Start(Cams[i].KsmHttp.ToString());
                }
                catch (Exception) { }
            }

            Button ClipRtspBtn = new Button();
            camMiniPanel.Controls.Add(ClipRtspBtn);
            ClipRtspBtn.Size = new Size(65, 22);
            ClipRtspBtn.Location = new Point(65, 23);
            ClipRtspBtn.AutoSize = true;
            ClipRtspBtn.Text = "RTSP";
            ClipRtspBtn.Font = new Font("Tw Cen MT Condensed", (float)10);
            ClipRtspBtn.FlatStyle = FlatStyle.Popup;
            ClipRtspBtn.Margin = new Padding(0);
            ClipRtspBtn.Tag = i;
            ClipRtspBtn.BackColor = Color.Black;
            ClipRtspBtn.ForeColor = Color.White;
            ClipRtspBtn.Dock = DockStyle.Right;
            ClipRtspBtn.Click += new System.EventHandler(ClipRtspBtn_Click);
            void ClipRtspBtn_Click(object sender, EventArgs e)
            {
                Clipboard.SetText(Cams[i].Rtsp);
            }
        }

        public void CreateAllCamBtns()
        {
            if(Cams != null)
            {
                foreach(var c in Cams)
                {
                    Device cam = c.Value as Device;
                    int i = c.Key;
                    switch (cam.CamFixType)
                    {
                        case "фикс.к.":
                            CreateCamBtn(CamPanelCol1, i);
                            break;
                        case "обзор.к.":
                            CreateCamBtn(CamPanelCol2, i);
                            break;
                        case "доп.к.":
                            CreateCamBtn(CamPanelCol3, i);
                            break;
                        default: break;
                    }
                }
            }
        }

        public void SetSortOptions()
        {
            if(SortVars != null)
            {

            }
        }
        public void SetMainPanel(int i)
        {
            if(Devices != null)
            {
                if (Devices.ContainsKey(i))
                {
                    DeviceChosen = Devices[i];
                    MapShowKvf(DeviceChosen);
                    //if (DeviceChosen.DeviceType == "перекресток")
                    //{
                    //    Cams.Clear();
                    //    ClearAllCamBtns();
                    //    Cams = Mssql.CamsbyKvfNumber(DeviceChosen.KvfNumber);
                    //    CreateAllCamBtns();
                    //}

                    Cams.Clear();
                    ClearAllCamBtns();
                    Cams = Mssql.CamsbyKvfNumber(DeviceChosen.KvfNumber);
                    CreateAllCamBtns();
                    Extras = Mssql.ExtrasbyKvfNumber(DeviceChosen.KvfNumber);
                    BtnClipGK.Text = DeviceChosen.GKCommon;
                    BtnClipDeviceType.Text = DeviceChosen.DeviceType;
                    BtnClipKvfModel.Text = DeviceChosen.KvfModel;
                    BtnClipKvfNumber.Text = DeviceChosen.KvfNumber;
                    if (LblOfAddress.Text.ToString() == "Адрес РГИС:") { BtnClipAddress.Text = DeviceChosen.AddressRGIS; }
                    else { BtnClipAddress.Text = DeviceChosen.AddressDoc; }
                    BtnClipPoput.Text = DeviceChosen.Poput;
                    BtnClipVstrech.Text = DeviceChosen.Vstrech;
                    BtnClipNCode.Text = DeviceChosen.NCode;
                    BtnClipGps.Text = DeviceChosen.Gps;
                    BtnClipDeviceIP.Text = DeviceChosen.DeviceIP;
                    BtnToWebDeviceIPTech.Text = DeviceChosen.DeviceIPTech;
                    BtnClipEtherProvider.Text = DeviceChosen.EtherProvider;
                    BtnClipIrzIp.Text = DeviceChosen.IrzIp;
                    BtnToWebShinobiIp.Text = DeviceChosen.ShinobiIp;
                    BtnClipCamQuant.Text = DeviceChosen.CamQuant;
                    BtnClipPodrOrg1Common.Text = DeviceChosen.PodrOrg1Common;
                    BtnClipPodrOrg2Common.Text = DeviceChosen.PodrOrg2Common;
                    if (DeviceChosen.PodrOrg1Common == "МКЛ")
                    {
                        BtnCreateIssue1.BackgroundImage = global::aisha3.Properties.Resources.mkl;
                    }
                    else
                    {
                        BtnCreateIssue1.BackgroundImage = global::aisha3.Properties.Resources.netline;
                    }
                    LblInfoConst.Text = DeviceChosen.InfoConst;
                    BtnClipSpeed.Text = DeviceChosen.Speed + "\nкм/ч";
                    CommentTBox.Text = DeviceChosen.InfoDyn;
                }
            }
        }

        public static string TodayDateTime()
        {
            return DateTime.Now.ToString("HH:mm dd.MM.yy");
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
            CamPanelOuter.BackColor = borderColor;
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
            if(DeviceChosen != null) { Clipboard.SetText(DeviceChosen.GK); }
        }
        private void BtnClipDeviceType_Click(object sender, EventArgs e)
        {
            if (DeviceChosen != null) { Clipboard.SetText(DeviceChosen.DeviceType); }
        }
        private void BtnClipKvfModel_Click(object sender, EventArgs e)
        {
            if (DeviceChosen != null) { Clipboard.SetText(DeviceChosen.KvfModel); }
        }
        private void BtnClipKvfNumber_Click(object sender, EventArgs e)
        {
            if (DeviceChosen != null) { Clipboard.SetText(DeviceChosen.KvfNumber); }
        }
        private void DEBUG_Btn_Click(object sender, EventArgs e) //DEBUG BTN
        {
            string mssqldb = Properties.Settings.Default.mssqldb;
            string mssqlcatalog = Properties.Settings.Default.mssqlcatalog;
            string mssqluser = Properties.Settings.Default.mssqluser;
            string mssqlpass = Properties.Settings.Default.mssqlpass;
            string mssqlintegrated = Properties.Settings.Default.mssqlintegrated.ToString();
            MessageBox.Show($"{mssqldb} - {mssqlcatalog} - {mssqluser} - {mssqlpass} - {mssqlintegrated}");
        }
        private void BtnClipAddress_Click(object sender, EventArgs e)
        {
            if (LblOfAddress.Text == "Адрес РГИС:")
            {
                if (DeviceChosen != null) { Clipboard.SetText(DeviceChosen.AddressRGIS); }
            }
            else
            {
                if (DeviceChosen != null) { Clipboard.SetText(DeviceChosen.AddressDoc); }
            }
        }
        private void BtnClipAddress_TextChanged(object sender, EventArgs e)
        {
            if(BtnClipAddress.Text != "")
            {
                BtnClipAddress.Text = Regex.Replace(BtnClipAddress.Text.ToString(), "(?<=\\G.{39})(?=.)", "\n");
            }
            else { BtnClipAddress.Text = "_"; }
        }
        private void BtnClipPoput_Click(object sender, EventArgs e)
        {
            if(BtnClipPoput.Text != "") { Clipboard.SetText(BtnClipPoput.Text.ToString().Replace("\n", "")); }
        }
        private void BtnClipPoput_TextChanged(object sender, EventArgs e)
        {
            if(BtnClipPoput.Text != "")
            {
                BtnClipPoput.Text = Regex.Replace(BtnClipPoput.Text.ToString(), "(?<=\\G.{39})(?=.)", "\n");
            }
            else { BtnClipPoput.Text = "_"; }
        }
        private void BtnClipVstrech_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipVstrech.Text.ToString().Replace("\n", ""));
        }
        private void BtnClipVstrech_TextChanged(object sender, EventArgs e)
        {
            if (BtnClipVstrech.Text != "")
            {
                BtnClipVstrech.Text = Regex.Replace(BtnClipVstrech.Text.ToString(), "(?<=\\G.{39})(?=.)", "\n");
            }
            else { BtnClipVstrech.Text = "_"; }
        }

        private void BtnClipNCode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnClipNCode.Text.ToString().Replace("\n", ""));
        }

        private void BtnClipNCode_TextChanged(object sender, EventArgs e)
        {
            if(BtnClipNCode.Text != "")
            {
                BtnClipNCode.Text = Regex.Replace(BtnClipNCode.Text.ToString(), "(?<=\\G.{39})(?=.)", "\n");
            }
            else
            {
                BtnClipNCode.Text = "_";
            }
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
            if(DeviceChosen != null)
            {
                if(DeviceChosen.Rtsp != null)
                {
                    Clipboard.SetText(DeviceChosen.Rtsp);
                }
            }
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
            Clipboard.SetText(TodayDateTime() 
            + " - результат диагностики подрядной организации (МКЛ) - отсутствует ЭП в ШУ;/отсутствует КС;");
        }

        private void BtnClipR2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
            + " - результат диагностики подрядной организации (Нетлайн) - отсутствует ЭП в ШУ;/отсутствует КС;");
        }

        private void BtnClipR3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
           + " - результат диагностики АВР бригады РЦР - отсутствует ЭП в ШУ;/отсутствует КС;");
        }

        private void BtnClipR4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
           + " - направлена заявка подрядной организации (ПТС/ТАКТ) на восстановление ЭП/КС;");
        }

        private void BtnClipR5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
           + " - отсутствие фиксаций, сбита ориентация прибора, исправлена, комплекс работает в штатном режиме.");
        }

        private void BtnClipR6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
           + " - отсутствие фиксаций, подрядная организация (Нетлайн/МКЛ)/АВР бригада РЦР выполнила очистку оптики КВФ.");
        }

        private void BtnClipR7_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
           + " - направлена заявка в ТП РТК на отсутствие ЭП;/на отсутствие канала связи;/на нестабильность канала связи;");
        }

        private void BtnClipR8_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
           + " - обновление статуса заявки от ТП РТК - принято в работу;");
        }

        private void BtnClipR9_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
           + " - направлена заявка в ТП ЭРТХ на отсутствие канала связи;/на нестабильность канала связи;");
        }

        private void BtnClipR10_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
           + " - обновление статуса заявки от ТП ЭРТХ - принято в работу за № ____ ;");
        }

        private void BtnClipR11_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
           + " - отсутствие фиксаций и видеопотока, работоспособность комплекса восстановлена.");
        }

        private void BtnClipRCustom_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TodayDateTime()
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
            Cams?.Clear();
            Extras?.Clear();
            if (TBox.Text.Length >= 3)
            {
                Devices = Mssql.DevicesbyString(TBox.Text.ToString());
                SetMainPanel(0);
                if(Devices != null)
                {
                    if (Devices.Count > 1)
                    {
                        switch (Devices.Count)
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

        private void BtnToWebDeviceIPTech_TextChanged(object sender, EventArgs e)
        {
            if(BtnToWebDeviceIPTech.Text == "")
            {
                BtnToWebDeviceIPTech.Text = "_";
            }
        }

        private void BtnClipIrzIp_TextChanged(object sender, EventArgs e)
        {
            if (BtnClipIrzIp.Text == "")
            {
                BtnClipIrzIp.Text = "_";
            }
        }

        private void BtnToHttpKsm_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeviceChosen != null)
                {
                    if(DeviceChosen.KsmHttp != "")
                    {
                        System.Diagnostics.Process.Start(DeviceChosen.KsmHttp);
                    }
                }
            }
            catch (Exception) { }
        }

        private void BtnToHttpDuplo1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeviceChosen != null)
                {
                    if (DeviceChosen.KsmHttp != "")
                    {
                        System.Diagnostics.Process.Start("http://" + DeviceChosen.Duplo1Ip);
                    }
                }
            }
            catch (Exception) { }
        }

        private void BtnToHttpDuplo2_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeviceChosen != null)
                {
                    if (DeviceChosen.KsmHttp != "")
                    {
                        System.Diagnostics.Process.Start("http://" + DeviceChosen.Duplo2Ip);
                    }
                }
            }
            catch (Exception) { }
        }
        private void BtnClipSpeed_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeviceChosen != null)
                {
                    if (DeviceChosen.Speed != "")
                    {
                        Clipboard.SetText(DeviceChosen.Speed.ToString());
                    }
                }
            }
            catch (Exception) { }
        }
        private void BtnIssue0_Click(object sender, EventArgs e)
        {
            BtnIssue1.BackgroundImage = global::aisha3.Properties.Resources.nophoto;
            BtnIssue2.BackgroundImage = global::aisha3.Properties.Resources.videocamoff;
            BtnIssue3.BackgroundImage = global::aisha3.Properties.Resources.castwarning;
            BtnIssue4.BackgroundImage = global::aisha3.Properties.Resources.sunnysnow;
            BtnIssue5.BackgroundImage = global::aisha3.Properties.Resources.locationoff;
            BtnIssue6.BackgroundImage = global::aisha3.Properties.Resources.pulsealert;
            ChosenIssueTheme = 0;
        }
        private void BtnIssue1_Click(object sender, EventArgs e)
        {
            BtnIssue1.BackgroundImage = global::aisha3.Properties.Resources.nophoto1;
            BtnIssue2.BackgroundImage = global::aisha3.Properties.Resources.videocamoff;
            BtnIssue3.BackgroundImage = global::aisha3.Properties.Resources.castwarning;
            BtnIssue4.BackgroundImage = global::aisha3.Properties.Resources.sunnysnow;
            BtnIssue5.BackgroundImage = global::aisha3.Properties.Resources.locationoff;
            BtnIssue6.BackgroundImage = global::aisha3.Properties.Resources.pulsealert;
            ChosenIssueTheme = 1;
        }
        private void BtnIssue2_Click(object sender, EventArgs e)
        {
            BtnIssue1.BackgroundImage = global::aisha3.Properties.Resources.nophoto;
            BtnIssue2.BackgroundImage = global::aisha3.Properties.Resources.videocamoff1;
            BtnIssue3.BackgroundImage = global::aisha3.Properties.Resources.castwarning;
            BtnIssue4.BackgroundImage = global::aisha3.Properties.Resources.sunnysnow;
            BtnIssue5.BackgroundImage = global::aisha3.Properties.Resources.locationoff;
            BtnIssue6.BackgroundImage = global::aisha3.Properties.Resources.pulsealert;
            ChosenIssueTheme = 2;
        }
        private void BtnIssue3_Click(object sender, EventArgs e)
        {
            BtnIssue1.BackgroundImage = global::aisha3.Properties.Resources.nophoto;
            BtnIssue2.BackgroundImage = global::aisha3.Properties.Resources.videocamoff;
            BtnIssue3.BackgroundImage = global::aisha3.Properties.Resources.castwarning1;
            BtnIssue4.BackgroundImage = global::aisha3.Properties.Resources.sunnysnow;
            BtnIssue5.BackgroundImage = global::aisha3.Properties.Resources.locationoff;
            BtnIssue6.BackgroundImage = global::aisha3.Properties.Resources.pulsealert;
            ChosenIssueTheme = 3;
        }
        private void BtnIssue4_Click(object sender, EventArgs e)
        {
            BtnIssue1.BackgroundImage = global::aisha3.Properties.Resources.nophoto;
            BtnIssue2.BackgroundImage = global::aisha3.Properties.Resources.videocamoff;
            BtnIssue3.BackgroundImage = global::aisha3.Properties.Resources.castwarning;
            BtnIssue4.BackgroundImage = global::aisha3.Properties.Resources.sunnysnow1;
            BtnIssue5.BackgroundImage = global::aisha3.Properties.Resources.locationoff;
            BtnIssue6.BackgroundImage = global::aisha3.Properties.Resources.pulsealert;
            ChosenIssueTheme = 4;
        }
        private void BtnIssue5_Click(object sender, EventArgs e)
        {
            BtnIssue1.BackgroundImage = global::aisha3.Properties.Resources.nophoto;
            BtnIssue2.BackgroundImage = global::aisha3.Properties.Resources.videocamoff;
            BtnIssue3.BackgroundImage = global::aisha3.Properties.Resources.castwarning;
            BtnIssue4.BackgroundImage = global::aisha3.Properties.Resources.sunnysnow;
            BtnIssue5.BackgroundImage = global::aisha3.Properties.Resources.locationoff1;
            BtnIssue6.BackgroundImage = global::aisha3.Properties.Resources.pulsealert;
            ChosenIssueTheme = 5;
        }
        private void BtnIssue6_Click(object sender, EventArgs e)
        {
            BtnIssue1.BackgroundImage = global::aisha3.Properties.Resources.nophoto;
            BtnIssue2.BackgroundImage = global::aisha3.Properties.Resources.videocamoff;
            BtnIssue3.BackgroundImage = global::aisha3.Properties.Resources.castwarning;
            BtnIssue4.BackgroundImage = global::aisha3.Properties.Resources.sunnysnow;
            BtnIssue5.BackgroundImage = global::aisha3.Properties.Resources.locationoff;
            BtnIssue6.BackgroundImage = global::aisha3.Properties.Resources.pulsealert1;
            ChosenIssueTheme = 6;
        }
        private void BtnCreateIssue1_Click(object sender, EventArgs e)
        {
            KvmHook.IssueToWeb(ChosenIssueTheme, 1);
        }

        private void BtnCreateIssue2_Click(object sender, EventArgs e)
        {
            KvmHook.IssueToWeb(ChosenIssueTheme, 2);
        }

        private void BtnCreateIssue3_Click(object sender, EventArgs e)
        {
            KvmHook.IssueToWeb(ChosenIssueTheme, 3);
        }

        private void CommentTBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (DeviceChosen != null)
                {
                    if (CommentTBox.Text.ToString() != "")
                    {
                        Mssql.SetInfoDyn(DeviceChosen.KvfNumber, CommentTBox.Text.ToString());
                    }
                }
            }
        }

        private void BtnMap_Click(object sender, EventArgs e)
        {
            if (MapOpen)
            {
                MapOpen = false;
                MapPanelOuter.Visible = false;
            }
            else
            {
                MapOpen = true;
                MapPanelOuter.Visible = true;
                
            }
        }

        private void BtnCams_Click(object sender, EventArgs e)
        {
            if (CamsOpen)
            {
                CamsOpen = false;
                CamPanelOuter.Visible = false;
            }
            else
            {
                if (SortOpen)
                {
                    SortOpen = false;
                    SortPanelOuter.Visible = false;
                    SortPrefPanelOuter.Visible = false;

                    CamsOpen = true;
                    CamPanelOuter.Visible = true;
                }
                else
                {
                    CamsOpen = true;
                    CamPanelOuter.Visible = true;
                }
            }
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            if (SortOpen)
            {
                SortOpen = false;
                SortPanelOuter.Visible = false;
                SortPrefPanelOuter.Visible = false;
            }
            else
            {
                if (CamsOpen)
                {
                    CamsOpen = false;
                    CamPanelOuter.Visible = false;

                    SortOpen = true;
                    SortPanelOuter.Visible = true;
                    SortPrefPanelOuter.Visible = true;
                }
                else
                {
                    SortOpen = true;
                    SortPanelOuter.Visible = true;
                    SortPrefPanelOuter.Visible = true;
                }
            }
        }

        private void Sort()
        {
            try
            {
                DevicesSort.Clear();
                DGV.Rows.Clear();
                DevicesSort = Mssql.DevicesbySort();
                int i = 0;
                foreach(var item in DevicesSort)
                {
                    if(i <= DevicesSort.Count-1)
                    {
                        DGV.Rows.Add(i+1, DevicesSort[i].KvfNumber, DevicesSort[i].GKCommon, DevicesSort[i].Vstrech, DevicesSort[i].Poput,
                            DevicesSort[i].PodrOrg1Common, DevicesSort[i].PodrOrg2Common, DevicesSort[i].CamFixType,
                            DevicesSort[i].EtherProvider, DevicesSort[i].DeviceIP, DevicesSort[i].KsmHttp);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR SORT: {ex.Message}");
            }
        }

        private void BtnUseSort_Click(object sender, EventArgs e)
        {
            Sort();
            BtnDGVCount.Text = DevicesSort.Count.ToString();
        }

        private void BtnDGVCount_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(BtnDGVCount.Text.ToString());
        }

        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == 8 && senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                try
                {
                    System.Diagnostics.Process.Start("http://" + senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
                catch (Exception) { }
            }
            if (e.ColumnIndex == 9 && senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                try
                {
                    System.Diagnostics.Process.Start(senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
                catch (Exception) { }
            }
        }

        private string DgvToClipString()
        {
            string output = "№ п.п.\t№ КВФ\tГК\tМодель\tТип\tТип камеры\tАдрес по ГК\tАдрес по РГИС\tвстречное\tпопутное\tрайон СПб\tGPS\tIP\tПодр.1\tПодр.2\tКС\tНарушения\tСкорость\r\n";
            int i = 0;
            foreach (var item in DevicesSort)
            {
                if (i <= DevicesSort.Count - 1)
                {
                    output += $"{i + 1}\t{DevicesSort[i].KvfNumber}\t{DevicesSort[i].GKCommon}\t{DevicesSort[i].KvfModelCommon}" +
                        $"\t{DevicesSort[i].DeviceType}\t{DevicesSort[i].CamFixType}\t{DevicesSort[i].AddressDoc}" +
                        $"\t{DevicesSort[i].AddressRGIS}\t{DevicesSort[i].Vstrech}\t{DevicesSort[i].Poput}" +
                        $"\t{DevicesSort[i].Dist}\t{DevicesSort[i].Gps}\t{DevicesSort[i].DeviceIP}" +
                        $"\t{DevicesSort[i].PodrOrg1Common}\t{DevicesSort[i].PodrOrg2Common}\t{DevicesSort[i].EtherProvider}" +
                        $"\t{DevicesSort[i].NCode}\t{DevicesSort[i].Speed}\r\n";
                    i++;
                }
            }
            return output;
        }

        private void BtnDGVToClip_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(DgvToClipString());
        }



        //Main panel btns

        //MAP
        public void MapMakeMarker(Device device)
        {
            string[] coords = device.Gps.ToString().Split(" ".ToCharArray());
            string latitudeStr = coords[0];
            string longitudeStr = coords[1];
            if (double.TryParse(latitudeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude) &&
                        double.TryParse(longitudeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
            {
                if (int.TryParse(device.Azimut, out int azimuth))
                { }
                else { Console.WriteLine("ERROR 18: Failed to parse marker azimuth."); }

                Bitmap icon = CreateVectorIcon(32, 32);
                Bitmap rotatedIcon = RotateImage(icon, azimuth);

                GMapOverlay markersOverlay = new GMapOverlay("markers");
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(latitude, longitude), rotatedIcon);

                marker.ToolTip = new CustomToolTip(marker);
                marker.ToolTipMode = MarkerTooltipMode.Always;

                if (device.DeviceType == "перекресток")
                {
                    marker.ToolTipText = device.KvfNumber + " - " + device.KvfModelCommon + " - " + device.GKCommon;
                }
                else if (device.DeviceType == "камера")
                {
                    marker.ToolTipText = device.KvfNumber + " - " + device.CamFixType +
                        $"\nВстречное: {device.Vstrech}" +
                        $"\nПопутное: {device.Poput}";
                }
                else
                {
                    marker.ToolTipText = device.KvfNumber + " - " + device.KvfModelCommon + " - " + device.GKCommon +
                        $"\nВстречное: {device.Vstrech}" +
                        $"\nПопутное: {device.Poput}";
                }

                markersOverlay.Markers.Add(marker);
                GMapUI.Overlays.Add(markersOverlay);
            }
            else
            {
                Console.WriteLine("ERROR 15: Failed to make marker latitude and longitude.");
            }
        }

        public class CustomToolTip : GMapToolTip
        {
            private readonly Font _font;
            private readonly Brush _backgroundBrush;
            private readonly Brush _textBrush;

            public CustomToolTip(GMapMarker marker) : base(marker)
            {
                _font = new Font("Arial", 7); // Уменьшаем кегль текста
                _backgroundBrush = new SolidBrush(Color.FromArgb(10, Color.Gray)); // Устанавливаем более прозрачный фон
                _textBrush = new SolidBrush(Color.Black);
            }

            public override void OnRender(Graphics g)
            {
                SizeF textSize = g.MeasureString(Marker.ToolTipText, _font);
                SizeF adjustedSize = new SizeF(textSize.Width, textSize.Height * 0.6f); // Уменьшаем высоту
                RectangleF rect = new RectangleF(Marker.ToolTipPosition.X, Marker.ToolTipPosition.Y - adjustedSize.Height, adjustedSize.Width + TextPadding.Width, adjustedSize.Height + TextPadding.Height);
                RectangleF rectText = new RectangleF(Marker.ToolTipPosition.X, Marker.ToolTipPosition.Y - adjustedSize.Height, adjustedSize.Width + TextPadding.Width, adjustedSize.Height + TextPadding.Height);
                rect.Offset(Offset.X, Offset.Y);
                rectText.Offset(Offset.X + 7, Offset.Y + 7);
                g.DrawLine(Stroke, Marker.ToolTipPosition.X, Marker.ToolTipPosition.Y, rect.X, rect.Y + rect.Height / 2f);
                g.FillRectangle(_backgroundBrush, rect);
                DrawRoundRectangle(g, Stroke, rect.X, rect.Y, rect.Width, rect.Height, 8f);
                g.DrawString(Marker.ToolTipText, _font, _textBrush, rectText);
                g.Flush();
            }
        }

        public void MapShowKvf(Device device)
        {
            if(device.DeviceType == "перекресток")
            {
                GMapUI.Overlays.Clear();
                GMapUI.Zoom = 10;
                string[] coords = device.Gps.ToString().Split(" ".ToCharArray());
                string latitudeStr = coords[0];
                string longitudeStr = coords[1];

                if (double.TryParse(latitudeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude) &&
                        double.TryParse(longitudeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
                {
                    GMapUI.Position = new PointLatLng(latitude, longitude);
                    GMapUI.Zoom = 16;
                    MapMakeMarker(device);
                }
                else
                {
                    Console.WriteLine("ERROR 16: Failed to parse latitude and longitude.");
                }
                BtnClipGPSAddress.Text = $"{device.KvfModel} {device.KvfNumber} {device.DeviceType} - ГК: {device.GKCommon}" +
                    $"\nАдрес РГИС: {device.AddressRGIS} - Кол-во камер: {device.CamQuant}";
            }
            else
            {
                GMapUI.Overlays.Clear();
                GMapUI.Zoom = 10;
                string[] coords = device.Gps.ToString().Split(" ".ToCharArray());
                string latitudeStr = coords[0];
                string longitudeStr = coords[1];

                if (double.TryParse(latitudeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude) &&
                        double.TryParse(longitudeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
                {
                    GMapUI.Position = new PointLatLng(latitude, longitude);
                    GMapUI.Zoom = 16;
                    MapMakeMarker(device);
                }
                else
                {
                    Console.WriteLine("ERROR 19: Failed to parse latitude and longitude.");
                }
                BtnClipGPSAddress.Text = $"{device.KvfModel} {device.KvfNumber} {device.DeviceType} - ГК: {device.GKCommon}" +
                    $"\nАдрес РГИС: {device.AddressRGIS} - Кол-во камер: {device.CamQuant}";
            }
        }

        public void MapSearchAndShow(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("User-Agent: Other");
                    string nomStrStart = "https://nominatim.openstreetmap.org/search?addressdetails=1&q=";
                    string nomStrMiddle = text.Trim().Replace(" ", "+");
                    string nomStrEnd = "&format=jsonv2&limit=1";
                    byte[] jsonData = webClient.DownloadData(nomStrStart + nomStrMiddle + nomStrEnd);
                    string json = System.Text.Encoding.UTF8.GetString(jsonData);
                    JArray jsonArray = JArray.Parse(json);

                    string latitudeStr = jsonArray[0]["lat"].ToString();
                    string longitudeStr = jsonArray[0]["lon"].ToString();

                    if (double.TryParse(latitudeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude) &&
                        double.TryParse(longitudeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
                    {
                        string address = jsonArray[0]["display_name"].ToString();
                        BtnClipGPSAddress.Text = $"Широта N: {latitude}, Долгота E {longitude}\nAddress: {address}";
                        GMapUI.Position = new PointLatLng(latitude, longitude);
                    }
                    else
                    {
                        Console.WriteLine("ERROR 17: Failed to parse latitude and longitude.");
                        BtnClipGPSAddress.Text = $"Не удалось найти \"text\"";
                    }
                }
            }
        }

        private void BtnClipGPSAddress_Click(object sender, EventArgs e)
        {
            if (BtnClipGPSAddress.Text.ToString() != "")
            {
                Clipboard.SetText(BtnClipGPSAddress.Text.ToString());
            }
        }

        private void GMapSearchTBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(GMapSearchTBox.Text.ToString().Trim() != "")
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    // Вызываем метод GetGPSRequest при нажатии клавиши Enter
                    MapSearchAndShow(GMapSearchTBox.Text.ToString());
                    // После вызова метода можно предотвратить дальнейшее обработка события клавиши Enter
                    e.Handled = true;
                }
            }
        }

        private void GMapUI_Load(object sender, EventArgs e)
        {
            GMapUI.MouseDown += GMap_MouseDown;
            GMapUI.MouseMove += GMap_MouseMove;
            GMapUI.MouseUp += GMap_MouseUp;

            GMapUI.Bearing = 0;
            GMapUI.CanDragMap = true;
            GMapUI.DragButton = MouseButtons.Left;
            GMapUI.GrayScaleMode = false;
            GMapUI.MarkersEnabled = true;
            GMapUI.MaxZoom = 20;
            GMapUI.MinZoom = 8;
            GMapUI.Zoom = 10;
            GMapUI.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            GMapUI.MapProvider = GMapProviders.OpenStreetMap;
            
            GMapUI.Position = new PointLatLng(59.942998, 30.269919);
            if(DeviceChosen != null)
            {
                MapShowKvf(DeviceChosen);
            }
        }

        private Bitmap CreateVectorIcon(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                GraphicsPath path = new GraphicsPath();

                path.AddPolygon(new PointF[]
                {
                    new PointF(width / 2, 0),
                    new PointF(width * 3 / 4, height),
                    new PointF(width / 4, height)
                });

                Color semiTransparentBlue = Color.FromArgb(128, Color.Blue);
                using (Brush semiTransparentBrush = new SolidBrush(semiTransparentBlue))
                {
                    g.FillPath(semiTransparentBrush, path);
                }
                using (Pen semiTransparentPen = new Pen(Color.FromArgb(128, Color.Black)))
                {
                    g.DrawPath(semiTransparentPen, path);
                }
            }
            return bmp;
        }


        private Bitmap RotateImage(Bitmap bmp, float angle)
        {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
                g.DrawImage(bmp, new Point(0, 0));
            }
            return rotatedImage;
        }



        private void GMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }
        private void GMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }
        private void GMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }

        private void BtnShowCamsOnMap_Click(object sender, EventArgs e)
        {
            if(Cams != null)
            {
                foreach (var dictionary in Cams)
                {
                    Device cam = dictionary.Value;
                    MapMakeMarker(cam);
                }
            }
            if (Extras != null)
            {
                foreach (var dictionary in Extras)
                {
                    Device extra = dictionary.Value;
                    MapMakeMarker(extra);
                }
            }
        }

        private void BtnIssues_Click(object sender, EventArgs e)
        {
            if(IssuesOpen)
            {
                BtnIssues.BackColor = Color.Black;
                BtnIssues.ForeColor = Color.White;
                IssuesOpen = false;
            }
            else
            {
                IssuesOpen = true;
                BtnIssues.BackColor = Color.White;
                BtnIssues.ForeColor = Color.Black;
            }
        }
        //MAP
    }
}
