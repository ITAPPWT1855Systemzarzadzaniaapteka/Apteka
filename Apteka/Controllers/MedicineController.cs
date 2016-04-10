using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apteka.Models;
using System.Web.Script.Serialization;
using System.IO;

namespace Apteka.Controllers
{
    public class MedicineController : Controller
    {
        private aptekaEntities1 db = new aptekaEntities1();
        // GET: Medicine
        public ActionResult Index()
        {
            return View(db.Lek.ToList().Take(10));
        }

        // GET: Medicine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lek lek = db.Lek.Find(id);
            if (lek == null)
            {
                return HttpNotFound();
            }
            return View(lek);
        }

        // GET: Medicine/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: Medicines
        public JsonResult GetMedicines(string text)
        {
            var medicines = db.Lek.Where(m => m.Nazwa.Contains(text)).Take(10).ToList();
            return new JsonResult
            {
                Data = medicines.Select(m => m),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        // GET: Medicine/Create
        public ActionResult Sell(Operacja operacja = null)
        {
            ViewBag.Operation = operacja;
            ViewBag.Medicines = from item in db.Lek.Take(5)
                                select new SelectListItem
                                {
                                    Text = item.Nazwa + " " + item.Postac + " " + item.Opakowanie,
                                    Value = item.Id_lek.ToString()
                                };
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SerializeSell([Bind(Include = "ID_lek, Rozchod")] Operacja operacja)
        {
            operacja.Data = DateTime.Now.ToString();
            operacja.ID_user = db.AspNetUsers.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;
            operacja.Przychod = 0;
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(operacja);
            var dataFile = Server.MapPath("~/App_Data/sell/sell.json");
            using (StreamWriter stream = System.IO.File.AppendText(dataFile))
            {
                stream.WriteLine(serializedResult);
            }
            return RedirectToAction("Sell", operacja);
        }
        // POST: Medicine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_lek,Nazwa,Postac,Opakowanie,Dawka")] Lek lek)
        {
            if (ModelState.IsValid)
            {
                db.Lek.Add(lek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lek);
        }

        // GET: Medicine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lek lek = db.Lek.Find(id);
            if (lek == null)
            {
                return HttpNotFound();
            }
            return View(lek);
        }

        // POST: Medicine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_lek,Nazwa,Postac,Opakowanie,Dawka")] Lek lek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lek);
        }

        // GET: Medicine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lek lek = db.Lek.Find(id);
            if (lek == null)
            {
                return HttpNotFound();
            }
            return View(lek);
        }

        // POST: Medicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lek lek = db.Lek.Find(id);
            db.Lek.Remove(lek);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
