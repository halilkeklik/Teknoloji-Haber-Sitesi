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

        public ActionResult Post(int id)
        {
            var post = db.Post.Where(p => p.PostId == id).SingleOrDefault();
            if(post==null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        public JsonResult Comment(string comment,int postids, int yazdimi)
        {
                var userid = Session["userid"];
                db.Comment.Add(new Comment { UserId = Convert.ToInt32(Session["userid"]), PostId = postids, CommentContent = comment, CommentDate = DateTime.Now });
                db.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}