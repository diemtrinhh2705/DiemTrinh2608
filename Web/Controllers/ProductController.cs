using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Context;

namespace Web.Controllers
{ 
    public class ProductController : Controller
    {
        WebEntities5 objWebEntities5 = new WebEntities5();
        public ActionResult Detail(int id)
        {
            var objProduct = objWebEntities5.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
    }
}  