using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apteka.Models;

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