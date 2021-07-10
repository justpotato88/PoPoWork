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
//-------------------------

namespace PotatoLab.Controllers
{
    public class mesWorkController : Controller
    {
        // GET: mesWorkController
        public ActionResult Index(int page=1)
        {
            int currentPage = page < 1 ? 1 : page;
            var resultList = GetSampleData().OrderBy(m => m.WorkID).ToList();

            ViewBag.PageList = resultList.ToPagedList(currentPage, 3);
            return View();
        }
        //var result = resultList.ToPagedList(currentPage, 10);

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
                string WorkID = form["txtWorkID"].ToString();
                string WorkName = form["txtWorkName"].ToString();
                string WorkDetail = form["txtWorkDetail"].ToString();
                string Benefit = form["txtBenefit"].ToString();

                string ITOwner = form["txtITOwner"].ToString();
                string User1 = form["txtUser1"].ToString();
                string User2 = form["txtUser2"].ToString();

                string Type1 = form["ddlType1"].ToString();
                string Type2 = form["txtType2"].ToString();
                string Type3 = form["txtType3"].ToString();

                string Weight = form["ddlWeight"].ToString();
                string Status = form["ddlStatus"].ToString();
                string SRNo = form["txtSRNo"].ToString();
                string SRTitle = form["txtSRTitle"].ToString();

                string Fac = form["txtFac"].ToString();
                string Oper = form["txtOper"].ToString();
                string Cust3 = form["txtCust3"].ToString();

                string IssueDate = form["txtIssueDate"].ToString();
                string DueDate = form["txtDueDate"].ToString();
                string StartDate = form["txtStartDate"].ToString();
                string EndDate = form["txtEndDate"].ToString();
                string CloseDate = form["txtCloseDate"].ToString();

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

            tmpWork = new MESWork();
            tmpWork.WorkID = "002";
            tmpWork.WorkName = "BBBBBBB";
            xx.Add(tmpWork);

            tmpWork = new MESWork();
            tmpWork.WorkID = "003";
            tmpWork.WorkName = "CCCCCC";
            xx.Add(tmpWork);

            tmpWork = new MESWork();
            tmpWork.WorkID = "004";
            tmpWork.WorkName = "DDDDDD";
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
    }
}
