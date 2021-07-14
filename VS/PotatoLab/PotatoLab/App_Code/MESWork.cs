using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using System.Text;

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
        public string SRNo { get; set; }
        public string SRTitle { get; set; }
        public string SRLink { get; set; }
        public string DateIssue { get; set; }
        public string DateDue { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string DateClose { get; set; }
        public string ITOwner { get; set; }
        public string User1 { get; set; }
        public string User2 { get; set; }
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
        public string Type3 { get; set; }
        public int Weight { get; set; }

        public bool SaveToFile()
        {
            return true;
        }
        public bool SaveToDB(out string result)
        {
            string sql = "";
            
            if (WorkID != "")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update xx.xxxxx set ");
                //-------------------------------------
                sb.Append(string.Format("WORK_NAME='{0}', ", WorkName));
                sb.Append(string.Format("WORK_DETAIL='{0}', ", WorkDetail));
                sb.Append(string.Format("WORK_NOTE='{0}', ", WorkNote));
                sb.Append(string.Format("STATUS='{0}', ", Status));
                sb.Append(string.Format("SR_NO='{0}', ", SRNo));
                sb.Append(string.Format("SR_TITLE='{0}', ", SRTitle));
                sb.Append(string.Format("SR_LINK='{0}', ", SRLink));
                sb.Append(string.Format("ISSUE_DATE='{0}', ", DateIssue));
                sb.Append(string.Format("DUE_DATE='{0}', ", DateDue));
                sb.Append(string.Format("START_DATE='{0}', ", DateStart));
                sb.Append(string.Format("END_DATE='{0}', ", DateEnd));
                sb.Append(string.Format("CLOSE_DATE='{0}', ", DateClose));
                sb.Append(string.Format("IT_OWNER='{0}', ", ITOwner));
                sb.Append(string.Format("USER1='{0}', ", User1));
                sb.Append(string.Format("USER2='{0}', ", User2));
                sb.Append(string.Format("CIM_NOTE='{0}', ", NoteCIM));
                sb.Append(string.Format("IT_NOTE='{0}', ", NoteIT));
                sb.Append(string.Format("BENEFIT='{0}', ", Benefit));
                sb.Append(string.Format("FAC='{0}', ", Fac));
                sb.Append(string.Format("OPER='{0}', ", Oper));
                sb.Append(string.Format("CUST3='{0}', ", Cust3));

                sb.Append(string.Format("TYPE1='{0}', ", Type1));
                sb.Append(string.Format("TYPE2='{0}', ", Type2));
                sb.Append(string.Format("TYPE3='{0}', ", Type3));

                sb.Append(string.Format("WEIGHT='{0}' ", Weight));
                //-------------------------------------
                sb.Append(string.Format("where WORK_ID='{0}'", WorkID));
                //-------------------------------------
                result = "OK";
            }
            else
            {
                //檢查資料重複
                sql = string.Format("select * from XXXX.XXXX where SR_NO='{0}'", SRNo);

                StringBuilder sb = new StringBuilder();
                sb.Append("insert into XXXX.XXXX(");
                sb.Append("WORK_ID, ");
                sb.Append("WORK_NAME, WORK_DETAIL, WORK_NOTE, ");
                sb.Append("STATUS, ");
                sb.Append("SR_NO, SR_TITLE, SR_LINK, ");
                sb.Append("ISSUE_DATE, DUE_DATE, START_DATE, END_DATE, CLOSE_DATE, ");
                sb.Append("IT_OWNER, USER1, USER2, ");
                sb.Append("CIM_NOTE, IT_NOTE, BENEFIT, ");
                sb.Append("FAC, OPER, CUST3, ");
                sb.Append("TYPE1, TYPE2, TYPE3, ");
                sb.Append("WEIGHT ");
                sb.Append(") values(");
                sb.Append(string.Format("'{0}', ", WorkID));
                sb.Append(string.Format("'{0}', ", WorkName)); sb.Append(string.Format("'{0}', ", WorkDetail)); sb.Append(string.Format("'{0}', ", WorkNote));
                sb.Append(string.Format("'{0}', ", Status));
                sb.Append(string.Format("'{0}', ", SRNo)); sb.Append(string.Format("'{0}', ", SRTitle)); sb.Append(string.Format("'{0}', ", SRLink));
                sb.Append(string.Format("'{0}', ", DateIssue)); sb.Append(string.Format("'{0}', ", DateDue)); sb.Append(string.Format("'{0}', ", DateStart)); sb.Append(string.Format("'{0}', ", DateEnd)); sb.Append(string.Format("'{0}', ", DateClose));
                sb.Append(string.Format("'{0}', ", ITOwner)); sb.Append(string.Format("'{0}', ", User1)); sb.Append(string.Format("'{0}', ", User2));
                sb.Append(string.Format("'{0}', ", NoteCIM)); sb.Append(string.Format("'{0}', ", NoteIT)); sb.Append(string.Format("'{0}', ", Benefit));
                sb.Append(string.Format("'{0}', ", Fac)); sb.Append(string.Format("'{0}', ", Oper)); sb.Append(string.Format("'{0}', ", Cust3));
                sb.Append(string.Format("'{0}', ", Type1)); sb.Append(string.Format("'{0}', ", Type2)); sb.Append(string.Format("'{0}', ", Type3));
                sb.Append(string.Format("'{0}' ", Weight));
                sb.Append(")");
                //-------------------------------------
                result = "OK";
            }
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
