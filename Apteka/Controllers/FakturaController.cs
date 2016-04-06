using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apteka.Models;
using System.IO;

namespace Apteka.Controllers
{
    public class FakturaController : Controller
    {
        aptekaEntities context = new aptekaEntities();
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
            //if (!ModelState.IsValid) {
            //    return View("Create", model);
            //}

            var sr = new StreamReader(Request.InputStream);
            string content = sr.ReadToEnd();
            System.Diagnostics.Debug.WriteLine(model.date.ToString() + " " + model.Products.Count);
            System.Diagnostics.Debug.WriteLine("Pirate!" + content);
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

    }
}