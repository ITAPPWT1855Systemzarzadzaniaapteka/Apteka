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
        public ActionResult SellPlot()
        {
          //  var groupByDayOperations = db.Operacja.GroupBy(x => x.Data.ToString("dd/M/yyyy")).First();
            List<Operacja> JsonObject = db.Operacja.ToList();

            var warehouses = db.Operacja.ToList();
            List<JsonOperationSell> misioweHurtownie = new List<JsonOperationSell>();

            foreach (var warehouse in warehouses)
            {
                var misiu = new JsonOperationSell();
                misiu.day = warehouse.Data;
                misiu.value = warehouse.Przychod;
                misioweHurtownie.Add(misiu);
            }

            return View(misioweHurtownie);
        }
    }
}