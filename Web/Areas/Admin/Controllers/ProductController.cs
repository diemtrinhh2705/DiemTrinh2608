using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Context;

namespace Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebEntities5 dbObj = new WebEntities5();
        public ActionResult Index()
        {
            var ListProduct = dbObj.Products.ToList();
            return View(ListProduct);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //var ListProduct = dbObj.Products.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            try
            {
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + extension + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                dbObj.Products.Add(objProduct);
                dbObj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

            [HttpGet]
            public ActionResult Details(int id)
            {
                var objProduct = dbObj.Products.Where(n=>n.id == id).FirstOrDefault();
                return View(objProduct);
            }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = dbObj.Products.Where(n => n.id == id).FirstOrDefault();
           
            return View(objProduct);
        }
    }

    }