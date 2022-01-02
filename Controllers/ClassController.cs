using jqueryCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adminCrud_with_ForeignKeys.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        readonly jqueryAcEntities db = new jqueryAcEntities(); 
        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Class cl)
        {
            db.Classes.Add(cl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            return View(db.Classes.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Class cl)
        {
            var item = db.Classes.Find(cl.Id);
            if (item != null)
            {
                item.Name = cl.Name;
                item.Section = cl.Section;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(db.Classes.Find(id));
        }
        public ActionResult Delete(int id)
        {
            return View(db.Classes.Find(id));
        }
        [HttpPost]
        public ActionResult Delete(Class cl)
        {
            var item = db.Classes.Find(cl.Id);
            db.Classes.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ViewFullList(int id)
        {
            var item = db.Classes.Find(id);
            return View(item.Students.ToList());
        }

    }
}