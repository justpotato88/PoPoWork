using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace PotatoLab
{
    public class DB
    {
        public class PDB 
        {
            public static DataTable GetDataTable(string sql)
            {
                return new DataTable();
            }

            public static void ExecuteNonQuery(string sql)
            {
                
            }

            public static Oracle.ManagedDataAccess.Client.OracleConnection GetConnection()
            {
                return new Oracle.ManagedDataAccess.Client.OracleConnection();
            }
        }
    }
}
