using Apteka.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Apteka.Controllers
{
    [Authorize]
    public class ChartController : Controller {
        aptekaEntities1 context = new aptekaEntities1();

        [HttpGet]
        public JsonResult Index() {
            return Json("Hello", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SalesToday() {
            var rnd = new Random();
            Operacja a = new Operacja();
            var DateStart = DateTime.Today;
            return Json(context.Operacja
                .Where(i => i.Rozchod > 0 && i.Data >= DateStart)
                .OrderBy(i => i.Data)
                .Select(i => new { Data = i.Data, value = i.Netto * i.Rozchod })
                .ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EarningsDaily(int days = 7) {
            var DateStart = DateTime.Today.AddDays(-1 * days + 1);
            var groups = context.Operacja
                .Where(i => i.Data >= DateStart && i.Rozchod > 0)
                .OrderBy(i => i.Data)
                .GroupBy(i => DbFunctions.TruncateTime(i.Data))
                .Select(i => new { Key = i.Key, value = i.Sum(it => it.Rozchod * it.Netto)})
                .ToList();
            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ExpensesDaily(int days = 7) {
            var DateStart = DateTime.Today.AddDays(-1 * days + 1);
            var groups = context.Operacja
                .Where(i => i.Data >= DateStart && i.Przychod > 0)
                .OrderBy(i => i.Data)
                .GroupBy(i => DbFunctions.TruncateTime(i.Data))
                .Select(i => new { Key = i.Key, value = i.Sum(it => it.Przychod * it.Netto) })
                .ToList();
            return Json(groups, JsonRequestBehavior.AllowGet);
        }
    }
}
