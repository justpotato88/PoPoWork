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

        public string WorkID { get; set; }
        public string WorkName { get; set; }
        public string WorkDetail { get; set; }
        public string WorkNote { get; set; }
        public string Status { get; set; }
        public string CloseDate { get; set; }
        public string SRNo { get; set; }
        public string SRTitle { get; set; }
        public string SRLink { get; set; }
        public string DateStart { get; set; }
        public string DateEdn { get; set; }
        public string DateDue { get; set; }
        public string OwnerIT { get; set; }
        public string OwnerCIM { get; set; }
        public string OwnerUser { get; set; }
        public string NoteCIM { get; set; }
        public string NoteIT { get; set; }
        public string Benefit { get; set; }
        public string Fac { get; set; }
        public string Oper { get; set; }
        public string Cust3 { get; set; }
        
        /// <summary>
        /// 程式、設定、資料、RMS
        /// </summary>
        public string Type1 { get; set; }
        /// <summary>
        /// CAB、PE、EE、MFG、QA、PC、GMO-IE
        /// </summary>
        public string Type2 { get; set; }
        public string Score { get; set; }

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
