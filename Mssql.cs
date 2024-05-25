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

namespace aisha3
{
    public class Mssql
    {
        public static bool Connected = false;
        public static string mssqldb = Properties.Settings.Default.mssqldb;
        public static string mssqlcatalog = Properties.Settings.Default.mssqlcatalog;
        public static string mssqluser = Properties.Settings.Default.mssqluser;
        public static string mssqlpass = Properties.Settings.Default.mssqlpass;
        public static Microsoft.Data.SqlClient.SqlConnection conn = 
            new Microsoft.Data.SqlClient.SqlConnection
            ($"Data Source={mssqldb}; Initial Catalog={mssqlcatalog};" +
            $" User ID={mssqluser}; Password={mssqlpass}; Integrated Security=True;TrustServerCertificate=True");

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

        public static Dictionary<int, Device> DevicesbyCode(string code)
        {
            try
            {
                Dictionary<int, Device> Devices = new Dictionary<int, Device>();
                Device device = new Device();
                OpenConnection();
                string selectString = "SELECT * FROM dbo.aishadt WHERE Code LIKE @var1;";
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                cmdgetversion.Parameters.AddWithValue("@var1", code);
                MSSQLReader reader = cmdgetversion.ExecuteReader();
                using (reader)
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        device.Code = reader.GetValue(1).ToString();
                        Devices.Add(i, device);
                        i++;
                    }
                }
                return Devices;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static Dictionary<int, Device> DevicesbyString(string code)
        {
            try
            {
                Dictionary<int, Device> Devices = new Dictionary<int, Device>();
                Device device = new Device();
                OpenConnection();
                string selectString = $"SELECT * FROM dbo.aishadt WHERE KvfNumber LIKE '%{code}%' AND DeviceType NOT LIKE 'камера'";
                MSSQLCmd cmdgetversion = new MSSQLCmd(selectString, conn);
                //cmdgetversion.Parameters.AddWithValue("@var1", code);
                MSSQLReader reader = cmdgetversion.ExecuteReader();
                using (reader)
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        device.Code = reader.GetValue(0).ToString();
                        device.KvfNumber = reader.GetValue(1).ToString();
                        device.AddressDoc = reader.GetValue(2).ToString();
                        device.AddressRGIS = reader.GetValue(3).ToString();
                        device.Vstrech = reader.GetValue(4).ToString();
                        device.Poput = reader.GetValue(5).ToString();
                        device.Dist = reader.GetValue(6).ToString();
                        device.Gps = reader.GetValue(7).ToString();
                        device.Azimut = reader.GetValue(8).ToString();
                        device.InfoConst = reader.GetValue(9).ToString();
                        device.InfoDyn = reader.GetValue(10).ToString();
                        device.KvfModel = reader.GetValue(11).ToString();
                        device.KvfModelCommon = reader.GetValue(12).ToString();
                        device.DeviceType = reader.GetValue(13).ToString();
                        device.DeviceIP = reader.GetValue(14).ToString();
                        device.DeviceGWIP = reader.GetValue(15).ToString();
                        device.CamOwnerKvfNumber = reader.GetValue(16).ToString();
                        device.CamFixType = reader.GetValue(17).ToString();
                        device.CamQuant = reader.GetValue(18).ToString();
                        device.GK = reader.GetValue(19).ToString();
                        device.GKCommon = reader.GetValue(20).ToString();
                        device.OrgOwner = reader.GetValue(21).ToString();
                        device.OrgOwnerCommon = reader.GetValue(22).ToString();
                        device.PodrOrg1 = reader.GetValue(23).ToString();
                        device.PodrOrg1Common = reader.GetValue(24).ToString();
                        device.PodrOrg2 = reader.GetValue(25).ToString();
                        device.PodrOrg2Common = reader.GetValue(26).ToString();
                        device.EtherProvider = reader.GetValue(27).ToString();
                        device.EtherProviderID = reader.GetValue(28).ToString();
                        device.KsmHttp = reader.GetValue(29).ToString();
                        device.NCode = reader.GetValue(30).ToString();
                        device.Speed = reader.GetValue(31).ToString();
                        device.IrzIp = reader.GetValue(32).ToString();
                        device.Duplo1Ip = reader.GetValue(33).ToString();
                        device.Duplo2Ip = reader.GetValue(34).ToString();
                        device.DeviceIPTech = reader.GetValue(35).ToString();
                        device.ShinobiIp = reader.GetValue(36).ToString();
                        device.Rtsp = reader.GetValue(37).ToString();
                        device.XlsName = reader.GetValue(38).ToString();
                        Devices.Add(i, device);
                        i++;
                    }
                }
                return Devices;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
