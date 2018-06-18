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

        // GET: ImagePicker/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ImagePicker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImagePicker/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ImagePicker/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ImagePicker/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ImagePicker/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ImagePicker/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
