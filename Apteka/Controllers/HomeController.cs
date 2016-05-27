using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apteka.Helpers;
using Newtonsoft.Json;
using Apteka.Models;

namespace Apteka.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private WeatherHelper weatherHelper = new WeatherHelper();
        private aptekaEntities1 db = new aptekaEntities1();
        public ActionResult Index()
        {
            ViewBag.stores = db.Stan_magazynu.Take(5).OrderBy(x => x.Obecny_Stan_Magazynu);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Weather()
        {
            CityWeather JsonObject = weatherHelper.CityWeather;
            return View(JsonObject);
        }

        public ActionResult Error()
        {
            return View();
        }

        //public PartialViewResult _MessagesPartial()
        //{
        //   var stores = db.Stan_magazynu.Take(5).OrderBy(x=>x.Obecny_Stan_Magazynu);
        //   return PartialView("~/Shared/_MessagesPartial.cshtml", stores);
        //}
    }
}