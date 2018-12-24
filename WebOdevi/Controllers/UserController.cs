using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebOdevi.Models;

namespace WebOdevi.Controllers
{
    public class UserController : Controller
    {
        private webodevDB db = new webodevDB();
        // GET: User

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.GroupId = 2;
                db.User.Add(user);
                db.SaveChanges();
                Session["userid"] = user.UserId;
                Session["username"] = user.UserName;
                Session["usergroup"] = user.GroupId;
                return RedirectToAction("Index", "Home");
            }
            else
                return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                var login = db.User.Where(u => u.UserName == user.UserName).SingleOrDefault();
                if (login.UserName == user.UserName && login.UserPass == user.UserPass)
                {
                    Session["userid"] = login.UserId;
                    Session["username"] = login.UserName;
                    Session["usergroup"] = login.GroupId;
                    Session["usergroupname"] = login.Group.GroupName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["userid"] = null;
            Session["username"] = null;
            Session["usergroup"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Detail(int id)
        {
            var user = db.User.Where(u => u.UserId == id).SingleOrDefault();
            return View(user);
        }

        public ActionResult Edit(int id)
        {
            var user = db.User.Where(u => u.UserId == id).SingleOrDefault();
            if (Convert.ToInt32(Session["userid"]) != user.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user, int id)
        {
            if (ModelState.IsValid)
            {
                var usernow = db.User.Where(u => u.UserId == id).SingleOrDefault();
                usernow.UserName = user.UserName;
                usernow.UserPass = user.UserPass;
                usernow.UserMail = user.UserMail;
                usernow.UserFullName = user.UserFullName;
                db.SaveChanges();
                Session["username"] = user.UserName;
                return RedirectToAction("Detail", "User", new { id = usernow.UserId });
            }
            else
                return View();
        }
    }
}