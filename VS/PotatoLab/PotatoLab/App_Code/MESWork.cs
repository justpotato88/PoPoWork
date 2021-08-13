
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
        //const string constr = @"server=.\SQLEXPRESS01;Integrated Security=SSPI;database=PoPoDB;";

        public string WORK_ID { get; set; }
        public string WORK_NAME { get; set; }
        private string _workDetail = "";
        public string WORK_DETAIL { get { return _workDetail ?? ""; } set { _workDetail = value; } }
        private string _workNote = "";
        public string WORK_NOTE { get { return _workNote ?? ""; } set { _workNote = value; } }
        public string STATUS { get; set; }
        private string _srNo = "";
        public string SR_NO { get { return _srNo ?? ""; } set { _srNo = value; } }
        private string _srTitle = "";
        public string SR_TITLE { get { return _srTitle ?? ""; } set { _srTitle = value; } }
        public string SR_LINK { get; set; }
        public string ISSUE_DATE { get; set; }
        public string ISSUE_DATE_SHORT {
            get {
                if (ISSUE_DATE.Length > 5)
                    return ISSUE_DATE.Substring(5);
                else
                    return ISSUE_DATE;
            }
        }
        public string DUE_DATE { get; set; }
        public string DUE_DATE_SHORT
        {
            get
            {
                if (DUE_DATE.Length > 5)
                    return DUE_DATE.Substring(5);
                else
                    return DUE_DATE;
            }
        }
        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
        public string CLOSE_DATE { get; set; }
        private string _itOwner = "";
        public string IT_OWNER { get { return _itOwner ?? ""; } set { _itOwner = value; } }
        public string IT_OWNER_NAME { get; set; }
        private string _itUser1 = "";
        public string USER1 { get { return _itUser1 ?? ""; } set { _itUser1 = value; } }
        public string USER1_NAME { get; set; }
        private string _itUser2 = "";
        public string USER2 { get { return _itUser2 ?? ""; } set { _itUser2 = value; } }
        public string USER2_NAME { get; set; }
        
        private string _itNote = "";
        public string IT_NOTE { get { return _itNote ?? ""; } set { _itNote = value; } }
        private string _cimNote = "";
        public string CIM_NOTE { get { return _cimNote ?? ""; } set { _cimNote = value; } }
        private string _benefit = "";
        public string BENEFIT { get { return _benefit ?? ""; } set { _benefit = value; } }
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
        private int _remainDay = 1;
        public int REMAIN_DAY { get; set; }
        

        public bool SaveToFile()
        {
            return true;
        }
        public bool SaveToDB(out string result)
        {
            string sql = "";

            try
            {
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
                    //sql = string.Format("select * from fwpdb.MES_WORK_LIST where SR_NO='{0}'", SR_NO);

                    sql = "select fwpdb.cimseq() from dual";
                    DataTable seqDT = DB.PDB.GetDataTable(sql);
                    if (seqDT.Rows.Count > 0)
                        WORK_ID = seqDT.Rows[0][0].ToString();
                    else
                        WORK_ID = DateTime.Now.ToString("yyyyMMddHHmmss");


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
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
            
        }

        #region 靜態 Function -----------------------------------------------------------------

        public static bool DeleteWork(string workID, out string result)
        {
            try {
                string sql = string.Format("delete from fwpdb.MES_WORK_LIST where WORK_ID='{0}'", workID);
                DB.PDB.ExecuteNonQuery(sql);
                result = "OK";
                return true;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
        }

        #endregion ----------------------------------------------------------------------------

        //public static List<MESWork> DapperMapping()
        //{
        //    List<MESWork> result = new List<MESWork>();
        //    try
        //    {

        //        using (IDbConnection db = new SqlConnection(constr))
        //        {
        //            db.Execute("SELECT ID FROM dbo.TEST_TABLE");
        //            result = db.Query<MESWork>("SELECT ID FROM dbo.TEST_TABLE").ToList();
        //        }
        //    }
        //    catch(Exception ex) {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return result;
        //}

        public static MESWork GetWorkData(string workID)
        {
            //===================================================
            return GetSampleData(workID);
            //===================================================

            List<MESWork> result = new List<MESWork>();
            try
            {
                using (IDbConnection db = DB.PDB.GetConnection())
                {
                    string sql = string.Format(@"SELECT a.*, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.IT_OWNER) as IT_OWNER_NAME, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.USER1) as USER1_NAME, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.USER2) as USER2_NAME,
                               round(to_date(nvl(DUE_DATE, to_char(sysdate+1,'yyyy-mm-dd')), 'yyyy-mm-dd')-sysdate) as REMAIN_DAY
                               FROM fwpdb.MES_WORK_LIST a where WORK_ID='{0}'", workID);
                    //db.Execute("SELECT ID FROM fwpdb.MES_WORK_LIST");
                    result = db.Query<MESWork>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Function『MESWork.GetWorkData({1})』Fail：{0}", ex.Message, workID));
                //Console.WriteLine(ex.Message);
            }
            if (result.Count > 0)
                return result[0];
            else
                return new MESWork();
        }

        //public static List<MESWork> DapperMapping(string startDate, string endDate, string key, string userID, string status, string type)
        public static List<MESWork> GetWorkList(string startDate, string endDate, string key, string userID, string status, string type1, string type2, string type3, string oper, string cust3, int minWeight, string srNo, bool showSR)
        {
            //===================================================
            return GetSampleData();
            //===================================================

            List<MESWork> result = new List<MESWork>();
            try
            {
                StringBuilder sb = new StringBuilder();

                if (startDate.Length > 0)
                    sb.Append(string.Format(" and (DUE_DATE >= '{0}' or CLOSE_DATE >= '{0}') ", startDate));
                if (endDate.Length > 0)
                    sb.Append(string.Format(" and (ISSUE_DATE <= '{0}') ", endDate));
                if (key.Length > 0)
                    sb.Append(string.Format(" and (upper(WORK_NAME) like '%{0}%' or upper(WORK_DETAIL) like '%{0}%' or upper(WORK_NOTE) like '%{0}%' or upper(IT_NOTE) like '%{0}%') ", key));
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

                if(showSR==false)
                    sb.Append(" and SR_NO is null ");

                string sql = @"SELECT a.*, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.IT_OWNER) as IT_OWNER_NAME, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.USER1) as USER1_NAME, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.USER2) as USER2_NAME,
                               round(to_date(nvl(DUE_DATE, to_char(sysdate+1,'yyyy-mm-dd')), 'yyyy-mm-dd')-sysdate) as REMAIN_DAY
                               FROM fwpdb.MES_WORK_LIST a where 1=1";
                sql += sb.ToString();
                using (IDbConnection db = DB.PDB.GetConnection())
                {
                    result = db.Query<MESWork>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Function『MESWork.GetWorkList(...)』Fail：{0}", ex.Message));
                //Console.WriteLine(ex.Message);
            }
            return result;
        }

        public static List<MESWork> GetUrgentWorkList(int remindDay)
        {
            //===================================================
            return GetSampleData();
            //===================================================

            List<MESWork> result = new List<MESWork>();
            try
            {
                string sql = @"SELECT a.*, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.IT_OWNER) as IT_OWNER_NAME, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.USER1) as USER1_NAME, 
                               (select max(substr(EMAIL, 1, instr(EMAIL,'@')-1)) from fwpdb.hris_hrbank where EMPNO=a.USER2) as USER2_NAME,
                               round(to_date(nvl(DUE_DATE, to_char(sysdate+1,'yyyy-mm-dd')), 'yyyy-mm-dd')-sysdate) as REMAIN_DAY
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
                throw new Exception(string.Format("Function『MESWork.GetUrgentWorkList({1})』Fail：{0}", ex.Message, remindDay));
                //Console.WriteLine(ex.Message);
            }
            return result;
        }


        public static MESWork GetSampleData(string workID)
        {
            List<MESWork> tmpList = GetSampleData();
            foreach(MESWork tmpWork in tmpList)
            {
                if (tmpWork.WORK_ID == workID)
                    return tmpWork;
            }
            return new MESWork();
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
            tmpWork.WORK_NAME = "世界文明崩潰之後 這些國家人類生存機遇大";
            tmpWork.WORK_DETAIL = "最新一項科學研究顯示，如果世界文明毀滅，南太平洋島國紐西蘭（新西蘭）是人類最有機會存活下來的地方，北大西洋島國英國則排名第五，而多數大陸國家則情況不看好。";
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
            tmpWork.WORK_ID = "003";
            tmpWork.WORK_NAME = "氣候變化：IPCC——給地球氣候寫「體檢報告」的國際權威機構";
            tmpWork.WORK_DETAIL = "氣候變化是今日國際頭等大事之一，全球變暖帶來的各種變化日漸顯著。聯合國提出一個地球變暖的上限 — 氣溫比前工業化時代最多高1.5℃，";
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
            tmpWork.WORK_ID = "004";
            tmpWork.WORK_NAME = "健康與健身：走步鍛煉未必為人知曉的七大好處";
            tmpWork.WORK_DETAIL = "走步是最簡單易行的一種運動方式，特別是在大多數活動受到限制的新冠疫情期間，只有外出走步相對自由。";
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

            return xx;
            #endregion ---------------------------------
        }
    }
}
