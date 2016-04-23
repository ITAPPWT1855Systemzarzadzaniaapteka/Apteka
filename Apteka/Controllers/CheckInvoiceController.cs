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
    public class CheckInvoiceController : Controller
    {
        private aptekaEntities1 db = new aptekaEntities1();

        // GET: CheckInvoice
        public ActionResult Index()
        {
            return View(db.Sprawdz_faktury.ToList());
        }

        // GET: CheckInvoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprawdz_faktury sprawdz_faktury = db.Sprawdz_faktury.Find(id);
            if (sprawdz_faktury == null)
            {
                return HttpNotFound();
            }
            return View(sprawdz_faktury);
        }

    }
}
