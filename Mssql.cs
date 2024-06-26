﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlDAdapter = Microsoft.Data.SqlClient.SqlDataAdapter;
using MSSQLCmd = Microsoft.Data.SqlClient.SqlCommand;
using System.Data;
using MSSQLReader = Microsoft.Data.SqlClient.SqlDataReader;
using System.Data.Common;
using MSSQLConnection = Microsoft.Data.SqlClient.SqlConnection;
using System.Security.Cryptography;
using System.IO;
using System.Threading;

namespace aisha3
{
    public class Mssql
    {
        public static bool Connected = false;
        public static string mssqldb = Properties.Settings.Default.mssqldb;
        public static string mssqlcatalog = Properties.Settings.Default.mssqlcatalog;
        public static string mssqluser = Properties.Settings.Default.mssqluser;
        public static string mssqlpass = Properties.Settings.Default.mssqlpass;
        public static string mssqlintegrated = Properties.Settings.Default.mssqlintegrated.ToString();
        public static MSSQLConnection conn = new MSSQLConnection
            ($"Data Source={mssqldb}; Initial Catalog={mssqlcatalog};" +
            $" User ID={mssqluser}; Password={mssqlpass}; Integrated Security={mssqlintegrated};TrustServerCertificate=True");

        public static void OpenConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public static void CloseConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            return conn;
        }

        public static int GetLastVersion()
        {
            try
            {
                OpenConnection();
                string selectString = "SELECT TOP 1 version FROM dbo.aishaversions ORDER BY version DESC;";
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                MSSQLReader reader = cmdgetversion.ExecuteReader();
                reader.Read();
                int output = reader.GetInt32(0);
                reader.Close();
                Connected = true;
                return output;
            }
            catch (Exception ex)
            {
                Connected = false;
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public static Task<bool> DownloadNewVersionAsync()
        {
            Thread.Sleep(3000);
            return Task.Run(() => DownloadNewVersion());
        }

        public static bool DownloadNewVersion()
        {
            try
            {
                using (conn)
                {
                    OpenConnection();
                    string query = "SELECT TOP 1 bin FROM aishaversions ORDER BY version DESC";
                    MSSQLCmd command = new MSSQLCmd(query, conn);

                    if (command.ExecuteScalar() is byte[] newVersionData)
                    {
                        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string updatesDirectory = Path.Combine(appDirectory, "updates");
                        if (!Directory.Exists(updatesDirectory))
                        {
                            Directory.CreateDirectory(updatesDirectory);
                        }
                        string newFilePath = Path.Combine(updatesDirectory, "aisha3.exe");
                        File.WriteAllBytes(newFilePath, newVersionData);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error 21 - downloading new version: " + ex.Message);
            }
            return false;
        }


        public static Dictionary<int, Device> DevicesbyString(string code)
        {
            try
            {
                Dictionary<int, Device> Devices = new Dictionary<int, Device>();
                OpenConnection();
                string selectString = $"SELECT * FROM dbo.aishadt WHERE KvfNumber LIKE '%{code}%'" +
                    $"AND DeviceType NOT LIKE 'камера'" +
                    $"AND CamFixType LIKE 'некамера'";
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                SqlDAdapter adapter = new SqlDAdapter(selectString, conn);
                DataSet ds = new DataSet();
                ds?.Clear();
                adapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Device device = new Device
                        {
                            Code = ds.Tables[0].Rows[i].Field<string>("Code").ToString(),
                            KvfNumber = ds.Tables[0].Rows[i].Field<string>("KvfNumber").ToString(),
                            AddressDoc = ds.Tables[0].Rows[i].Field<string>("AddressDoc").ToString(),
                            AddressRGIS = ds.Tables[0].Rows[i].Field<string>("AddressRGIS").ToString(),
                            Vstrech = ds.Tables[0].Rows[i].Field<string>("Vstrech").ToString(),
                            Poput = ds.Tables[0].Rows[i].Field<string>("Poput").ToString(),
                            Dist = ds.Tables[0].Rows[i].Field<string>("Dist").ToString(),
                            Gps = ds.Tables[0].Rows[i].Field<string>("Gps").ToString(),
                            Azimut = ds.Tables[0].Rows[i].Field<Int32>("Azimut").ToString(),
                            InfoConst = ds.Tables[0].Rows[i].Field<string>("InfoConst").ToString(),
                            InfoDyn = ds.Tables[0].Rows[i].Field<string>("InfoDyn").ToString(),
                            KvfModel = ds.Tables[0].Rows[i].Field<string>("KvfModel").ToString(),
                            KvfModelCommon = ds.Tables[0].Rows[i].Field<string>("KvfModelCommon").ToString(),
                            DeviceType = ds.Tables[0].Rows[i].Field<string>("DeviceType").ToString(),
                            DeviceIP = ds.Tables[0].Rows[i].Field<string>("DeviceIP").ToString(),
                            DeviceGWIP = ds.Tables[0].Rows[i].Field<string>("DeviceGWIP").ToString(),
                            CamOwnerKvfNumber = ds.Tables[0].Rows[i].Field<string>("CamOwnerKvfNumber").ToString(),
                            CamFixType = ds.Tables[0].Rows[i].Field<string>("CamFixType").ToString(),
                            CamQuant = ds.Tables[0].Rows[i].Field<string>("CamQuant").ToString(),
                            GK = ds.Tables[0].Rows[i].Field<string>("GK").ToString(),
                            GKCommon = ds.Tables[0].Rows[i].Field<string>("GKCommon").ToString(),
                            OrgOwner = ds.Tables[0].Rows[i].Field<string>("OrgOwner").ToString(),
                            OrgOwnerCommon = ds.Tables[0].Rows[i].Field<string>("OrgOwnerCommon").ToString(),
                            PodrOrg1 = ds.Tables[0].Rows[i].Field<string>("PodrOrg1").ToString(),
                            PodrOrg1Common = ds.Tables[0].Rows[i].Field<string>("PodrOrg1Common").ToString(),
                            PodrOrg2 = ds.Tables[0].Rows[i].Field<string>("PodrOrg2").ToString(),
                            PodrOrg2Common = ds.Tables[0].Rows[i].Field<string>("PodrOrg2Common").ToString(),
                            EtherProvider = ds.Tables[0].Rows[i].Field<string>("EtherProvider").ToString(),
                            EtherProviderID = ds.Tables[0].Rows[i].Field<string>("EtherProviderID").ToString(),
                            KsmHttp = ds.Tables[0].Rows[i].Field<string>("KsmHttp").ToString(),
                            NCode = ds.Tables[0].Rows[i].Field<string>("NCode").ToString(),
                            Speed = ds.Tables[0].Rows[i].Field<Int32>("Speed").ToString(),
                            IrzIp = ds.Tables[0].Rows[i].Field<string>("IrzIp").ToString(),
                            Duplo1Ip = ds.Tables[0].Rows[i].Field<string>("Duplo1Ip").ToString(),
                            Duplo2Ip = ds.Tables[0].Rows[i].Field<string>("Duplo2Ip").ToString(),
                            DeviceIPTech = ds.Tables[0].Rows[i].Field<string>("DeviceIPTech").ToString(),
                            ShinobiIp = ds.Tables[0].Rows[i].Field<string>("ShinobiIp").ToString(),
                            Rtsp = ds.Tables[0].Rows[i].Field<string>("Rtsp").ToString(),
                            XlsName = ds.Tables[0].Rows[i].Field<string>("XlsName").ToString()
                        };
                        Devices.Add(i, device);
                        i++;
                    }
                }
                MainForm.Connected = true;
                return Devices;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR1 - main search: " + ex.Message);
                return null;
            }
        }

        public static Dictionary<int, Device> DevicesAll()
        {
            try
            {
                Dictionary<int, Device> Devices = new Dictionary<int, Device>();
                OpenConnection();
                string selectString = $"SELECT * FROM dbo.aishadt";
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                SqlDAdapter adapter = new SqlDAdapter(selectString, conn);
                DataSet ds = new DataSet();
                ds?.Clear();
                adapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Device device = new Device
                        {
                            Code = ds.Tables[0].Rows[i].Field<string>("Code").ToString(),
                            KvfNumber = ds.Tables[0].Rows[i].Field<string>("KvfNumber").ToString(),
                            AddressDoc = ds.Tables[0].Rows[i].Field<string>("AddressDoc").ToString(),
                            AddressRGIS = ds.Tables[0].Rows[i].Field<string>("AddressRGIS").ToString(),
                            Vstrech = ds.Tables[0].Rows[i].Field<string>("Vstrech").ToString(),
                            Poput = ds.Tables[0].Rows[i].Field<string>("Poput").ToString(),
                            Dist = ds.Tables[0].Rows[i].Field<string>("Dist").ToString(),
                            Gps = ds.Tables[0].Rows[i].Field<string>("Gps").ToString(),
                            Azimut = ds.Tables[0].Rows[i].Field<Int32>("Azimut").ToString(),
                            InfoConst = ds.Tables[0].Rows[i].Field<string>("InfoConst").ToString(),
                            InfoDyn = ds.Tables[0].Rows[i].Field<string>("InfoDyn").ToString(),
                            KvfModel = ds.Tables[0].Rows[i].Field<string>("KvfModel").ToString(),
                            KvfModelCommon = ds.Tables[0].Rows[i].Field<string>("KvfModelCommon").ToString(),
                            DeviceType = ds.Tables[0].Rows[i].Field<string>("DeviceType").ToString(),
                            DeviceIP = ds.Tables[0].Rows[i].Field<string>("DeviceIP").ToString(),
                            DeviceGWIP = ds.Tables[0].Rows[i].Field<string>("DeviceGWIP").ToString(),
                            CamOwnerKvfNumber = ds.Tables[0].Rows[i].Field<string>("CamOwnerKvfNumber").ToString(),
                            CamFixType = ds.Tables[0].Rows[i].Field<string>("CamFixType").ToString(),
                            CamQuant = ds.Tables[0].Rows[i].Field<string>("CamQuant").ToString(),
                            GK = ds.Tables[0].Rows[i].Field<string>("GK").ToString(),
                            GKCommon = ds.Tables[0].Rows[i].Field<string>("GKCommon").ToString(),
                            OrgOwner = ds.Tables[0].Rows[i].Field<string>("OrgOwner").ToString(),
                            OrgOwnerCommon = ds.Tables[0].Rows[i].Field<string>("OrgOwnerCommon").ToString(),
                            PodrOrg1 = ds.Tables[0].Rows[i].Field<string>("PodrOrg1").ToString(),
                            PodrOrg1Common = ds.Tables[0].Rows[i].Field<string>("PodrOrg1Common").ToString(),
                            PodrOrg2 = ds.Tables[0].Rows[i].Field<string>("PodrOrg2").ToString(),
                            PodrOrg2Common = ds.Tables[0].Rows[i].Field<string>("PodrOrg2Common").ToString(),
                            EtherProvider = ds.Tables[0].Rows[i].Field<string>("EtherProvider").ToString(),
                            EtherProviderID = ds.Tables[0].Rows[i].Field<string>("EtherProviderID").ToString(),
                            KsmHttp = ds.Tables[0].Rows[i].Field<string>("KsmHttp").ToString(),
                            NCode = ds.Tables[0].Rows[i].Field<string>("NCode").ToString(),
                            Speed = ds.Tables[0].Rows[i].Field<Int32>("Speed").ToString(),
                            IrzIp = ds.Tables[0].Rows[i].Field<string>("IrzIp").ToString(),
                            Duplo1Ip = ds.Tables[0].Rows[i].Field<string>("Duplo1Ip").ToString(),
                            Duplo2Ip = ds.Tables[0].Rows[i].Field<string>("Duplo2Ip").ToString(),
                            DeviceIPTech = ds.Tables[0].Rows[i].Field<string>("DeviceIPTech").ToString(),
                            ShinobiIp = ds.Tables[0].Rows[i].Field<string>("ShinobiIp").ToString(),
                            Rtsp = ds.Tables[0].Rows[i].Field<string>("Rtsp").ToString(),
                            XlsName = ds.Tables[0].Rows[i].Field<string>("XlsName").ToString()
                        };
                        Devices.Add(i, device);
                        i++;
                    }
                }
                MainForm.Connected = true;
                return Devices;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR5 - main search: " + ex.Message);
                return null;
            }
        }

        public static void SetInfoDyn(string kvfNumber, string info)
        {
            try
            {
                OpenConnection();
                string selectString = "UPDATE dbo.aishadt SET InfoDyn=@info WHERE KvfNumber LIKE @kvf;";
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                cmdgetversion.Parameters.AddWithValue("@kvf", kvfNumber);
                cmdgetversion.Parameters.AddWithValue("@info", info);
                MSSQLReader reader = cmdgetversion.ExecuteReader();
                reader.Read();
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Dictionary<int, Device> CamsbyKvfNumber(string KvfNumber)
        {
            try
            {
                Dictionary<int, Device> Devices = new Dictionary<int, Device>();
                OpenConnection();
                string selectString = $"SELECT * FROM dbo.aishadt WHERE CamOwnerKvfNumber LIKE '%{KvfNumber}%' AND DeviceType LIKE 'камера'";
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                SqlDAdapter adapter = new SqlDAdapter(selectString, conn);
                DataSet ds = new DataSet();
                ds?.Clear();
                adapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Device device = new Device
                        {
                            Code = ds.Tables[0].Rows[i].Field<string>("Code").ToString(),
                            KvfNumber = ds.Tables[0].Rows[i].Field<string>("KvfNumber").ToString(),
                            AddressDoc = ds.Tables[0].Rows[i].Field<string>("AddressDoc").ToString(),
                            AddressRGIS = ds.Tables[0].Rows[i].Field<string>("AddressRGIS").ToString(),
                            Vstrech = ds.Tables[0].Rows[i].Field<string>("Vstrech").ToString(),
                            Poput = ds.Tables[0].Rows[i].Field<string>("Poput").ToString(),
                            Dist = ds.Tables[0].Rows[i].Field<string>("Dist").ToString(),
                            Gps = ds.Tables[0].Rows[i].Field<string>("Gps").ToString(),
                            Azimut = ds.Tables[0].Rows[i].Field<Int32>("Azimut").ToString(),
                            InfoConst = ds.Tables[0].Rows[i].Field<string>("InfoConst").ToString(),
                            InfoDyn = ds.Tables[0].Rows[i].Field<string>("InfoDyn").ToString(),
                            KvfModel = ds.Tables[0].Rows[i].Field<string>("KvfModel").ToString(),
                            KvfModelCommon = ds.Tables[0].Rows[i].Field<string>("KvfModelCommon").ToString(),
                            DeviceType = ds.Tables[0].Rows[i].Field<string>("DeviceType").ToString(),
                            DeviceIP = ds.Tables[0].Rows[i].Field<string>("DeviceIP").ToString(),
                            DeviceGWIP = ds.Tables[0].Rows[i].Field<string>("DeviceGWIP").ToString(),
                            CamOwnerKvfNumber = ds.Tables[0].Rows[i].Field<string>("CamOwnerKvfNumber").ToString(),
                            CamFixType = ds.Tables[0].Rows[i].Field<string>("CamFixType").ToString(),
                            CamQuant = ds.Tables[0].Rows[i].Field<string>("CamQuant").ToString(),
                            GK = ds.Tables[0].Rows[i].Field<string>("GK").ToString(),
                            GKCommon = ds.Tables[0].Rows[i].Field<string>("GKCommon").ToString(),
                            OrgOwner = ds.Tables[0].Rows[i].Field<string>("OrgOwner").ToString(),
                            OrgOwnerCommon = ds.Tables[0].Rows[i].Field<string>("OrgOwnerCommon").ToString(),
                            PodrOrg1 = ds.Tables[0].Rows[i].Field<string>("PodrOrg1").ToString(),
                            PodrOrg1Common = ds.Tables[0].Rows[i].Field<string>("PodrOrg1Common").ToString(),
                            PodrOrg2 = ds.Tables[0].Rows[i].Field<string>("PodrOrg2").ToString(),
                            PodrOrg2Common = ds.Tables[0].Rows[i].Field<string>("PodrOrg2Common").ToString(),
                            EtherProvider = ds.Tables[0].Rows[i].Field<string>("EtherProvider").ToString(),
                            EtherProviderID = ds.Tables[0].Rows[i].Field<string>("EtherProviderID").ToString(),
                            KsmHttp = ds.Tables[0].Rows[i].Field<string>("KsmHttp").ToString(),
                            NCode = ds.Tables[0].Rows[i].Field<string>("NCode").ToString(),
                            Speed = ds.Tables[0].Rows[i].Field<Int32>("Speed").ToString(),
                            IrzIp = ds.Tables[0].Rows[i].Field<string>("IrzIp").ToString(),
                            Duplo1Ip = ds.Tables[0].Rows[i].Field<string>("Duplo1Ip").ToString(),
                            Duplo2Ip = ds.Tables[0].Rows[i].Field<string>("Duplo2Ip").ToString(),
                            DeviceIPTech = ds.Tables[0].Rows[i].Field<string>("DeviceIPTech").ToString(),
                            ShinobiIp = ds.Tables[0].Rows[i].Field<string>("ShinobiIp").ToString(),
                            Rtsp = ds.Tables[0].Rows[i].Field<string>("Rtsp").ToString(),
                            XlsName = ds.Tables[0].Rows[i].Field<string>("XlsName").ToString()
                        };
                        Devices.Add(i, device);
                        i++;
                    }
                }
                return Devices;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR2 - cams search: " + ex.Message);
                return null;
            }
        }

        public static Dictionary<int, Device> ExtrasbyKvfNumber(string KvfNumber)
        {
            try
            {
                Dictionary<int, Device> Devices = new Dictionary<int, Device>();
                OpenConnection();
                string selectString = $"SELECT * FROM dbo.aishadt WHERE CamOwnerKvfNumber LIKE '%{KvfNumber}%' AND DeviceType NOT LIKE 'некамера'";
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                SqlDAdapter adapter = new SqlDAdapter(selectString, conn);
                DataSet ds = new DataSet();
                ds?.Clear();
                adapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Device device = new Device
                        {
                            Code = ds.Tables[0].Rows[i].Field<string>("Code").ToString(),
                            KvfNumber = ds.Tables[0].Rows[i].Field<string>("KvfNumber").ToString(),
                            AddressDoc = ds.Tables[0].Rows[i].Field<string>("AddressDoc").ToString(),
                            AddressRGIS = ds.Tables[0].Rows[i].Field<string>("AddressRGIS").ToString(),
                            Vstrech = ds.Tables[0].Rows[i].Field<string>("Vstrech").ToString(),
                            Poput = ds.Tables[0].Rows[i].Field<string>("Poput").ToString(),
                            Dist = ds.Tables[0].Rows[i].Field<string>("Dist").ToString(),
                            Gps = ds.Tables[0].Rows[i].Field<string>("Gps").ToString(),
                            Azimut = ds.Tables[0].Rows[i].Field<Int32>("Azimut").ToString(),
                            InfoConst = ds.Tables[0].Rows[i].Field<string>("InfoConst").ToString(),
                            InfoDyn = ds.Tables[0].Rows[i].Field<string>("InfoDyn").ToString(),
                            KvfModel = ds.Tables[0].Rows[i].Field<string>("KvfModel").ToString(),
                            KvfModelCommon = ds.Tables[0].Rows[i].Field<string>("KvfModelCommon").ToString(),
                            DeviceType = ds.Tables[0].Rows[i].Field<string>("DeviceType").ToString(),
                            DeviceIP = ds.Tables[0].Rows[i].Field<string>("DeviceIP").ToString(),
                            DeviceGWIP = ds.Tables[0].Rows[i].Field<string>("DeviceGWIP").ToString(),
                            CamOwnerKvfNumber = ds.Tables[0].Rows[i].Field<string>("CamOwnerKvfNumber").ToString(),
                            CamFixType = ds.Tables[0].Rows[i].Field<string>("CamFixType").ToString(),
                            CamQuant = ds.Tables[0].Rows[i].Field<string>("CamQuant").ToString(),
                            GK = ds.Tables[0].Rows[i].Field<string>("GK").ToString(),
                            GKCommon = ds.Tables[0].Rows[i].Field<string>("GKCommon").ToString(),
                            OrgOwner = ds.Tables[0].Rows[i].Field<string>("OrgOwner").ToString(),
                            OrgOwnerCommon = ds.Tables[0].Rows[i].Field<string>("OrgOwnerCommon").ToString(),
                            PodrOrg1 = ds.Tables[0].Rows[i].Field<string>("PodrOrg1").ToString(),
                            PodrOrg1Common = ds.Tables[0].Rows[i].Field<string>("PodrOrg1Common").ToString(),
                            PodrOrg2 = ds.Tables[0].Rows[i].Field<string>("PodrOrg2").ToString(),
                            PodrOrg2Common = ds.Tables[0].Rows[i].Field<string>("PodrOrg2Common").ToString(),
                            EtherProvider = ds.Tables[0].Rows[i].Field<string>("EtherProvider").ToString(),
                            EtherProviderID = ds.Tables[0].Rows[i].Field<string>("EtherProviderID").ToString(),
                            KsmHttp = ds.Tables[0].Rows[i].Field<string>("KsmHttp").ToString(),
                            NCode = ds.Tables[0].Rows[i].Field<string>("NCode").ToString(),
                            Speed = ds.Tables[0].Rows[i].Field<Int32>("Speed").ToString(),
                            IrzIp = ds.Tables[0].Rows[i].Field<string>("IrzIp").ToString(),
                            Duplo1Ip = ds.Tables[0].Rows[i].Field<string>("Duplo1Ip").ToString(),
                            Duplo2Ip = ds.Tables[0].Rows[i].Field<string>("Duplo2Ip").ToString(),
                            DeviceIPTech = ds.Tables[0].Rows[i].Field<string>("DeviceIPTech").ToString(),
                            ShinobiIp = ds.Tables[0].Rows[i].Field<string>("ShinobiIp").ToString(),
                            Rtsp = ds.Tables[0].Rows[i].Field<string>("Rtsp").ToString(),
                            XlsName = ds.Tables[0].Rows[i].Field<string>("XlsName").ToString()
                        };
                        Devices.Add(i, device);
                        i++;
                    }
                }
                return Devices;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR2 - cams search: " + ex.Message);
                return null;
            }
        }

        public static Dictionary<int, string> Uniqs(string variant)
        {
            try
            {
                Dictionary<int, string> Uniqs = new Dictionary<int, string>();
                OpenConnection();
                string selectString = $"SELECT DISTINCT {variant} FROM dbo.aishadt WHERE {variant} NOT LIKE ''";
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                SqlDAdapter adapter = new SqlDAdapter(selectString, conn);
                DataSet ds = new DataSet();
                ds?.Clear();
                adapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if(variant == "Speed")
                        {
                            string var = ds.Tables[0].Rows[i].Field<Int32>($"{variant}").ToString();
                            Uniqs.Add(i, var);
                            i++;
                        }
                        else
                        {
                            string var = ds.Tables[0].Rows[i].Field<string>($"{variant}").ToString();
                            Uniqs.Add(i, var);
                            i++;
                        }
                    }
                }
                return Uniqs;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR3 - uniqs search: " + ex.Message);
                return null;
            }
        }

        private static string SelectSorted()
        {
            // Инициализируем строки для каждого параметра
            string strGKCommon = "";
            string strKvfModel = "";
            string strDeviceType = "";
            string strCamFixType = "";
            string strEtherProvider = "";
            string strPodrOrg1Common = "";
            string strPodrOrg2Common = "";
            string strNCode = "";
            string strSpeed = "";
            string strDist = "";
            string strOrgOwner = "";

            // Хранение всех элементов для каждой группы
            Dictionary<int, List<string>> allElementsByGroup = new Dictionary<int, List<string>>();
            foreach (var sortVar in MainForm.SortVars.Values)
            {
                if (!allElementsByGroup.ContainsKey(sortVar.Group))
                {
                    allElementsByGroup[sortVar.Group] = new List<string>();
                }
                allElementsByGroup[sortVar.Group].Add(sortVar.VarName);
            }

            // Проходим по каждому элементу словаря SortVars
            foreach (var sortVar in MainForm.SortVars.Values)
            {
                // Если элемент выбран (Chosen == true), добавляем его VarName в соответствующую строку
                if (sortVar.Chosen)
                {
                    switch (sortVar.Group)
                    {
                        case 0:
                            strGKCommon += $"'{sortVar.VarName}',";
                            break;
                        case 1:
                            strKvfModel += $"'{sortVar.VarName}',";
                            break;
                        case 2:
                            strDeviceType += $"'{sortVar.VarName}',";
                            break;
                        case 3:
                            strCamFixType += $"'{sortVar.VarName}',";
                            break;
                        case 4:
                            strEtherProvider += $"'{sortVar.VarName}',";
                            break;
                        case 5:
                            strPodrOrg1Common += $"'{sortVar.VarName}',";
                            break;
                        case 6:
                            strPodrOrg2Common += $"'{sortVar.VarName}',";
                            break;
                        case 7:
                            strNCode += $"'{sortVar.VarName}',";
                            break;
                        case 8:
                            strSpeed += $"'{sortVar.VarName}',";
                            break;
                        case 9:
                            strDist += $"'{sortVar.VarName}',";
                            break;
                        case 10:
                            strOrgOwner += $"'{sortVar.VarName}',";
                            break;
                    }
                }
            }

            // Проверка и добавление всех элементов группы, если нет выбранных
            strGKCommon = EnsureAllElementsIncluded(0, strGKCommon, allElementsByGroup);
            strKvfModel = EnsureAllElementsIncluded(1, strKvfModel, allElementsByGroup);
            strDeviceType = EnsureAllElementsIncluded(2, strDeviceType, allElementsByGroup);
            strCamFixType = EnsureAllElementsIncluded(3, strCamFixType, allElementsByGroup);
            strEtherProvider = EnsureAllElementsIncluded(4, strEtherProvider, allElementsByGroup);
            strPodrOrg1Common = EnsureAllElementsIncluded(5, strPodrOrg1Common, allElementsByGroup);
            strPodrOrg2Common = EnsureAllElementsIncluded(6, strPodrOrg2Common, allElementsByGroup);
            strNCode = EnsureAllElementsIncluded(7, strNCode, allElementsByGroup);
            strSpeed = EnsureAllElementsIncluded(8, strSpeed, allElementsByGroup);
            strDist = EnsureAllElementsIncluded(9, strDist, allElementsByGroup);
            strOrgOwner = EnsureAllElementsIncluded(10, strOrgOwner, allElementsByGroup);

            // Составляем окончательный запрос
            string output = "SELECT * FROM dbo.aishadt WHERE " +
                $"GKCommon IN ({strGKCommon}) AND " +
                $"KvfModel IN ({strKvfModel}) AND " +
                $"DeviceType IN ({strDeviceType}) AND " +
                $"CamFixType IN ({strCamFixType}) AND " +
                $"EtherProvider IN ({strEtherProvider}) AND " +
                $"PodrOrg1Common IN ({strPodrOrg1Common}) AND " +
                $"PodrOrg2Common IN ({strPodrOrg2Common}) AND " +
                $"NCode IN ({strNCode}) AND " +
                $"Speed IN ({strSpeed}) AND " +
                $"Dist IN ({strDist}) AND " +
                $"OrgOwner IN ({strOrgOwner})";

            return output;
        }

        // Метод для проверки и добавления всех элементов группы, если нет выбранных
        private static string EnsureAllElementsIncluded(int group, string currentStr, Dictionary<int, List<string>> allElementsByGroup)
        {
            if (string.IsNullOrEmpty(currentStr))
            {
                // Если строка пустая, добавляем все элементы группы
                if (allElementsByGroup.ContainsKey(group))
                {
                    currentStr = string.Join(",", allElementsByGroup[group].Select(e => $"'{e}'"));
                }
            }
            else
            {
                // Удаляем последнюю запятую, если она присутствует
                currentStr = RemoveLastComma(currentStr);
            }
            return currentStr;
        }

        // Вспомогательный метод для удаления последней запятой
        private static string RemoveLastComma(string input)
        {
            if (!string.IsNullOrEmpty(input) && input.EndsWith(","))
            {
                input = input.Remove(input.Length - 1);
            }
            return input;
        }


        public static Dictionary<int, Device> DevicesbySort()
        {
            try
            {
                Dictionary<int, Device> Devices = new Dictionary<int, Device>();
                OpenConnection();
                string selectString = SelectSorted();
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                SqlDAdapter adapter = new SqlDAdapter(selectString, conn);
                DataSet ds = new DataSet();
                ds?.Clear();
                adapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Device device = new Device
                        {
                            Code = ds.Tables[0].Rows[i].Field<string>("Code").ToString(),
                            KvfNumber = ds.Tables[0].Rows[i].Field<string>("KvfNumber").ToString(),
                            AddressDoc = ds.Tables[0].Rows[i].Field<string>("AddressDoc").ToString(),
                            AddressRGIS = ds.Tables[0].Rows[i].Field<string>("AddressRGIS").ToString(),
                            Vstrech = ds.Tables[0].Rows[i].Field<string>("Vstrech").ToString(),
                            Poput = ds.Tables[0].Rows[i].Field<string>("Poput").ToString(),
                            Dist = ds.Tables[0].Rows[i].Field<string>("Dist").ToString(),
                            Gps = ds.Tables[0].Rows[i].Field<string>("Gps").ToString(),
                            Azimut = ds.Tables[0].Rows[i].Field<Int32>("Azimut").ToString(),
                            InfoConst = ds.Tables[0].Rows[i].Field<string>("InfoConst").ToString(),
                            InfoDyn = ds.Tables[0].Rows[i].Field<string>("InfoDyn").ToString(),
                            KvfModel = ds.Tables[0].Rows[i].Field<string>("KvfModel").ToString(),
                            KvfModelCommon = ds.Tables[0].Rows[i].Field<string>("KvfModelCommon").ToString(),
                            DeviceType = ds.Tables[0].Rows[i].Field<string>("DeviceType").ToString(),
                            DeviceIP = ds.Tables[0].Rows[i].Field<string>("DeviceIP").ToString(),
                            DeviceGWIP = ds.Tables[0].Rows[i].Field<string>("DeviceGWIP").ToString(),
                            CamOwnerKvfNumber = ds.Tables[0].Rows[i].Field<string>("CamOwnerKvfNumber").ToString(),
                            CamFixType = ds.Tables[0].Rows[i].Field<string>("CamFixType").ToString(),
                            CamQuant = ds.Tables[0].Rows[i].Field<string>("CamQuant").ToString(),
                            GK = ds.Tables[0].Rows[i].Field<string>("GK").ToString(),
                            GKCommon = ds.Tables[0].Rows[i].Field<string>("GKCommon").ToString(),
                            OrgOwner = ds.Tables[0].Rows[i].Field<string>("OrgOwner").ToString(),
                            OrgOwnerCommon = ds.Tables[0].Rows[i].Field<string>("OrgOwnerCommon").ToString(),
                            PodrOrg1 = ds.Tables[0].Rows[i].Field<string>("PodrOrg1").ToString(),
                            PodrOrg1Common = ds.Tables[0].Rows[i].Field<string>("PodrOrg1Common").ToString(),
                            PodrOrg2 = ds.Tables[0].Rows[i].Field<string>("PodrOrg2").ToString(),
                            PodrOrg2Common = ds.Tables[0].Rows[i].Field<string>("PodrOrg2Common").ToString(),
                            EtherProvider = ds.Tables[0].Rows[i].Field<string>("EtherProvider").ToString(),
                            EtherProviderID = ds.Tables[0].Rows[i].Field<string>("EtherProviderID").ToString(),
                            KsmHttp = ds.Tables[0].Rows[i].Field<string>("KsmHttp").ToString(),
                            NCode = ds.Tables[0].Rows[i].Field<string>("NCode").ToString(),
                            Speed = ds.Tables[0].Rows[i].Field<Int32>("Speed").ToString(),
                            IrzIp = ds.Tables[0].Rows[i].Field<string>("IrzIp").ToString(),
                            Duplo1Ip = ds.Tables[0].Rows[i].Field<string>("Duplo1Ip").ToString(),
                            Duplo2Ip = ds.Tables[0].Rows[i].Field<string>("Duplo2Ip").ToString(),
                            DeviceIPTech = ds.Tables[0].Rows[i].Field<string>("DeviceIPTech").ToString(),
                            ShinobiIp = ds.Tables[0].Rows[i].Field<string>("ShinobiIp").ToString(),
                            Rtsp = ds.Tables[0].Rows[i].Field<string>("Rtsp").ToString(),
                            XlsName = ds.Tables[0].Rows[i].Field<string>("XlsName").ToString()
                        };
                        Devices.Add(i, device);
                        i++;
                    }
                }
                return Devices;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR4 - sorted search: " + ex.Message);
                return null;
            }
        }

    }
}
