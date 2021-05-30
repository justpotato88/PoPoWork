using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//-----------------
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace PotatoLab.Controllers.Sample
{
    public class FileUploadSample : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileUploadSample(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Index()
        {
            //return Content($"WebRootPath = {_hostingEnvironment.WebRootPath}\n" +
            //               $"ContentRootPath = {_hostingEnvironment.ContentRootPath}");

            return View();
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}

        public string test()
        {
            return "XXX";
        }

        //https://blog.johnwu.cc/article/asp-net-core-getting-web-root-path.html
        //Sample 2 https://code-maze.com/file-upload-aspnetcore-mvc/
        //安全 https://docs.microsoft.com/zh-tw/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0

        [HttpPost]
        public IActionResult Upload(Microsoft.AspNetCore.Http.IFormFile[] files)
        {
            // Iterate through uploaded files array
            foreach (var file in files)
            {
                // Extract file name from whatever was posted by browser
                var fileName = System.IO.Path.GetFileName(file.FileName);

                // If file with same name exists delete it
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
                
                // Create new local file and copy contents of uploaded file
                //using (var localFile = System.IO.File.OpenWrite(fileName))
                using (var uploadedFile = file.OpenReadStream())
                {
                    string EndDirectory = @"E:\01_工作區\MVC\PotatoLab\PotatoLab\uploadFileFolder\";
                    FileStream DestinationStream = System.IO.File.Create(EndDirectory + fileName);
                    //uploadedFile.CopyTo(localFile);
                    uploadedFile.CopyTo(DestinationStream);
                    
                }
            }

            ViewBag.Message = "Files successfully uploaded";

            return View();
        }

        //public ActionResult BasicUpload(string qqfile)
        //{
        //    string uploadFolder = "FileUpload";
        //    //string path = Server.MapPath(string.Format("~/{0}", uploadFolder));
        //    string path = _hostingEnvironment.ContentRootPath;

        //    try
        //    {

        //        // To handle differences in FireFox/Chrome/Safari/Opera
        //        Stream stream = Request.Files.Count > 0
        //            ? Request.Files[0].InputStream
        //            : Request.InputStream;
        //        string filePath = Request.Files.Count > 0
        //            ? Path.Combine(path, System.IO.Path.GetFileName(Request.Files[0].FileName))
        //            : Path.Combine(path, qqfile);
        //        var buffer = new byte[stream.Length];
        //        stream.Read(buffer, 0, buffer.Length);
        //        System.IO.File.WriteAllBytes(filePath, buffer);
        //        return Json(new { success = true }, "text/html");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, "text/html");
        //    }
        //}
    }
}
