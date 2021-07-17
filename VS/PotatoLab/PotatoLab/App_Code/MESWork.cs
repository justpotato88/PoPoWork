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

        #region 靜態 Function -----------------------------------------------------------------
        
        public static bool UpdateStatus(string workID, out string result, string status, string startDate, string endDate, string closeDate, string dueDate)
        {
            result = "OK";
            return true;
        }

        //Note 可以放會議紀錄之類的 itNote 放 SQL
        public static bool UpdateWorkInfo(string workID, out string result, string workName, string workDetail, string workNote, string itNote)
        {
            result = "OK";
            return true;
        }

        public static bool UpdateCIMNote(string workID, out string result, string noteText)
        {
            result = "OK";
            return true;
        }

        public static string ReadTextFile(string filePath)
        {
            return "XXXXX";
        }
        public static bool SaveTextFile(out string result, string filePath, string saveHtml)
        {
            result = "OK";
            return true;
        }
        #endregion ----------------------------------------------------------------------------

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

        //public static List<MESWork> DapperMapping(string startDate, string endDate, string key, string userID, string status, string type)
        public static List<MESWork> GetWorkList(string startDate, string endDate, string key, string userID, string status, string type1, string type2, string type3, string oper, string cust3, int minWeight)
        {
            return GetSampleData();
            List<MESWork> result = new List<MESWork>();
            try
            {

                using (IDbConnection db = new SqlConnection(constr))
                {
                    db.Execute("SELECT ID FROM dbo.TEST_TABLE");
                    result = db.Query<MESWork>("SELECT ID FROM dbo.TEST_TABLE").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public static List<MESWork> GetUrgentWorkList(int remindDay)
        {
            return GetSampleData();
            List<MESWork> result = new List<MESWork>();
            try
            {

                using (IDbConnection db = new SqlConnection(constr))
                {
                    db.Execute("SELECT ID FROM dbo.TEST_TABLE");
                    result = db.Query<MESWork>("SELECT ID FROM dbo.TEST_TABLE").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public static List<MESWork> GetSampleData()
        {
            #region Test -------------------------------
            List<PotatoLab.MESWork> xx = new List<MESWork>();
            PotatoLab.MESWork tmpWork = new MESWork();
            tmpWork.WorkID = "001";
            tmpWork.WorkName = "客戶 XD123 需求，這邊那邊都要通通包起來";
            tmpWork.WorkDetail = "1.WORKDETAIL<br>2.SSFSGSGSGSFSFSFF";
            tmpWork.Benefit = "每天要三個人出手，要花1000000元";

            tmpWork.ITOwner = "Potato";
            tmpWork.User1 = "Abcd Huang";
            tmpWork.User2 = "Jc Lee";

            tmpWork.Type1 = "課程認證";
            tmpWork.Type2 = "PE";
            tmpWork.Type3 = "RMS";
            tmpWork.Weight = 1;

            tmpWork.Status = "Wait-Assign";
            tmpWork.SRNo = "SR-123456875";
            tmpWork.SRTitle = "XD123-SRTITLEssssssssss";
            tmpWork.SRLink = "http://";

            tmpWork.Fac = "ASE07";
            tmpWork.Oper = "3800";
            tmpWork.Cust3 = "MVL";

            tmpWork.DateIssue = "2019-01-01";
            tmpWork.DateStart = "2019-01-02";
            tmpWork.DateEnd = "2019-01-03";
            tmpWork.DateDue = "2019-01-04";
            tmpWork.DateClose = "2019-01-05";
            xx.Add(tmpWork);
            //----------------------------------
            tmpWork = new MESWork();
            tmpWork.WorkID = "002";
            tmpWork.WorkName = "BBBBBBB";
            tmpWork.SRNo = "sr_002";
            xx.Add(tmpWork);
            //----------------------------------
            tmpWork = new MESWork();
            tmpWork.WorkID = "003";
            tmpWork.WorkName = "CCCCCC";
            tmpWork.SRNo = "sr_003";
            xx.Add(tmpWork);

            tmpWork = new MESWork();
            tmpWork.WorkID = "004";
            tmpWork.WorkName = "DDDDDD";
            tmpWork.SRNo = "sr_004";
            xx.Add(tmpWork);

            tmpWork = new MESWork();
            tmpWork.WorkID = "005";
            tmpWork.WorkName = "EEEEEEE";
            xx.Add(tmpWork);

            tmpWork = new MESWork();
            tmpWork.WorkID = "006";
            tmpWork.WorkName = "FFFFFF";
            xx.Add(tmpWork);

            return xx;
            #endregion ---------------------------------
        }
    }
}
