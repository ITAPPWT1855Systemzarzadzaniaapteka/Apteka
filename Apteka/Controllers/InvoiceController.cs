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
            ViewBag.Id_hurtownia = new SelectList(context.Hurtownia, "Id_hurtownia", "Nazwa");
            ViewBag.Id_lek = new SelectList(context.Lek, "Id_lek", "Nazwa");
            return View();
        }
        //[HttpGet]
        //public ActionResult CreateFacture()
        //{
        //    return View();
        //}
        
     //   public ActionResult Create([Bind(Include = "Date,Id_hurtownia, InvoiceId")] CreateInvoiceModel model)
        [HttpPost]
        public ActionResult Create(CreateInvoiceModel model)
   //     public ActionResult Create(int InvoiceId, List<Operacja> operations)       
       {
      //      System.Diagnostics.Debug.WriteLine(model.Date.ToString() + " " );
            return View();
        }
        [HttpGet]
        public JsonResult GetWarehouses()
        {
            var warehouses = context.Hurtownia.ToList();
            List<JsonWarehouse> misioweHurtownie = new List<JsonWarehouse>();

            foreach (var warehouse in warehouses)
            {
                var misiu = new JsonWarehouse();
                misiu.label = warehouse.Nazwa;
                misiu.value = warehouse.Id_hurtownia;
                misioweHurtownie.Add(misiu);
            }
           
            return new JsonResult
            {
                Data = misioweHurtownie,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                
            };
        }
        //[HttpPost]
        //public ActionResult Create(Faktura faktura)
        //{
        //    context.Faktura.Add(faktura);
        //    context.SaveChanges();
        //    var data = jsonFacture.Data;
        //    Faktura facture = new Faktura() { Numer = jsonFacture.nr, Hurtownia = jsonFacture.warehouseNr };

        //    return RedirectToAction("Index", "Home");
        //}

    }
}