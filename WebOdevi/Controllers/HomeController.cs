using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOdevi.Models;
using PagedList;
using PagedList.Mvc;
using System.Net;

namespace WebOdevi.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        webodevDB db = new webodevDB();
        public ActionResult Index(int Page = 1)
        {

            var post = db.Post.OrderByDescending(p => p.PostId).ToPagedList(Page, 3);
            return View(post);
        }

        public ActionResult Category(int id, int Page = 1)
        {
            var posts = db.Post.Where(p => p.CatId == id).OrderByDescending(i => i.PostId).ToPagedList(Page, 3);
            var post = db.Cat.Where(c => c.CatId == id).SingleOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        public ActionResult Post(int id)
        {
            var post = db.Post.Where(p => p.PostId == id).SingleOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //public JsonResult Comment(string comment, int postids)
        //{
        //    var userid = Session["userid"];
        //    Comment comments = new Comment
        //    {
        //        UserId = Convert.ToInt32(Session["userid"]),
        //        PostId = postids,
        //        CommentContent = comment,
        //        CommentDate = DateTime.Now
        //    };
        //    db.Comment.Add(comments);
        //    db.SaveChanges();

        //    return Json(comments);
        //}

        public ActionResult CreateComment(int id)
        {
            var varmi = db.Post.Where(p => p.PostId == id).SingleOrDefault();
            if (varmi == null)
            {
                return HttpNotFound();
            }
            Session["postid"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreateComment(Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            comment.UserId = Convert.ToInt32(Session["userid"]);
            comment.PostId = Convert.ToInt32(Session["postid"]);
            db.Comment.Add(comment);
            db.SaveChanges();
            Session["postid"] = "";
            ModelState.Clear();
            return View();
        }

        public ActionResult DeleteComment(int id)
        {
            var userid = Session["userid"];
            var comment = db.Comment.Where(i => i.CommentId == id).SingleOrDefault();
            var post = db.Post.Where(p => p.PostId == comment.PostId).SingleOrDefault();

            if (comment.UserId == Convert.ToInt32(userid))
            {
                db.Comment.Remove(comment);
                db.SaveChanges();
                return RedirectToAction("Post", "Home", new { id = post.PostId });
            }
            else
                return HttpNotFound();
        }

        // POST: deneme/Delete/5
        [HttpPost]
        public ActionResult DeleteComment(int id, Comment comment)
        {
            try
            {
                if (comment.UserId == Convert.ToInt32(Session["userid"]))
                {
                    var comments = db.Comment.Where(i => i.CommentId == id).SingleOrDefault();
                    if (comments == null)
                    {
                        return HttpNotFound();
                    }

                    db.Comment.Remove(comments);
                    db.SaveChanges();
                    return View();
                }
                else
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CatWidget()
        {
            return View(db.Cat.ToList());
        }

        public ActionResult Search(string search = null, int Page = 1)
        {
            var searchend = db.Post.Where(p => p.Context.Contains(search)).OrderByDescending(i => i.PostId).ToPagedList(Page, 3);
            return View(searchend);
        }

    }
}