using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Context;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        WebEntities5 objWebEntities5 = new WebEntities5();

        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objWebEntities5.Categories.ToList();
            objHomeModel.ListProduct = objWebEntities5.Products.ToList();
            return View(objHomeModel);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //Post: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objWebEntities5.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objWebEntities5.Configuration.ValidateOnSaveEnabled = false;
                    objWebEntities5.Users.Add(_user);
                    objWebEntities5.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
                {
                ViewBag.error = "Email already exists";
                return View();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objWebEntities5.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FistName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        //create a string md5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i=0; i<targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;

        }


    }
}