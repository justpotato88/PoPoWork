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
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace PotatoLab.Controllers
{
    public class mesMemoController : Controller
    {
        #region 取得根目錄-----------------------------------------
        private readonly IHostingEnvironment _hostingEnvironment;
        public mesMemoController(IHostingEnvironment hostingEnvironment)
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
        public ActionResult Index(int page = 1, string orderby = "due", string sort = "desc")
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

            ViewBag.QueryType01 = "checked";
            ViewBag.QueryType02 = "checked";


            ViewBag.InPost = "N";

            return View();
        }

        [HttpPost]
        public ActionResult Index(IFormCollection form)
        {
            //存檔
            if (form["actionMode"].ToString() == "EDIT")
            {
                string result = "";
                string workID = "";
                if (Edit(form, out result, out workID) == false)
                    ViewBag.ResultMessage = result;
                ViewBag.LastEditID = workID;
            }
            else if (form["actionMode"].ToString() == "CKEDIT")
            {
                string workID = form["ckeditorWorkID"].ToString();
                string gg = form["myCKEditorContain"].ToString();
                try
                {
                    string path = _hostingEnvironment.WebRootPath + "\\my_ckeditor_files\\" + workID + ".txt";
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false))
                    {
                        sw.Write(gg);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ResultMessage = ex.Message;
                }
                ViewBag.LastEditID = workID;
            }
            else if (form["deleteWorkID"].ToString() != "")
            {
                string delWorkID = form["deleteWorkID"].ToString();
                string delResult = "";
                if (MESWork.DeleteWork(delWorkID, out delResult) == true)
                    ViewBag.ResultMessage = string.Format("已刪除({0})!!", delWorkID);
                else
                    ViewBag.ResultMessage = string.Format("刪除失敗({0})!!", delResult);
            }

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
            type = type.TrimEnd(',');
            #endregion ----------------------------------------------


            var resultList = new List<MESWork>();

            resultList = MESWork.GetWorkList(form["txtQueryStartDate"].ToString(), form["txtQueryEndDate"].ToString(), form["txtQueryKeyWord"].ToString(), "", status, "MEMO", type, "", "", "", 0, "", false);
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
                case "type2":
                    if (sort == "asc")
                        resultList = resultList.OrderBy(m => m.TYPE2).ToList();
                    else
                        resultList = resultList.OrderByDescending(m => m.TYPE2).ToList();
                    break;
                
                default:
                    resultList = resultList.OrderByDescending(m => m.ISSUE_DATE).ToList();
                    break;
            }

            //查詢條件
            ViewBag.QueryStart = form["txtQueryStartDate"].ToString();
            ViewBag.QueryEnd = form["txtQueryEndDate"].ToString();
            ViewBag.QueryKeyWord = form["txtQueryKeyWord"].ToString();


            //分頁
            int currentPage = page < 1 ? 1 : page;
            ViewBag.PageList = resultList.ToPagedList(currentPage, 3);

            //排序
            ViewBag.OrderBy = orderby;
            ViewBag.Sort = sort;
            ViewBag.PageIndex = page;

            ViewBag.InPost = "Y";

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool Edit(IFormCollection form, out string result, out string workID)
        {
            result = "";
            workID = form["txtWorkID"].ToString();
            try
            {
                MESWork tmpWork = new MESWork();
                tmpWork.WORK_ID = form["txtWorkID"].ToString();
                tmpWork.WORK_NAME = form["txtWorkName"].ToString();
                tmpWork.WORK_NOTE = form["txtWorkNote"].ToString();
                
                tmpWork.TYPE1 = "MEMO";
                tmpWork.TYPE2 = form["ddlType"].ToString();
                tmpWork.STATUS = form["ddlStatus"].ToString();
               
                tmpWork.ISSUE_DATE = form["txtIssueDate"].ToString();
                tmpWork.DUE_DATE = form["txtDueDate"].ToString();

                string resultMsg = "";
                if (tmpWork.SaveToDB(out resultMsg) == false)
                {
                    result = resultMsg;
                    return false;
                }
                else
                {
                    result = "OK";
                    workID = tmpWork.WORK_ID;
                    return true;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
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
            PotatoLab.MESWork tmpWork = PotatoLab.MESWork.GetWorkData(workID);
            string jsonString = JsonSerializer.Serialize(tmpWork);
            return jsonString;
        }

        [HttpPost]
        public string GetWorkHtml(string workID)
        {
            string result = "";
            string path = _hostingEnvironment.WebRootPath + "\\my_ckeditor_files\\" + workID + ".txt";
            try
            {
                if (System.IO.File.Exists(path) == false)
                    return "";
                System.IO.FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using (StreamReader sr = new StreamReader(fs))
                {
                    result = sr.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                return string.Format("讀取失敗：({1})：{0}", ex.Message, path);
            }
        }

        public string DeleteWork(string workID)
        {
            return "{\"Result\":\"OK" + workID + "\"}";
        }

    }
}
