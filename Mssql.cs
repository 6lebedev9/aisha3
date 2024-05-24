using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aisha3
{
    public class Mssql
    {
        public static string mssqldb = Properties.Settings.Default.mssqldb;
        public static string mssqlcatalog = Properties.Settings.Default.mssqlcatalog;
        public static string mssqluser = Properties.Settings.Default.mssqluser;
        public static string mssqlpass = Properties.Settings.Default.mssqlpass;
        public SqlConnection conn = new SqlConnection($"Data Source={mssqldb}; Initial Catalog={mssqlcatalog};" +
            $" User ID={mssqluser}; Password={mssqlpass}");
    }
}
