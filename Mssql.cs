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
            $" User ID={mssqluser}; Password={mssqlpass}; Integrated Security=False;TrustServerCertificate=True");

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
