using Apteka.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.IO;

namespace Apteka.Controllers
{
    public class OrderController : Controller {
        aptekaEntities1 context = new aptekaEntities1();

        [HttpGet]
        public ActionResult Index() {
            var DateStart = DateTime.Today.AddDays(-5);

            var LowMeds = context.Lek
                .Where(m =>
                    m.Operacja.Sum(o => o.Przychod - o.Rozchod)
                    <= 3.0 * m.Operacja
                        .Where(o => o.Data > DateStart)
                        .Sum(o => o.Rozchod) / 5
                ).Select(i => new MedAvailability {
                    Id = i.Id_lek,
                    Nazwa = i.Nazwa,
                    Dawka = i.Dawka,
                    Opakowanie = i.Opakowanie.HasValue ? i.Opakowanie.Value : 0,
                    Stan = i.Operacja.Sum(o => o.Przychod - o.Rozchod),
                    Zapotrzebowanie = 1.0 * i.Operacja
                        .Where(o => o.Data > DateStart)
                        .Sum(o => o.Rozchod) / 5
                }).ToList();

            return View(LowMeds);
        }
        [HttpPost]
        public string UploadCSV()
        {
            var path = Server.MapPath("~/App_Data/cenniki");
            var file = Request.Files[0];
            file.SaveAs(Path.Combine(path, file.FileName));
            return "Added successfully contacts from " + file.FileName;
        }
    }
}
