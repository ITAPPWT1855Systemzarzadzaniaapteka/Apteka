using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apteka.Models;
using System.IO;
using Newtonsoft.Json;

namespace Apteka.Controllers 
{

    [Authorize]
    public class InvoiceController : Controller
    {
        aptekaEntities1 context = new aptekaEntities1();
        public ActionResult Index()
        {
            ViewBag.Operacje = db.Operacja.Include(f => f.Lek).Limit(20).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(CreateInvoiceModel model)
        {
            return Json(model);
        }

        [HttpGet]
        public ActionResult GetMedicines()
        {
            var medicines = context.Lek.ToList()
                .Select(i => new { i.Id_lek, i.Nazwa, i.Postac, i.Opakowanie})
                .ToList();
            return Json(medicines, JsonRequestBehavior.AllowGet);
        }
    }
}