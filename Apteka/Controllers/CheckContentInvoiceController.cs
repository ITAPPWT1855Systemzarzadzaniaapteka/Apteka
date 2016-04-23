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
    public class CheckContentInvoiceController : Controller
    {
        private aptekaEntities1 db = new aptekaEntities1();

        // GET: CheckContentInvoice
        public ActionResult Index()
        {
            return View(db.Sprawdz_zawartosc_faktury.ToList());
        }

        // GET: CheckContentInvoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprawdz_zawartosc_faktury sprawdz_zawartosc_faktury = db.Sprawdz_zawartosc_faktury.Find(id);
            if (sprawdz_zawartosc_faktury == null)
            {
                return HttpNotFound();
            }
            return View(sprawdz_zawartosc_faktury);
        }

        // GET: CheckContentInvoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckContentInvoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_faktura,Id_lek,Nazwa,Dawka,Opakowanie,Postac,Ilosc_zakupionego_leku")] Sprawdz_zawartosc_faktury sprawdz_zawartosc_faktury)
        {
            if (ModelState.IsValid)
            {
                db.Sprawdz_zawartosc_faktury.Add(sprawdz_zawartosc_faktury);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sprawdz_zawartosc_faktury);
        }

        // GET: CheckContentInvoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprawdz_zawartosc_faktury sprawdz_zawartosc_faktury = db.Sprawdz_zawartosc_faktury.Find(id);
            if (sprawdz_zawartosc_faktury == null)
            {
                return HttpNotFound();
            }
            return View(sprawdz_zawartosc_faktury);
        }

        // POST: CheckContentInvoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_faktura,Id_lek,Nazwa,Dawka,Opakowanie,Postac,Ilosc_zakupionego_leku")] Sprawdz_zawartosc_faktury sprawdz_zawartosc_faktury)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprawdz_zawartosc_faktury).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sprawdz_zawartosc_faktury);
        }

        // GET: CheckContentInvoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprawdz_zawartosc_faktury sprawdz_zawartosc_faktury = db.Sprawdz_zawartosc_faktury.Find(id);
            if (sprawdz_zawartosc_faktury == null)
            {
                return HttpNotFound();
            }
            return View(sprawdz_zawartosc_faktury);
        }

        // POST: CheckContentInvoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sprawdz_zawartosc_faktury sprawdz_zawartosc_faktury = db.Sprawdz_zawartosc_faktury.Find(id);
            db.Sprawdz_zawartosc_faktury.Remove(sprawdz_zawartosc_faktury);
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
