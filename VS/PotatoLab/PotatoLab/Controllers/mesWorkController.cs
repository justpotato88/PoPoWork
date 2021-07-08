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
                string workName = form["txtWorkName"].ToString();
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
            tmpWork.OwnerIT = "C2043";
            tmpWork.DateDue = "2019-01-02";


            string jsonString = JsonSerializer.Serialize(tmpWork);

            return jsonString;

            //Date = DateTime.Parse("2019-08-01"),
            //反序略化
            /*
             WeatherForecast weatherForecast = 
                JsonSerializer.Deserialize<WeatherForecast>(jsonString);
             */
        }


        private List<PotatoLab.MESWork> GetSampleData()
        {
            List<PotatoLab.MESWork> xx = new List<MESWork>();
            PotatoLab.MESWork tmpWork = new MESWork();
            tmpWork.WorkID = "001";
            tmpWork.WorkName = "AAAAAAAAA";
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


        public string GetMatchUser(string query)
        {
            //return "[\"CCC\",\"AAA\"]";
            return "[{\"label\":\"Potato Huang (黃群凱)\",\"value\":\"C2043\"},{\"label\":\"略皮略皮\",\"value\":\"2\"}]";
        }
    }
}
