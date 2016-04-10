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
    public class StoreController : Controller
    {
        private aptekaEntities1 db = new aptekaEntities1();

        // GET: Store
        public ActionResult Index()
        {
            return View(db.Stan_magazynu.ToList());
        }

        // GET: Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stan_magazynu stan_magazynu = db.Stan_magazynu.Find(id);
            if (stan_magazynu == null)
            {
                return HttpNotFound();
            }
            return View(stan_magazynu);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_lek,Nazwa,Dawka,Postac,Opakowanie,Obecny_Stan_Magazynu")] Stan_magazynu stan_magazynu)
        {
            if (ModelState.IsValid)
            {
                db.Stan_magazynu.Add(stan_magazynu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stan_magazynu);
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stan_magazynu stan_magazynu = db.Stan_magazynu.Find(id);
            if (stan_magazynu == null)
            {
                return HttpNotFound();
            }
            return View(stan_magazynu);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_lek,Nazwa,Dawka,Postac,Opakowanie,Obecny_Stan_Magazynu")] Stan_magazynu stan_magazynu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stan_magazynu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stan_magazynu);
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stan_magazynu stan_magazynu = db.Stan_magazynu.Find(id);
            if (stan_magazynu == null)
            {
                return HttpNotFound();
            }
            return View(stan_magazynu);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stan_magazynu stan_magazynu = db.Stan_magazynu.Find(id);
            db.Stan_magazynu.Remove(stan_magazynu);
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
