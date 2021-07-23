
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
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

        public string WORK_ID { get; set; }
        public string WORK_NAME { get; set; }
        public string WORK_DETAIL { get; set; }
        public string WORK_NOTE { get; set; }
        public string STATUS { get; set; }
        public string SR_NO { get; set; }
        public string SR_TITLE { get; set; }
        public string SR_LINK { get; set; }
        public string ISSUE_DATE { get; set; }
        public string DUE_DATE { get; set; }
        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
        public string CLOSE_DATE { get; set; }
        public string IT_OWNER { get; set; }
        public string IT_OWNER_NAME { get; set; }
        public string USER1 { get; set; }
        public string USER1_NAME { get; set; }
        public string USER2 { get; set; }
        public string USER2_NAME { get; set; }
        public string CIM_NOTE { get; set; }
        public string IT_NOTE { get; set; }
        public string BENEFIT { get; set; }
        public string FAC { get; set; }
        public string OPER { get; set; }
        public string CUST3 { get; set; }
        
        /// <summary>
        /// 程式、設定、資料、RMS
        /// </summary>
        public string TYPE1 { get; set; }
        /// <summary>
        /// CAB、PE、EE、MFG、QA、PC、GMO-IE
        /// </summary>
        public string TYPE2 { get; set; }
        public string TYPE3 { get; set; }
        public int WEIGHT { get; set; }

        //剩餘天數
        public int REMAIN_DATE { get; set; }
        

        public bool SaveToFile()
        {
            return true;
        }
        public bool SaveToDB(out string result)
        {
            string sql = "";
            
            if (WORK_ID != "")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("update fwpdb.MES_WORK_LIST set ");
                //-------------------------------------
                sb.Append(string.Format("WORK_NAME='{0}', ", WORK_NAME.Replace("'", "''")));
                sb.Append(string.Format("WORK_DETAIL='{0}', ", WORK_DETAIL.Replace("'", "''")));
                sb.Append(string.Format("WORK_NOTE='{0}', ", WORK_NOTE.Replace("'", "''")));
                sb.Append(string.Format("STATUS='{0}', ", STATUS));
                sb.Append(string.Format("SR_NO='{0}', ", SR_NO.ToUpper()));
                sb.Append(string.Format("SR_TITLE='{0}', ", SR_TITLE));
                sb.Append(string.Format("SR_LINK='{0}', ", SR_LINK));
                sb.Append(string.Format("ISSUE_DATE='{0}', ", ISSUE_DATE));
                sb.Append(string.Format("DUE_DATE='{0}', ", DUE_DATE));
                sb.Append(string.Format("START_DATE='{0}', ", START_DATE));
                sb.Append(string.Format("END_DATE='{0}', ", END_DATE));
                sb.Append(string.Format("CLOSE_DATE='{0}', ", CLOSE_DATE));
                sb.Append(string.Format("IT_OWNER='{0}', ", IT_OWNER.ToUpper()));
                sb.Append(string.Format("USER1='{0}', ", USER1.ToUpper()));
                sb.Append(string.Format("USER2='{0}', ", USER2.ToUpper()));
                sb.Append(string.Format("CIM_NOTE='{0}', ", CIM_NOTE.Replace("'", "''")));
                sb.Append(string.Format("IT_NOTE='{0}', ", IT_NOTE.Replace("'", "''")));
                sb.Append(string.Format("BENEFIT='{0}', ", BENEFIT.Replace("'", "''")));
                sb.Append(string.Format("FAC='{0}', ", FAC));
                sb.Append(string.Format("OPER='{0}', ", OPER));
                sb.Append(string.Format("CUST3='{0}', ", CUST3.ToUpper()));

                sb.Append(string.Format("TYPE1='{0}', ", TYPE1));
                sb.Append(string.Format("TYPE2='{0}', ", TYPE2));
                sb.Append(string.Format("TYPE3='{0}', ", TYPE3));

                sb.Append(string.Format("WEIGHT='{0}' ", WEIGHT));
                //-------------------------------------
                sb.Append(string.Format("where WORK_ID='{0}'", WORK_ID));
                //-------------------------------------
                DB.PDB.ExecuteNonQuery(sb.ToString());
                result = "OK";
            }
            else
            {
                //檢查資料重複
                sql = string.Format("select * from fwpdb.MES_WORK_LIST where SR_NO='{0}'", SR_NO);

                StringBuilder sb = new StringBuilder();
                sb.Append("insert into fwpdb.MES_WORK_LIST(");
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
                sb.Append(string.Format("'{0}', ", WORK_ID));
                sb.Append(string.Format("'{0}', ", WORK_NAME.Replace("'", "''"))); sb.Append(string.Format("'{0}', ", WORK_DETAIL.Replace("'", "''"))); sb.Append(string.Format("'{0}', ", WORK_NOTE.Replace("'", "''")));
                sb.Append(string.Format("'{0}', ", STATUS));
                sb.Append(string.Format("'{0}', ", SR_NO)); sb.Append(string.Format("'{0}', ", SR_TITLE)); sb.Append(string.Format("'{0}', ", SR_LINK));
                sb.Append(string.Format("'{0}', ", ISSUE_DATE)); sb.Append(string.Format("'{0}', ", DUE_DATE)); sb.Append(string.Format("'{0}', ", START_DATE)); sb.Append(string.Format("'{0}', ", END_DATE)); sb.Append(string.Format("'{0}', ", CLOSE_DATE));
                sb.Append(string.Format("'{0}', ", IT_OWNER)); sb.Append(string.Format("'{0}', ", USER1)); sb.Append(string.Format("'{0}', ", USER2));
                sb.Append(string.Format("'{0}', ", CIM_NOTE.Replace("'", "''"))); sb.Append(string.Format("'{0}', ", IT_NOTE.Replace("'", "''"))); sb.Append(string.Format("'{0}', ", BENEFIT.Replace("'", "''")));
                sb.Append(string.Format("'{0}', ", FAC)); sb.Append(string.Format("'{0}', ", OPER)); sb.Append(string.Format("'{0}', ", CUST3));
                sb.Append(string.Format("'{0}', ", TYPE1)); sb.Append(string.Format("'{0}', ", TYPE2)); sb.Append(string.Format("'{0}', ", TYPE3));
                sb.Append(string.Format("'{0}' ", WEIGHT));
                sb.Append(")");
                //-------------------------------------
                DB.PDB.ExecuteNonQuery(sb.ToString());
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

        public static MESWork GetWorkData(string workID)
        {
            List<MESWork> result = new List<MESWork>();
            try
            {
                using (IDbConnection db = DB.PDB.GetConnection())
                {
                    //db.Execute("SELECT ID FROM fwpdb.MES_WORK_LIST");
                    result = db.Query<MESWork>("SELECT * FROM fwpdb.MES_WORK_LIST where WORK_ID='" + workID + "'").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (result.Count > 0)
                return result[0];
            else
                return new MESWork();
        }

        //public static List<MESWork> DapperMapping(string startDate, string endDate, string key, string userID, string status, string type)
        public static List<MESWork> GetWorkList(string startDate, string endDate, string key, string userID, string status, string type1, string type2, string type3, string oper, string cust3, int minWeight, string srNo)
        {
            return GetSampleData();
            List<MESWork> result = new List<MESWork>();
            try
            {
                StringBuilder sb = new StringBuilder();

                if (startDate.Length > 0)
                    sb.Append(string.Format(" and (DUE_DATE >= '{0}' or CLOSE_DATE >= '{0}') ", startDate));
                if (endDate.Length > 0)
                    sb.Append(string.Format(" and (ISSUE_DATE <= '{0}') ", endDate));
                if (key.Length > 0)
                    sb.Append(string.Format(" and (upper(WORK_TITLE) like '%{0}%' or upper(WORK_DETAIL) like '%{0}%' or upper(WORK_NOTE) like '%{0}%') ", key));
                if (userID.Length > 0)
                    sb.Append(string.Format(" and (IT_OWNER='{0}' or USER1='{0}' or USER2='{0}') ", userID));
                if (status.Length > 0)
                    sb.Append(string.Format(" and STATUS in ('{0}') ", status.Replace(",", "','")));
                if (type1.Length > 0)
                    sb.Append(string.Format(" and TYPE1 in ('{0}') ", type1.Replace(",", "','")));
                if (type2.Length > 0)
                    sb.Append(string.Format(" and upper(TYPE2) like '%{0}%' ", type2.ToUpper()));
                if (type3.Length > 0)
                    sb.Append(string.Format(" and upper(TYPE3) like '%{0}%' ", type3.ToUpper()));
                if (oper.Length > 0)
                    sb.Append(string.Format(" and OPER like '%{0}%' ", oper));
                if (cust3.Length > 0)
                    sb.Append(string.Format(" and CUST3 like '%{0}%' ", cust3));
                if (minWeight >= 0)
                    sb.Append(string.Format(" and WEIGHT >= {0} ", minWeight));
                if(srNo.Length > 0)
                    sb.Append(string.Format(" and SR_NO like '{0}%' ", srNo));

                string sql = @"SELECT a.* 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.IT_OWNER) as IT_OWNER_NAME, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.USER1) as USER1_NAME, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.USER2) as USER2_NAME
                               FROM fwpdb.MES_WORK_LIST a where 1=1";
                sql += sb.ToString();
                using (IDbConnection db = DB.PDB.GetConnection())
                {
                    result = db.Query<MESWork>(sql).ToList();
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
                string sql = @"SELECT a.* 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.IT_OWNER) as IT_OWNER_NAME, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.USER1) as USER1_NAME, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.USER2) as USER2_NAME
                               FROM fwpdb.MES_WORK_LIST a where DUE_DATE < to_char(sysdate+14,'yyyy-mm-dd') and Status not in ('Close')";
                using (IDbConnection db = DB.PDB.GetConnection())
                {
                    result = db.Query<MESWork>(sql).ToList();
                }
                //using (IDbConnection db = DB.PDB.GetConnection())
                //{
                //    //db.Execute("SELECT ID FROM fwpdb.MES_WORK_LIST");
                //    result = db.Query<MESWork>("SELECT * FROM fwpdb.MES_WORK_LIST where DUE_DATE < to_char(sysdate+8,'yyyy-mm-dd') and Status not in ('Close')").ToList();
                //}
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
            tmpWork.WORK_ID = "001";
            tmpWork.WORK_NAME = "客戶 XD123 需求，這邊那邊都要通通包起來";
            tmpWork.WORK_DETAIL = "1.WORKDETAIL<br>2.SSFSGSGSGSFSFSFF";
            tmpWork.BENEFIT = "每天要三個人出手，要花1000000元";

            tmpWork.IT_OWNER = "Potato";
            tmpWork.IT_OWNER_NAME = "Potato";
            tmpWork.USER1 = "Abcd Huang";
            tmpWork.USER1_NAME = "Abcd Huang";
            tmpWork.USER2 = "Jc Lee";
            tmpWork.USER2_NAME = "Jc Lee";

            tmpWork.TYPE1 = "課程認證";
            tmpWork.TYPE2 = "PE";
            tmpWork.TYPE3 = "RMS";
            tmpWork.WEIGHT = 1;

            tmpWork.STATUS = "Wait-Assign";
            tmpWork.SR_NO = "SR-123456875";
            tmpWork.SR_TITLE = "XD123-SRTITLEssssssssss";
            tmpWork.SR_LINK = "http://";

            tmpWork.FAC = "ASE07";
            tmpWork.OPER = "3800";
            tmpWork.CUST3 = "MVL";

            tmpWork.ISSUE_DATE = "2019-01-01";
            tmpWork.START_DATE = "2019-01-02";
            tmpWork.END_DATE = "2019-01-03";
            tmpWork.DUE_DATE = "2019-01-04";
            tmpWork.CLOSE_DATE = "2019-01-05";
            xx.Add(tmpWork);
            //----------------------------------
            tmpWork = new MESWork();
            tmpWork.WORK_ID = "002";
            tmpWork.WORK_NAME = "BBBBBBB";
            tmpWork.SR_NO = "sr_002";
            xx.Add(tmpWork);
            //----------------------------------
            tmpWork = new MESWork();
            tmpWork.WORK_ID = "003";
            tmpWork.WORK_NAME = "CCCCCC";
            tmpWork.SR_NO = "sr_003";
            xx.Add(tmpWork);

            tmpWork = new MESWork();
            tmpWork.WORK_ID = "004";
            tmpWork.WORK_NAME = "DDDDDD";
            tmpWork.SR_NO = "sr_004";
            xx.Add(tmpWork);

            tmpWork = new MESWork();
            tmpWork.WORK_ID = "005";
            tmpWork.WORK_NAME = "EEEEEEE";
            xx.Add(tmpWork);

            tmpWork = new MESWork();
            tmpWork.WORK_ID = "006";
            tmpWork.WORK_NAME = "FFFFFF";
            xx.Add(tmpWork);

            return xx;
            #endregion ---------------------------------
        }
    }
}
