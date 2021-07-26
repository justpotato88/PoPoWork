using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//-----------------
using System.Data;
using Dapper;

namespace PotatoLab
{
    public class CustFile
    {
        public string Cust2 { get; set; }
        public string Cust3 { get; set; }
        public string CustName { get; set; }
    }
    public class UserFile
    {
        public string ID { get; set; }
        public string CName { get; set; }
        public string NotesID { get; set; }

    }
    public class BaseLib
    {
        public static List<CustFile> QueryCust(string queryKey)
        {
            List<CustFile> result = new List<CustFile>();
            try
            {
                queryKey = queryKey.ToUpper();
                using (IDbConnection db = DB.PDB.GetConnection())
                {
                    string sql = string.Format(@"
                    select * from (
                    select CUST_GROUP as CustName, FORECAST_CUST_GRP as Cust3, CUST_CODE as Cust2
                    from fwpdb.ase_cust_file 
                    where CUST_GROUP is not null
                    and (upper(CUST_GROUP) like '%{0}%' or FORECAST_CUST_GRP like '{0}%' or CUST_CODE like '{0}%')
                    order by FORECAST_CUST_GRP ) where rownum < 12", queryKey);
                    //db.Execute("SELECT ID FROM fwpdb.MES_WORK_LIST");
                    result = db.Query<CustFile>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("函數 QueryCust 發生錯誤：" + ex.Message);
                //Console.WriteLine(ex.Message);
            }
            return result;
        }

        public static List<UserFile> QueryUser(string queryKey)
        {
            List<UserFile> result = new List<UserFile>();
            try
            {
                queryKey = queryKey.ToUpper();
                using (IDbConnection db = DB.PDB.GetConnection())
                {
                    string sql = string.Format(@"
                    select * from (
                    select EMPNO as ID, PERSONALNAMETW as CName, substr(EMAIL,1,instr(EMAIL,'@')-1) as NotesID
                    from fwpdb.hris_hrbank 
                    where EMAIL is not null
                    and (upper(EMAIL) like '{0}%' or EMPNO like '{0}%' or PERSONALNAMETW like '%{0}%')
                    order by EMAIL ) where rownum < 12", queryKey);
                    //db.Execute("SELECT ID FROM fwpdb.MES_WORK_LIST");
                    result = db.Query<UserFile>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("函數 QueryUser 發生錯誤：" + ex.Message);
                //Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
