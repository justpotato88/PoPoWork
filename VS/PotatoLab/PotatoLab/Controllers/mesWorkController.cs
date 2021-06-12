using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//-------------------------
using X.PagedList;

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

        // GET: mesWorkController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: mesWorkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET:
        public string GetWorkData2(string workID)
        {
            PotatoLab.MESWork tmpWork = new MESWork();
            return "aaaa";
        }

        public PotatoLab.MESWork GetWorkData(string workID)
        {
            PotatoLab.MESWork tmpWork = new MESWork();
            tmpWork.WorkID = "999";
            tmpWork.WorkName = "hk4g4";
            return tmpWork;
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
    }
}
