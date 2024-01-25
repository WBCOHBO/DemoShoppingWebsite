using DemoShoppingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoShoppingWebsite.Controllers
{
    public class HomeController : Controller
    {
        dbShoppingCarEntities db = new dbShoppingCarEntities();
        public ActionResult Index()
        {
            var products = db.table_Product.OrderByDescending(m =>  m.Id).ToList();
            return View(products);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(table_Member Member)
        {
            //如果資料驗證未通過，則回傳原本的View
            if(!ModelState.IsValid)
            {
                return View();
            }

            //如果註冊帳號不存在，才能新增與儲存
            var member = db.table_Member.Where(m => m.UserId == Member.UserId).FirstOrDefault();
            if(member == null)
            {
                db.table_Member.Add(Member);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ViewBag.Message = "帳號已被使用，請重新註冊";
            return View();
        }
    }
}