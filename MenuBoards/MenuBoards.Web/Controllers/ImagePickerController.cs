using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MenuBoards.Web.ViewModels.Images;

namespace MenuBoards.Web.Controllers
{
    public class ImagePickerController : Controller
    {
        
        [HttpGet]
        public ActionResult LoadImages()
        {
            var images = new List<ImageFileViewModel>();
            string curDir = ControllerContext.HttpContext.Server.MapPath("~/Images");

            var directoryInfo = new DirectoryInfo(curDir);

            if (directoryInfo.Exists)
            {
                foreach (var fileInfo in directoryInfo.EnumerateFiles())
                {
                    images.Add(new ImageFileViewModel
                    {
                        Name = fileInfo.Name,
                        Path = fileInfo.FullName
                    });
                }
            }

            return this.PartialView(images);
        }
        
        [HttpGet]
        public JsonResult GetImages(string folder)
        {
            var images = new List<ImageFileViewModel>();
            string curDir = ControllerContext.HttpContext.Server.MapPath("~/Images");
            if (!string.IsNullOrEmpty(folder))
            {
                curDir += "/" + folder;
            }

            var directoryInfo = new DirectoryInfo(curDir);

            if (directoryInfo.Exists)
            {
                foreach (var fileInfo in directoryInfo.EnumerateFiles())
                {
                    images.Add(new ImageFileViewModel
                    {
                        Name = fileInfo.Name,
                        Path = fileInfo.FullName
                    });
                }
            }

            return Json(images, JsonRequestBehavior.AllowGet);
        }
    }
}
