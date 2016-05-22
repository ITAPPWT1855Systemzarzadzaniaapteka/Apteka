﻿using System.Linq;
using System.Web.Mvc;
using Apteka.Models;
using Microsoft.AspNet.Identity;

namespace Apteka.Controllers 
{

    [Authorize]
    public class SaleController : Controller
    {
        aptekaEntities1 context = new aptekaEntities1();
        public ActionResult Index()
        {
            return View(context.Operacja
                .Where(i => i.Rozchod > 0)
                .OrderByDescending(i => i.Id_operacja)
                .Take(50)
                .ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(CreateInvoiceModel model)
        {
            model.Products.ForEach(i =>
                context.Operacja.Add(new Operacja {
                    Id_user = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                    Data = model.Date,
                    Id_lek = i.Id,
                    Rozchod = i.Quantity,
                    Przychod = 0,
                    Netto = i.Price,
                    Brutto = (1 + (i.Vat / 100)) * i.Price
                })
            );
            context.SaveChanges();
            ViewBag.Message = "Pomyślnie dodano sprzedaż";
            return View();
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