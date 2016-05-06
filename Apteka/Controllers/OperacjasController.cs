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
    public class OperacjasController : Controller
    {
        private aptekaEntities1 db = new aptekaEntities1();

        // GET: Operacjas
        public ActionResult Index()
        {
            var operacja = db.Operacja.Include(o => o.AspNetUsers).Include(o => o.Lek);
            return View(operacja.ToList());
        }

        // GET: Operacjas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operacja operacja = db.Operacja.Find(id);
            if (operacja == null)
            {
                return HttpNotFound();
            }
            return View(operacja);
        }

        // GET: Operacjas/Create
        public ActionResult Create()
        {
            ViewBag.ID_user = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Id_lek = new SelectList(db.Lek, "Id_lek", "Nazwa");
            return View();
        }

        // POST: Operacjas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_operacja,Id_lek,Data,ID_user,Przychod,Rozchod,Netto,Brutto")] Operacja operacja)
        {
            if (ModelState.IsValid)
            {
                db.Operacja.Add(operacja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_user = new SelectList(db.AspNetUsers, "Id", "Email", operacja.ID_user);
            ViewBag.Id_lek = new SelectList(db.Lek, "Id_lek", "Nazwa", operacja.ID_lek);
            return View(operacja);
        }

        // GET: Operacjas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operacja operacja = db.Operacja.Find(id);
            if (operacja == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_user = new SelectList(db.AspNetUsers, "Id", "Email", operacja.ID_user);
            ViewBag.Id_lek = new SelectList(db.Lek, "Id_lek", "Nazwa", operacja.ID_lek);
            return View(operacja);
        }

        // POST: Operacjas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_operacja,Id_lek,Data,ID_user,Przychod,Rozchod,Netto,Brutto")] Operacja operacja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operacja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_user = new SelectList(db.AspNetUsers, "Id", "Email", operacja.ID_user);
            ViewBag.Id_lek = new SelectList(db.Lek, "Id_lek", "Nazwa", operacja.ID_lek);
            return View(operacja);
        }

        // GET: Operacjas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operacja operacja = db.Operacja.Find(id);
            if (operacja == null)
            {
                return HttpNotFound();
            }
            return View(operacja);
        }

        // POST: Operacjas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Operacja operacja = db.Operacja.Find(id);
            db.Operacja.Remove(operacja);
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
