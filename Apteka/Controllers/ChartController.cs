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
            var DateStart = DateTime.Today;
            var DateEnd = DateTime.Now;
            var groups = context.Operacja
                .Where(i => i.Data >= DateStart && i.Rozchod > 0)
                .OrderBy(i => i.Data)
                .GroupBy(i => DbFunctions.DiffHours(DateStart, i.Data))
                .Select(i => new { Key = i.Key, value = i.Sum(it => it.Rozchod * it.Netto) })
                .ToDictionary(i => i.Key, i => i.value);

            var result = Enumerable.Range(0, (int)(DateEnd - DateStart).TotalHours).Select(h => new { Date = DateStart.AddHours(h), Count = groups.ContainsKey(h) ? groups[h] : 0 });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EarningsDaily(int days = 7) {
            var DateStart = DateTime.Today.AddDays(-1 * days + 1);
            var DateEnd = DateTime.Today;
            var groups = context.Operacja
                .Where(i => i.Data >= DateStart && i.Rozchod > 0)
                .OrderBy(i => i.Data)
                .GroupBy(i => DbFunctions.TruncateTime(i.Data))
                .Select(i => new { Key = i.Key, value = i.Sum(it => it.Rozchod * it.Netto)})
                .ToDictionary(i => i.Key, i => i.value);
            
            var result = Enumerable.Range(0, (int)(DateEnd - DateStart).TotalDays).Select(h => new { Date = DateStart.AddDays(h), Count = groups.ContainsKey(DateStart.AddDays(h)) ? groups[DateStart.AddDays(h)] : 0 });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ExpensesDaily(int days = 7) {
            var DateStart = DateTime.Today.AddDays(-1 * days + 1);
            var DateEnd = DateTime.Today;
            var groups = context.Operacja
                .Where(i => i.Data >= DateStart && i.Przychod > 0)
                .OrderBy(i => i.Data)
                .GroupBy(i => DbFunctions.TruncateTime(i.Data))
                .Select(i => new { Key = i.Key, value = i.Sum(it => it.Przychod * it.Netto) })
                .ToDictionary(i => i.Key, i => i.value);

            var result = Enumerable.Range(0, (int)(DateEnd - DateStart).TotalDays).Select(h => new { Date = DateStart.AddDays(h), Count = groups.ContainsKey(DateStart.AddDays(h)) ? groups[DateStart.AddDays(h)] : 0 });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
