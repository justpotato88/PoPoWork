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
            var resultList = MESWork.GetUrgentWorkList(7).OrderBy(m => m.DateDue).ToList();


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

            resultList = MESWork.GetWorkList(form["txtQueryStartDate"].ToString(), form["txtQueryEndDate"].ToString(), form["txtQueryUser"].ToString(), form["txtQueryKeyWord"].ToString(), status, type, "", "", "", "", weight);
            switch (orderby)
            {
                case "title":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.WorkName).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.WorkName).ToList();
                    break;
                case "status":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.Status).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.Status).ToList();
                    break;
                case "sr_no":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.SRNo).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.SRNo).ToList();
                    break;
                case "due":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.DateDue).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.DateDue).ToList();
                    break;
                case "it":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.ITOwner).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.ITOwner).ToList();
                    break;
                case "user1":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.User1).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.User1).ToList();
                    break;
                case "user2":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.User2).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.User2).ToList();
                    break;

                case "type1":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.Type1).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.Type1).ToList();
                    break;
                case "type2":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.Type2).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.Type2).ToList();
                    break;
                case "type3":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.Type3).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.Type3).ToList();
                    break;
                case "oper":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.Oper).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.Oper).ToList();
                    break;
                case "cust":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.Cust3).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.Cust3).ToList();
                    break;
                default:
                    resultList = resultList.OrderBy(m => m.WorkID).ToList();
                    break;
            }

            //查詢條件
            ViewBag.QueryStart = form["txtQueryStartDate"].ToString();
            ViewBag.QueryEnd = form["txtQueryEndDate"].ToString();
            ViewBag.QueryUser = form["txtQueryUser"].ToString();
            ViewBag.QueryKeyWord = form["txtQueryKeyWord"].ToString();
            ViewBag.QueryWeight = form["txtQueryWeight"].ToString();

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

        // GET: mesWorkController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mesWorkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
                tmpWork.WorkID = form["txtWorkID"].ToString();
                tmpWork.WorkName = form["txtWorkName"].ToString();
                tmpWork.WorkDetail = form["txtWorkDetail"].ToString();
                tmpWork.Benefit = form["txtBenefit"].ToString();

                tmpWork.ITOwner = form["txtITOwner"].ToString();
                tmpWork.User1 = form["txtUser1"].ToString();
                tmpWork.User2 = form["txtUser2"].ToString();

                tmpWork.Type1 = form["ddlType1"].ToString();
                tmpWork.Type2 = form["txtType2"].ToString();
                tmpWork.Type3 = form["txtType3"].ToString();

                tmpWork.Weight = Convert.ToInt32(form["ddlWeight"]);
                tmpWork.Status = form["ddlStatus"].ToString();
                tmpWork.SRNo = form["txtSRNo"].ToString();
                tmpWork.SRTitle = form["txtSRTitle"].ToString();

                tmpWork.Fac = form["txtFac"].ToString();
                tmpWork.Oper = form["txtOper"].ToString();
                tmpWork.Cust3 = form["txtCust3"].ToString();

                tmpWork.DateIssue = form["txtIssueDate"].ToString();
                tmpWork.DateDue = form["txtDueDate"].ToString();
                tmpWork.DateStart = form["txtStartDate"].ToString();
                tmpWork.DateEnd = form["txtEndDate"].ToString();
                tmpWork.DateClose = form["txtCloseDate"].ToString();

                string resultMsg = "";
                if (tmpWork.SaveToDB(out resultMsg) == false)
                {
                    ViewBag.Message = resultMsg;
                }

                ViewBag.QueryStart = tmpWork.DateIssue;
                ViewBag.QueryEnd = tmpWork.DateIssue;

                //To Save
                //string WorkID = form["txtWorkID"].ToString();
                //string WorkName = form["txtWorkName"].ToString();
                //string WorkDetail = form["txtWorkDetail"].ToString();
                //string Benefit = form["txtBenefit"].ToString();

                //string ITOwner = form["txtITOwner"].ToString();
                //string User1 = form["txtUser1"].ToString();
                //string User2 = form["txtUser2"].ToString();

                //string Type1 = form["ddlType1"].ToString();
                //string Type2 = form["txtType2"].ToString();
                //string Type3 = form["txtType3"].ToString();

                //string Weight = form["ddlWeight"].ToString();
                //string Status = form["ddlStatus"].ToString();
                //string SRNo = form["txtSRNo"].ToString();
                //string SRTitle = form["txtSRTitle"].ToString();

                //string Fac = form["txtFac"].ToString();
                //string Oper = form["txtOper"].ToString();
                //string Cust3 = form["txtCust3"].ToString();

                //string IssueDate = form["txtIssueDate"].ToString();
                //string DueDate = form["txtDueDate"].ToString();
                //string StartDate = form["txtStartDate"].ToString();
                //string EndDate = form["txtEndDate"].ToString();
                //string CloseDate = form["txtCloseDate"].ToString();

                //string a = txtWorkID;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
            PotatoLab.MESWork tmpWork = new MESWork();
            tmpWork.WorkID = workID;
            tmpWork.WorkName = "hk4g4";
            tmpWork.WorkDetail = "WORKDETAIL";
            tmpWork.WorkNote = "WORKDETAILsss1";
            tmpWork.NoteIT = "WORKDEssTAILsss2";
            tmpWork.Benefit = "BENFIT";

            tmpWork.ITOwner = "C2043";
            tmpWork.User1 = "C12112";
            tmpWork.User2 = "C12113";

            tmpWork.Type1 = "課程認證";
            tmpWork.Type2 = "T2";
            tmpWork.Type3 = "T3";
            tmpWork.Weight = 1;

            tmpWork.Status = "Wait-Assign";
            tmpWork.SRNo = "sr_no";
            tmpWork.SRTitle = "SRTITLE";
            tmpWork.SRLink = "http://";

            tmpWork.Fac = "ASE07";
            tmpWork.Oper = "3800";
            tmpWork.Cust3 = "MVL";

            tmpWork.DateIssue = "2019-01-01";
            tmpWork.DateStart = "2019-01-02";
            tmpWork.DateEnd = "2019-01-03";
            tmpWork.DateDue = "2019-01-04";
            tmpWork.DateClose = "2019-01-05";


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
            tmpWork.WorkID = "001";
            tmpWork.WorkName = "hk4g4";
            tmpWork.WorkDetail = "WORKDETAIL";
            tmpWork.Benefit = "BENFIT";

            tmpWork.ITOwner = "C2043";
            tmpWork.User1 = "C12112";
            tmpWork.User2 = "C12113";

            tmpWork.Type1 = "課程認證";
            tmpWork.Type2 = "T2";
            tmpWork.Type3 = "T3";
            tmpWork.Weight = 1;

            tmpWork.Status = "Wait-Assign";
            tmpWork.SRNo = "sr_no";
            tmpWork.SRTitle = "SRTITLE";
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
        }

        private List<PotatoLab.MESWork> GetSampleData(string startDate, string endDate, string key, string userID, string status, string type)
        {
            List<PotatoLab.MESWork> xx = new List<MESWork>();
            PotatoLab.MESWork tmpWork = new MESWork();
            tmpWork.WorkID = "001";
            tmpWork.WorkName = "hk4g4";
            tmpWork.WorkDetail = "WORKDETAIL";
            tmpWork.Benefit = "BENFIT";

            tmpWork.ITOwner = "C2043";
            tmpWork.User1 = "C12112";
            tmpWork.User2 = "C12113";

            tmpWork.Type1 = "課程認證";
            tmpWork.Type2 = "T2";
            tmpWork.Type3 = "T3";
            tmpWork.Weight = 1;

            tmpWork.Status = "Wait-Assign";
            tmpWork.SRNo = "sr_no";
            tmpWork.SRTitle = "SRTITLE";
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
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string GetMatchUser(string query)
        {
            //return "[\"CCC\",\"AAA\"]";
            return "[{\"label\":\"黃群凱 (Potato Huang)\",\"value\":\"C2043\"},{\"label\":\"略皮略皮\",\"value\":\"2\"}]";
        }

        public string GetMatchCust(string query)
        {
            //return "[\"CCC\",\"AAA\"]";
            return "[{\"label\":\"MVL (Potato Huang)\",\"value\":\"C2043\"},{\"label\":\"略皮略皮\",\"value\":\"2\"}]";
        }
    }
}
