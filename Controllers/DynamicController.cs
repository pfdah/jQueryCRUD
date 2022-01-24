using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jqueryCrud.Models;
namespace jqueryCrud.Controllers
{
    public class DynamicController : Controller
    {
        // GET: Dynamic
        readonly itemdbEntities db = new itemdbEntities(); 
        public ActionResult Index()
        {
            var lst = db.floors.ToList();
            return View(lst);
        }
        [HttpPost]
        public ActionResult Index(List<floor> floor)
        {
            foreach(var data in floor)
            {
                db.floors.Add(data);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateFloor(floor floor)
        {

            var updatedFloor = (from c in db.floors
                                where c.floor_id == floor.floor_id
                                select c).FirstOrDefault();
            updatedFloor.floor_name = floor.floor_name;
            updatedFloor.area = floor.area;
            updatedFloor.rate = floor.rate;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteFloor(int floor_id)
        {

            var data = (from c in db.floors
                        where c.floor_id == floor_id
                        select c).FirstOrDefault();
            db.floors.Remove(data);
            db.SaveChanges();
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}