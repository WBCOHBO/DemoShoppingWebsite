using DemoShoppingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DemoShoppingWebsite.Controllers
{
    public class HomeController : Controller
    {
        dbShoppingCarAzureEntities db = new dbShoppingCarAzureEntities();
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserId, string Password)
        {
            //找出符合登入帳密的Member資料
            var member = db.table_Member.Where(m => m.UserId.Equals(UserId) && m.Password == Password).FirstOrDefault();
            if (UserId != null || Password != null)
            {
                if (member == null)
                {
                    ViewBag.Message = "帳號或密碼錯誤，請重新確認";
                    return View();
                }
                else
                {
                    Session["Welcome"] = $"{member.Name} 您好";
                    FormsAuthentication.RedirectFromLoginPage(UserId, true);
                    return RedirectToAction("Index", "Member");
                }
            }
            return View();
        }
    }
}