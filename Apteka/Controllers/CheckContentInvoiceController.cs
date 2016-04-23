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


    }
}
