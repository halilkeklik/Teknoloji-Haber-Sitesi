using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOdevi.Models;
using PagedList;
using PagedList.Mvc;

namespace WebOdevi.Controllers
{
    public class AdminController : Controller
    {
        webodevDB db = new webodevDB();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region // Post
        public ActionResult Post()
        {
            var posts = db.Post.ToList();
            return View(posts);
        }


        public ActionResult PostCreate()
        {
            ViewBag.CatId = new SelectList(db.Cat, "CatId", "CatName");
            //ViewBag.UserId = new SelectList(db.User, "UserId", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult PostCreate(Post post, string Tags)
        {
            try
            {
                post.Time = DateTime.Now;
                post.UserId = Convert.ToInt32(Session["userid"]);
                // TODO: Add insert logic here
                db.Post.Add(post);
                db.SaveChanges();

                return RedirectToAction("Post");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult PostEdit(int id)
        {
            var post = db.Post.Where(i => i.PostId == id).SingleOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatId = new SelectList(db.Cat, "CatId", "CatName", post.CatId);
            return View(post);
        }

        [HttpPost]
        public ActionResult PostEdit(int id, Post post)
        {
            try
            {
                var newpost = db.Post.Where(i => i.PostId == id).SingleOrDefault();

                newpost.Name = post.Name;
                newpost.Context = post.Context;
                newpost.Photo = post.Photo;
                newpost.CatId = post.CatId;
                db.SaveChanges();

                return RedirectToAction("Post");
            }
            catch
            {
                return View();
            }
        }

        // GET: deneme/Delete/5
        public ActionResult PostDelete(int id)
        {
            var post = db.Post.Where(i => i.PostId == id).SingleOrDefault();
            if (post == null)
                return HttpNotFound();
            return View(post);
        }

        // POST: deneme/Delete/5
        [HttpPost]
        public ActionResult PostDelete(int id, FormCollection collection)
        {
            try
            {
                var post = db.Post.Where(i => i.PostId == id).SingleOrDefault();
                if (post == null)
                {
                    return HttpNotFound();
                }

                db.Post.Remove(post);
                db.SaveChanges();
                return RedirectToAction("Post");
            }
            catch
            {
                return View();
            }

        }
        #endregion
        #region// Category

        public ActionResult Category()
        {
            var cats = db.Cat.ToList();
            return View(cats);
        }

        public ActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CategoryCreate(Cat cat, string Tags)
        {
            try
            {
                // TODO: Add insert logic here
                db.Cat.Add(cat);
                db.SaveChanges();

                return RedirectToAction("Category");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CategoryEdit(int id)
        {
            var cat = db.Cat.Where(i => i.CatId == id).SingleOrDefault();
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }

        [HttpPost]
        public ActionResult CategoryEdit(int id, Cat cat)
        {
            try
            {
                var newcat = db.Cat.Where(i => i.CatId == id).SingleOrDefault();

                newcat.CatName = cat.CatName;
                db.SaveChanges();

                return RedirectToAction("Category");
            }
            catch
            {
                return View();
            }
        }

        // GET: deneme/Delete/5
        public ActionResult CategoryDelete(int id)
        {
            var cat = db.Cat.Where(i => i.CatId == id).SingleOrDefault();
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // POST: deneme/Delete/5
        [HttpPost]
        public ActionResult CategoryDelete(int id, FormCollection collection)
        {
            try
            {
                var cat = db.Cat.Where(i => i.CatId == id).SingleOrDefault();
                if (cat == null)
                {
                    return HttpNotFound();
                }

                db.Cat.Remove(cat);
                db.SaveChanges();
                return RedirectToAction("Category");
            }
            catch
            {
                return View();
            }

        }
        #endregion
        #region // User
        public ActionResult Users()
        {
            var users = db.User.ToList();
            return View(users);
        }

        public ActionResult UserEdit(int id)
        {
            var user = db.User.Where(i => i.UserId == id).SingleOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Group, "GroupId", "GroupName", user.GroupId);
            return View(user);
        }

        [HttpPost]
        public ActionResult UserEdit(int id, User user)
        {
            try
            {
                var newuser = db.User.Where(i => i.UserId == id).SingleOrDefault();
                newuser.UserName = user.UserName;
                newuser.UserPass = user.UserPass;
                newuser.UserMail = user.UserMail;
                newuser.UserFullName = user.UserFullName;
                newuser.GroupId = user.GroupId;


                db.SaveChanges();

                return RedirectToAction("Users");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UserDelete(int id)
        {
            var user = db.User.Where(i => i.UserId == id).SingleOrDefault();
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        // POST: deneme/Delete/5
        [HttpPost]
        public ActionResult UserDelete(int id, FormCollection collection)
        {
            try
            {
                var users = db.User.Where(i => i.UserId == id).SingleOrDefault();
                if (users == null)
                {
                    return HttpNotFound();
                }

                db.User.Remove(users);
                db.SaveChanges();
                return RedirectToAction("Users");
            }
            catch
            {
                return View();
            }
        }
        #endregion
        #region // Comment
        public ActionResult Comment(int Page = 1)
        {
            var comments = db.Comment.OrderByDescending(p => p.CommentId).ToPagedList(Page, 10); ;
            return View(comments);
        }

        public ActionResult EditComment(int id)
        {
            var comment = db.Comment.Where(i => i.CommentId == id).SingleOrDefault();
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost]
        public ActionResult EditComment(int id, Comment comment)
        {
            try
            {
                var newcomment = db.Comment.Where(i => i.CommentId == id).SingleOrDefault();
                newcomment.CommentContent = comment.CommentContent;

                db.SaveChanges();

                return RedirectToAction("Comment");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteComment(int id)
        {
            var comment = db.Comment.Where(i => i.CommentId == id).SingleOrDefault();
            if (comment == null)
                return HttpNotFound();
            return View(comment);
        }

        // POST: deneme/Delete/5
        [HttpPost]
        public ActionResult DeleteComment(int id, FormCollection collection)
        {
            try
            {
                var comment = db.Comment.Where(i => i.CommentId == id).SingleOrDefault();
                if (comment == null)
                {
                    return HttpNotFound();
                }

                db.Comment.Remove(comment);
                db.SaveChanges();
                return RedirectToAction("Comment");
            }
            catch
            {
                return View();
            }

        }
        #endregion
    }
}