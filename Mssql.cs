using Microsoft.Data.SqlClient;
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
                string selectString = "SELECT version FROM dbo.aishaversions WHERE comment LIKE @comment;";
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                cmdgetversion.Parameters.AddWithValue("@comment", "last");
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
        public static Dictionary<int, Device> DevicesbyString(string code)
        {
            try
            {
                Dictionary<int, Device> Devices = new Dictionary<int, Device>();
                OpenConnection();
                string selectString = $"SELECT * FROM dbo.aishadt WHERE KvfNumber LIKE '%{code}%' AND DeviceType NOT LIKE 'камера'";
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
                Console.WriteLine("ERROR1 - main search: " + ex.Message);
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
    }
}
