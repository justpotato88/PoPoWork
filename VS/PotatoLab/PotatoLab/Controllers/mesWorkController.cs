using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//-------------------------
using System.Text.Json;
using System.Text.Json.Serialization;
//-------------------------
using X.PagedList;
using Microsoft.AspNetCore.Hosting;
//-------------------------
using System.Text;

namespace PotatoLab.Controllers
{
    public class mesWorkController : Controller
    {
        #region 取得根目錄-----------------------------------------
        private readonly IHostingEnvironment _hostingEnvironment;
        public mesWorkController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public ActionResult RootPath()
        {
            return Content($"WebRootPath = {_hostingEnvironment.WebRootPath}\n" +
                           $"ContentRootPath = {_hostingEnvironment.ContentRootPath}");
        }
        #endregion -----------------------------------------------

        // GET: mesWorkController
        public ActionResult Index(int page=1, string orderby = "due", string sort="asc")
        {
            

            int currentPage = page < 1 ? 1 : page;
            var resultList = MESWork.GetUrgentWorkList(7).OrderBy(m => m.DUE_DATE).ToList();


            ViewBag.PageList = resultList.ToPagedList(currentPage, 3);

            ////排序
            ViewBag.CurrentOrderBy = orderby;
            ViewBag.CurrentSort = sort;


            //預設值
            ViewBag.QueryStart = DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd");
            ViewBag.QueryEnd = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");

            ViewBag.QueryStatus01 = "checked";
            ViewBag.QueryStatus02 = "checked";
            ViewBag.QueryStatus03 = "checked";
            ViewBag.QueryStatus04 = "checked";
            ViewBag.QueryStatus05 = "checked";

            ViewBag.QueryType01 = "checked";
            ViewBag.QueryType02 = "checked";
            ViewBag.QueryType03 = "checked";
            ViewBag.QueryType04 = "checked";
            ViewBag.QueryType05 = "checked";
            ViewBag.QueryType06 = "checked";
            ViewBag.QueryType07 = "checked";
            ViewBag.QueryType08 = "checked";
            ViewBag.QueryType09 = "checked";
            ViewBag.QueryType10 = "checked";

            ViewBag.QueryWeight = 2;

            return View();
        }

        [HttpPost]
        public ActionResult Index(IFormCollection form)
        {
            int page = 1;
            try { page = Convert.ToInt32(form["PageIndex"]); }
            catch { }

            string orderby = form["orderBy"].ToString();
            string sort = form["sort"].ToString();

            string status = "";
            string type = "";
            #region 勾選的條件 --------------------------------------
            if (form["qStatus01"].ToString().Length > 0)
            {
                ViewBag.QueryStatus01 = "checked";
                status += form["qStatus01"].ToString() + ",";
            }
            if (form["qStatus02"].ToString().Length > 0)
            {
                ViewBag.QueryStatus02 = "checked";
                status += form["qStatus02"].ToString() + ",";
            }
            if (form["qStatus03"].ToString().Length > 0)
            {
                ViewBag.QueryStatus03 = "checked";
                status += form["qStatus03"].ToString() + ",";
            }
            if (form["qStatus04"].ToString().Length > 0)
            {
                ViewBag.QueryStatus04 = "checked";
                status += form["qStatus04"].ToString() + ",";
            }
            if (form["qStatus05"].ToString().Length > 0)
            {
                ViewBag.QueryStatus05 = "checked";
                status += form["qStatus05"].ToString() + ",";
            }
            if (form["qStatus06"].ToString().Length > 0)
            {
                ViewBag.QueryStatus06 = "checked";
                status += form["qStatus06"].ToString() + ",";
            }
            if (form["qStatus07"].ToString().Length > 0)
            {
                ViewBag.QueryStatus07 = "checked";
                status += form["qStatus07"].ToString() + ",";
            }
            status = status.TrimEnd(',');
            //---------------------------------------
            if (form["qType01"].ToString().Length > 0)
            {
                ViewBag.QueryType01 = "checked";
                type += form["qType01"].ToString() + ",";
            }
            if (form["qType02"].ToString().Length > 0)
            {
                ViewBag.QueryType02 = "checked";
                type += form["qType02"].ToString() + ",";
            }
            if (form["qType03"].ToString().Length > 0)
            {
                ViewBag.QueryType03 = "checked";
                type += form["qType03"].ToString() + ",";
            }
            if (form["qType04"].ToString().Length > 0)
            {
                ViewBag.QueryType04 = "checked";
                type += form["qType04"].ToString() + ",";
            }
            if (form["qType05"].ToString().Length > 0)
            {
                ViewBag.QueryType05 = "checked";
                type += form["qType05"].ToString() + ",";
            }
            if (form["qType06"].ToString().Length > 0)
            {
                ViewBag.QueryType06 = "checked";
                type += form["qType06"].ToString() + ",";
            }
            if (form["qType07"].ToString().Length > 0)
            {
                ViewBag.QueryType07 = "checked";
                type += form["qType07"].ToString() + ",";
            }
            if (form["qType08"].ToString().Length > 0)
            {
                ViewBag.QueryType08 = "checked";
                type += form["qType08"].ToString() + ",";
            }
            if (form["qType09"].ToString().Length > 0)
            {
                ViewBag.QueryType09 = "checked";
                type += form["qType09"].ToString() + ",";
            }
            if (form["qType10"].ToString().Length > 0)
            {
                ViewBag.QueryType10 = "checked";
                type += form["qType10"].ToString() + ",";
            }
            if (form["qType11"].ToString().Length > 0)
            {
                ViewBag.QueryType11 = "checked";
                type += form["qType11"].ToString() + ",";
            }
            if (form["qType12"].ToString().Length > 0)
            {
                ViewBag.QueryType12 = "checked";
                type += form["qType12"].ToString() + ",";
            }
            type = type.TrimEnd(',');

            #endregion ----------------------------------------------

            int weight = 2;
            try
            {
                Convert.ToInt32(form["txtQueryWeight"]);
            }
            catch { }
            var resultList = new List<MESWork>();

            resultList = MESWork.GetWorkList(form["txtQueryStartDate"].ToString(), form["txtQueryEndDate"].ToString(), form["txtQueryKeyWord"].ToString(), form["txtQueryUser"].ToString(), status, type, form["txtQueryType2"].ToString(), form["txtQueryType3"].ToString(), "", "", weight, form["txtSRNo"].ToString());
            switch (orderby)
            {
                case "title":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.WORK_NAME).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.WORK_NAME).ToList();
                    break;
                case "status":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.STATUS).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.STATUS).ToList();
                    break;
                case "sr_no":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.SR_NO).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.SR_NO).ToList();
                    break;
                case "due":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.DUE_DATE).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.DUE_DATE).ToList();
                    break;
                case "issue":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.ISSUE_DATE).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.ISSUE_DATE).ToList();
                    break;
                case "it":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.IT_OWNER).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.IT_OWNER).ToList();
                    break;
                case "user1":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.USER1).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.USER1).ToList();
                    break;
                case "user2":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.USER2).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.USER2).ToList();
                    break;

                case "type1":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.TYPE1).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.TYPE1).ToList();
                    break;
                case "type2":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.TYPE2).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.TYPE2).ToList();
                    break;
                case "type3":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.TYPE3).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.TYPE3).ToList();
                    break;
                case "oper":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.OPER).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.OPER).ToList();
                    break;
                case "cust":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.CUST3).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.CUST3).ToList();
                    break;
                default:
                    resultList = resultList.OrderBy(m => m.WORK_ID).ToList();
                    break;
            }

            //查詢條件
            ViewBag.QueryStart = form["txtQueryStartDate"].ToString();
            ViewBag.QueryEnd = form["txtQueryEndDate"].ToString();
            ViewBag.QueryUser = form["txtQueryUser"].ToString();
            ViewBag.QueryKeyWord = form["txtQueryKeyWord"].ToString();
            ViewBag.QueryWeight = form["txtQueryWeight"].ToString();

            ViewBag.QueryType2 = form["txtQueryType2"].ToString();
            ViewBag.QueryType3 = form["txtQueryType3"].ToString();
            ViewBag.QuerySRNo = form["txtSRNo"].ToString();



            //分頁
            int currentPage = page < 1 ? 1 : page;
            ViewBag.PageList = resultList.ToPagedList(currentPage, 3);

            //排序
            ViewBag.OrderBy = orderby;
            ViewBag.Sort = sort;
            ViewBag.PageIndex = page;


            return View();
        }

        // GET: mesWorkController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //// GET: mesWorkController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: mesWorkController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: mesWorkController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}
        //public ActionResult Edit(string txtWorkID, IFormCollection collection)
        // POST: mesWorkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection form)
        {
            try
            {
                MESWork tmpWork = new MESWork();
                tmpWork.WORK_ID = form["txtWorkID"].ToString();
                tmpWork.WORK_NAME = form["txtWorkName"].ToString();
                tmpWork.WORK_DETAIL = form["txtWorkDetail"].ToString();
                tmpWork.BENEFIT = form["txtBenefit"].ToString();

                tmpWork.IT_OWNER = form["ddlITOwner"].ToString();
                tmpWork.USER1 = form["txtUser1"].ToString();
                tmpWork.USER2 = form["txtUser2"].ToString();

                tmpWork.TYPE1 = form["ddlType1"].ToString();
                tmpWork.TYPE2 = form["txtType2"].ToString();
                tmpWork.TYPE3 = form["txtType3"].ToString();

                tmpWork.WEIGHT = Convert.ToInt32(form["ddlWeight"]);
                tmpWork.STATUS = form["ddlStatus"].ToString();
                tmpWork.SR_NO = form["txtSRNo"].ToString();
                tmpWork.SR_TITLE = form["txtSRTitle"].ToString();
                tmpWork.SR_LINK = form["txtSRLink"].ToString();

                tmpWork.FAC = form["txtFac"].ToString();
                tmpWork.OPER = form["txtOper"].ToString();
                tmpWork.CUST3 = form["txtCust3"].ToString();

                tmpWork.ISSUE_DATE = form["txtIssueDate"].ToString();
                tmpWork.DUE_DATE = form["txtDueDate"].ToString();
                tmpWork.START_DATE = form["txtStartDate"].ToString();
                tmpWork.END_DATE = form["txtEndDate"].ToString();
                tmpWork.CLOSE_DATE = form["txtCloseDate"].ToString();

                string resultMsg = "";
                if (tmpWork.SaveToDB(out resultMsg) == false)
                {
                    ViewBag.Message = resultMsg;
                }

                ViewBag.QueryStart = tmpWork.ISSUE_DATE;
                ViewBag.QueryEnd = tmpWork.ISSUE_DATE;


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: mesWorkController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: mesWorkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public string GetWorkData(string workID)
        {
            //PotatoLab.MESWork tmpWork = PotatoLab.MESWork.GetWorkData(workID);
            //string jsonString = JsonSerializer.Serialize(tmpWork);
            //return jsonString;

            PotatoLab.MESWork tmpWork = new MESWork();
            tmpWork.WORK_ID = workID;
            tmpWork.WORK_NAME = "hk4g4";
            tmpWork.WORK_DETAIL = "WORKDETAIL";
            tmpWork.WORK_NOTE = "WORKDETAILsss1";
            tmpWork.IT_NOTE = "WORKDEssTAILsss2";
            tmpWork.BENEFIT = "BENFIT";

            tmpWork.IT_OWNER = "C2043";
            tmpWork.IT_OWNER_NAME = "C2043";
            tmpWork.USER1 = "C12112";
            tmpWork.USER1_NAME = "C12112";
            tmpWork.USER2 = "C12113";
            tmpWork.USER2_NAME = "C12113";

            tmpWork.TYPE1 = "課程認證";
            tmpWork.TYPE2 = "T2";
            tmpWork.TYPE3 = "T3";
            tmpWork.WEIGHT = 1;

            tmpWork.STATUS = "Wait-Assign";
            tmpWork.SR_NO = "sr_no";
            tmpWork.SR_TITLE = "SRTITLE";
            tmpWork.SR_LINK = "http://";

            tmpWork.FAC = "ASE07";
            tmpWork.OPER = "3800";
            tmpWork.CUST3 = "MVL";

            tmpWork.ISSUE_DATE = "2019-01-01";
            tmpWork.START_DATE = "2019-01-02";
            tmpWork.END_DATE = "2019-01-03";
            tmpWork.DUE_DATE = "2019-01-04";
            tmpWork.CLOSE_DATE = "2019-01-05";


            string jsonString = JsonSerializer.Serialize(tmpWork);

            return jsonString;

            //Date = DateTime.Parse("2019-08-01"),
            //反序略化
            /*
             WeatherForecast weatherForecast = 
                JsonSerializer.Deserialize<WeatherForecast>(jsonString);
             */
        }

        public string DeleteWork(string workID)
        {
            //return RedirectToAction(nameof(Index));
            return "{\"Result\":\"OK" + workID + "\"}";
        }

        private List<PotatoLab.MESWork> GetSampleData()
        {
            List<PotatoLab.MESWork> xx = new List<MESWork>();
            PotatoLab.MESWork tmpWork = new MESWork();
            tmpWork.WORK_ID = "001";
            tmpWork.WORK_NAME = "hk4g4";
            tmpWork.WORK_DETAIL = "WORKDETAIL";
            tmpWork.BENEFIT = "BENFIT";

            tmpWork.IT_OWNER = "C2043";
            tmpWork.USER1 = "C12112";
            tmpWork.USER2 = "C12113";

            tmpWork.TYPE1 = "課程認證";
            tmpWork.TYPE2 = "T2";
            tmpWork.TYPE3 = "T3";
            tmpWork.WEIGHT = 1;

            tmpWork.STATUS = "Wait-Assign";
            tmpWork.SR_NO = "sr_no";
            tmpWork.SR_TITLE = "SRTITLE";
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
        }

        private List<PotatoLab.MESWork> GetSampleData(string startDate, string endDate, string key, string userID, string status, string type)
        {
            List<PotatoLab.MESWork> xx = new List<MESWork>();
            PotatoLab.MESWork tmpWork = new MESWork();
            tmpWork.WORK_ID = "001";
            tmpWork.WORK_NAME = "hk4g4";
            tmpWork.WORK_DETAIL = "WORKDETAIL";
            tmpWork.BENEFIT = "BENFIT";

            tmpWork.IT_OWNER = "C2043";
            tmpWork.USER1 = "C12112";
            tmpWork.USER2 = "C12113";

            tmpWork.TYPE1 = "課程認證";
            tmpWork.TYPE2 = "T2";
            tmpWork.TYPE3 = "T3";
            tmpWork.WEIGHT = 1;

            tmpWork.STATUS = "Wait-Assign";
            tmpWork.SR_NO = "sr_no";
            tmpWork.SR_TITLE = "SRTITLE";
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
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string GetMatchUser(string query)
        {
            return "[{\"label\":\"黃群凱 (Potato Huang)\",\"value\":\"C2043\"},{\"label\":\"略皮略皮\",\"value\":\"2\"}]";


            List<UserFile> tmpUser = BaseLib.QueryUser(query);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < tmpUser.Count; i++)
            {
                if(i>0)
                    sb.Append(",");
                sb.Append("{");
                sb.Append("\"label\":\"");
                sb.Append(tmpUser[i].NotesID);
                sb.Append("(");
                sb.Append(tmpUser[i].CName);
                sb.Append(")");
                sb.Append("\",");
                sb.Append("\"value\":\"");
                sb.Append(tmpUser[i].ID);
                sb.Append("\"");
                sb.Append(")");
                sb.Append("}");
            }
            sb.Append("]");
            return sb.ToString();
        }

        public string GetMatchCust(string query)
        {
            return "[{\"label\":\"MVL\",\"value\":\"MVL\"},{\"label\":\"略皮略皮\",\"value\":\"2\"}]";

            List<CustFile> tmpCust = BaseLib.QueryCust(query);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < tmpCust.Count; i++)
            {
                if (i > 0)
                    sb.Append(",");
                sb.Append("{");
                sb.Append("\"label\":\"");
                sb.Append(tmpCust[i].Cust3);
                sb.Append("(");
                sb.Append(tmpCust[i].CustName);
                sb.Append(")");
                sb.Append("\",");
                sb.Append("\"value\":\"");
                sb.Append(tmpCust[i].Cust3);
                sb.Append("\"");
                sb.Append(")");
                sb.Append("}");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
