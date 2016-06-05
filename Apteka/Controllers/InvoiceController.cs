using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apteka.Models;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net;
namespace Apteka.Controllers 
{

    [Authorize]
    public class InvoiceController : Controller
    {
        aptekaEntities1 context = new aptekaEntities1();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ID_hurtownia = new SelectList(context.Hurtownia, "ID_hurtownia", "Nazwa");
            ViewBag.Id_lek = new SelectList(context.Lek, "Id_lek", "Nazwa");
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faktura faktura = context.Faktura.Find(id);
            if (faktura == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_hurtownia = new SelectList(context.Hurtownia, "Id_hurtownia", "Nazwa", faktura.Id_hurtownia);
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
                context.Entry(faktura).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index", "CheckInvoice");
            }
            ViewBag.Id_hurtownia = new SelectList(context.Hurtownia, "Id_hurtownia", "Nazwa", faktura.Id_hurtownia);
            return RedirectToAction("Index", "CheckInvoice");
        }


        [HttpGet]
        public ActionResult GetWarehouses()
        {
            var warehouses = context.Hurtownia.ToList()
                .Select(i => new { i.ID_hurtownia, i.Nazwa, i.NIP })
                .ToList();
            return Json(warehouses, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMedicines()
        {
            var medicines = context.Lek.ToList()
                .Select(i => new { i.Id_lek, i.Nazwa, i.Postac, i.Opakowanie})
                .ToList();
            return Json(medicines, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(CreateInvoiceModel model)
        {
             
            foreach (var i in model.Products)
            {
                var vat = i.Vat;
                var price = i.Price;
                var netto = double.Parse(price.Replace(".", ","));
                var brutto = (1 + (double.Parse(vat.Replace("%", "")) / 100)) * double.Parse(price.Replace(".", ","));
                Operacja operacja = new Operacja
                {
                    ID_user = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                    Data = model.Date,
                    ID_lek = i.Id,
                    Rozchod = i.Quantity,
                    Przychod = 0,
                    Netto = netto,
                    Brutto = brutto
                };
                Faktura faktura = new Faktura()
                {
                    Numer = model.InvoiceId,
                    Id_hurtownia = model.WarehouseId,
                };
                Faktura_operacja faktura_operacja = new Faktura_operacja() { Faktura = faktura, Operacja = operacja };
                faktura.Faktura_operacja.Add(faktura_operacja);
                operacja.Faktura_operacja.Add(faktura_operacja);
                context.Operacja.Add(operacja);
                context.Faktura.Add(faktura);
            }

            context.SaveChanges();
            return RedirectToAction("Index", "CheckInvoice");
        }
    }
}