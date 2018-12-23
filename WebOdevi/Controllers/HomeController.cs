using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOdevi.Models;

namespace WebOdevi.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        webodevDB db = new webodevDB();
        public ActionResult Index()
        {
            var post = db.Post.OrderByDescending(p => p.PostId).ToList();
            return View(post);
        }
    }
}