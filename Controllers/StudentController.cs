using jqueryCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adminCrud_with_ForeignKeys.Controllers
{
    public class StudentController : Controller
    {
        readonly jqueryAcEntities db = new jqueryAcEntities();
        // GET: Student
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Class = new SelectList(db.Classes.ToList(), "Id", "Name");
            ViewBag.Gender = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Selected = false, Text = "Male", Value = ("M").ToString()},
                new SelectListItem { Selected = false, Text = "Female", Value = ("F").ToString()},
                new SelectListItem { Selected = false, Text = "Others", Value = ("O").ToString()},
            }, "Value", "Text", 1);
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var item = db.Students.Find(id);
            ViewBag.Class = new SelectList(db.Classes.ToList(), "Id", "Name", item.ClassId);
            ViewBag.Gender = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Selected = false, Text = "Male", Value = ("M").ToString()},
                new SelectListItem { Selected = false, Text = "Female", Value = ("F").ToString()},
                new SelectListItem { Selected = false, Text = "Others", Value = ("O").ToString()},
            }, "Value", "Text", item.Gender);
            return View(db.Students.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            var item = db.Students.Find(student.ID);
            if(item!= null)
            {
                item.Name = student.Name;
                item.Roll = student.Roll;
                item.Address = student.Address;
                item.Gender = student.Gender;
                item.ClassId = student.ClassId;
                item.StudentPerformance.EnglishScore = student.StudentPerformance.EnglishScore;
                item.StudentPerformance.MathScore = student.StudentPerformance.MathScore;
                item.StudentPerformance.ScienceScore = student.StudentPerformance.ScienceScore;
                item.StudentPerformance.Remarks = student.StudentPerformance.Remarks;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(db.Students.Find(id));
        }
        public ActionResult Delete(int id)
        {
            return View(db.Students.Find(id));
        }
        [HttpPost]
        public ActionResult Delete(Student student)
        {
            var item = db.Students.Find(student.ID);
            var peritem = db.StudentPerformances.Find(student.ID);
            db.StudentPerformances.Remove(peritem);
            db.Students.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SeePerf(int id)
        {
            var item = db.StudentPerformances.Find(id);
            return View(item);
        }
    }
}