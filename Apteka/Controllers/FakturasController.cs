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
    public class FakturasController : Controller
    {
        private aptekaEntities1 db = new aptekaEntities1();

        // GET: Fakturas
        public ActionResult Index()
        {
            var faktura = db.Faktura.Include(f => f.Hurtownia);
            return View(faktura.ToList());
        }

        // GET: Fakturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Faktura.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // GET: Fakturas/Create
        public ActionResult Create()
        {
            ViewBag.Id_hurtownia = new SelectList(db.Hurtownia, "Id_hurtownia", "Nazwa");
            return View();
        }

        // POST: Fakturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_faktura,Id_hurtownia,Numer")] Faktura faktura)
        {
            if (ModelState.IsValid)
            {
                db.Faktura.Add(faktura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_hurtownia = new SelectList(db.Hurtownia, "Id_hurtownia", "Nazwa", faktura.Id_hurtownia);
            return View(faktura);
        }

        // GET: Fakturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Faktura.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_hurtownia = new SelectList(db.Hurtownia, "Id_hurtownia", "Nazwa", faktura.Id_hurtownia);
            return View(faktura);
        }

        // POST: Fakturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_faktura,Id_hurtownia,Numer")] Faktura faktura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faktura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_hurtownia = new SelectList(db.Hurtownia, "Id_hurtownia", "Nazwa", faktura.Id_hurtownia);
            return View(faktura);
        }

        // GET: Fakturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = db.Faktura.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            return View(faktura);
        }

        // POST: Fakturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faktura faktura = db.Faktura.Find(id);
            db.Faktura.Remove(faktura);
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
