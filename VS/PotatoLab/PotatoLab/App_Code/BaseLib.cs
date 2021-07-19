using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

                //using (IDbConnection db = new SqlConnection(constr))
                //{
                //    db.Execute("SELECT ID FROM dbo.TEST_TABLE");
                //    result = db.Query<MESWork>("SELECT ID FROM dbo.TEST_TABLE").ToList();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public static List<UserFile> QueryUser(string queryKey)
        {
            List<UserFile> result = new List<UserFile>();
            try
            {

                //using (IDbConnection db = new SqlConnection(constr))
                //{
                //    db.Execute("SELECT ID FROM dbo.TEST_TABLE");
                //    result = db.Query<MESWork>("SELECT ID FROM dbo.TEST_TABLE").ToList();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
