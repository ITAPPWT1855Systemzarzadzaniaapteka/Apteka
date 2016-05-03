using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apteka.Models;

namespace Apteka.Controllers
{
    public class WarehouseController : Controller
    {
        private aptekaEntities1 db = new aptekaEntities1();

        // GET: Warehouse
        public ActionResult Index()
        {
            return View(db.Hurtownia.ToList());
        }

        // GET: Warehouse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hurtownia hurtownia = db.Hurtownia.Find(id);
            if (hurtownia == null)
            {
                return HttpNotFound();
            }
            return View(hurtownia);
        }

        // GET: Warehouse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_hurtownia,Nazwa,NIP")] Hurtownia hurtownia)
        {
            if (ModelState.IsValid)
            {
                db.Hurtownia.Add(hurtownia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hurtownia);
        }

        // GET: Warehouse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hurtownia hurtownia = db.Hurtownia.Find(id);
            if (hurtownia == null)
            {
                return HttpNotFound();
            }
            return View(hurtownia);
        }

        // POST: Warehouse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_hurtownia,Nazwa,NIP")] Hurtownia hurtownia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hurtownia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hurtownia);
        }

        // GET: Warehouse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hurtownia hurtownia = db.Hurtownia.Find(id);
            if (hurtownia == null)
            {
                return HttpNotFound();
            }
            return View(hurtownia);
        }

        // POST: Warehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hurtownia hurtownia = db.Hurtownia.Find(id);
            db.Hurtownia.Remove(hurtownia);
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
