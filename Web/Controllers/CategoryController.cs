using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Context;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        WebEntities5 objWebEntities5 = new WebEntities5();
        public ActionResult index()
        {
            var ListCategory = objWebEntities5.Categories.ToList();
            return View(ListCategory);
        }
        public ActionResult ProductCategory(int id)
        {
            var ListProduct = objWebEntities5.Products.Where(n => n.CategoryId == id).ToList();
            return View(ListProduct);
        }
    }
}