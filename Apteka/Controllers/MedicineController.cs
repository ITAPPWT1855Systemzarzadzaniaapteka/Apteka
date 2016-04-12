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
using CsvHelper;
using System.Data.Entity.Validation;


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
            //      operacja.Data = DateTime.Now.ToString();
            //    operacja.ID_user = db.AspNetUsers.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;

            var dataFile = Server.MapPath("~/App_Data/sell/sell.csv");
            string details = operacja.ID_lek + "," + operacja.Rozchod;
            System.IO.File.AppendAllText(dataFile, details);
            return RedirectToAction("Sell", operacja);
        }
        private List<Operacja> readFromCsv(){
              var dataFile = Server.MapPath("~/App_Data/sell/sell.csv");
            List<Operacja> operations = System.IO.File.ReadAllLines(dataFile)
                   .Select(x => x.Split(','))
                   .Select(x => new Operacja
                   {
                       ID_lek = Int32.Parse(x[0]),
                       Rozchod = Int32.Parse(x[1]),
                   }).ToList();

           return (from op in operations
                          group op by op.ID_lek
                              into newOp
                              select new Operacja
                              {
                                  ID_operacja = DateTime.Now.Day+DateTime.Now.Hour+ DateTime.Now.Minute + DateTime.Now.Second,
                                  ID_lek = newOp.First().ID_lek,
                                  Rozchod = newOp.Sum(o => o.Rozchod),
                                  Data =  DateTime.Now.ToString(),
                                  Przychod = 0,
                                  ID_user = db.AspNetUsers.FirstOrDefault(u => u.UserName == User.Identity.Name).Id
                              }).ToList();
        }
        public ActionResult Summary()
        {
            var grouped = readFromCsv();
            return View(grouped);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSell([Bind(Include = "ID_lek, Rozchod")] Operacja operacja)
        {
            var grouped = readFromCsv();
            foreach (var op in grouped)
            {
                try
                {
                    db.Operacjas.Add(op);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    throw e;
                }
              
                
            }
                            return RedirectToAction("Index");
     
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
