using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PotatoLab
{
    public class MESWork
    {
        //const string constr = "server=.\SQLExpress;Integrated Security=SSPI;database=PoPoDB;";
        //--

        //const string constr = "Data Source=(local);Initial Catalog=PoPoDB;Integrated Security=true";
        const string constr = @"server=.\SQLEXPRESS01;Integrated Security=SSPI;database=PoPoDB;";

        public string ID { get; set; }
        public string WorkID { get; set; }
        public string WorkName { get; set; }
        public string SRNo { get; set; }
        public string SRTitle { get; set; }

        public bool SaveToFile()
        {
            return true;
        }
        public bool SaveToDB()
        {
            return true;
        }

        public static List<MESWork> DapperMapping()
        {
            List<MESWork> result = new List<MESWork>();
            try
            {
                
                using (IDbConnection db = new SqlConnection(constr))
                {
                    db.Execute("SELECT ID FROM dbo.TEST_TABLE");
                    result = db.Query<MESWork>("SELECT ID FROM dbo.TEST_TABLE").ToList();
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
