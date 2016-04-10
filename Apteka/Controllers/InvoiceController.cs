using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apteka.Models;
using System.IO;

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
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateInvoiceModel model) {
            System.Diagnostics.Debug.WriteLine(model.Date.ToString() + " " + model.Products[0].Name);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public JsonResult GetWarehouses()
        {
            var warehouses = context.Hurtownias.ToList();
            return new JsonResult
            {
                Data = warehouses.Select(warehouse => warehouse),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public ActionResult Create(Faktura faktura)
        {
            context.Fakturas.Add(faktura);
            context.SaveChanges();
           // var data = jsonFacture.Data;
           // Faktura facture = new Faktura() { Numer = jsonFacture.nr, Hurtownia = jsonFacture.warehouseNr };

            return RedirectToAction("Index", "Home");
        }

    }
}