using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOdevi.Models;

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
    }
}