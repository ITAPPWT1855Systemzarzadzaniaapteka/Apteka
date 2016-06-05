using Apteka.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Text;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.IO;
using LinqToExcel;
using LinqToExcel.Domain;
using System.Text.RegularExpressions;
using System.Globalization;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using MinimumEditDistance;
using System.Web;
using Apteka.Helpers;

namespace Apteka.Controllers {
    public class OrderController : Controller {
        aptekaEntities1 context = new aptekaEntities1();

        [HttpGet]
        public ActionResult Index() {
            var DateStart = DateTime.Today.AddDays(-5);

            double weatherRatio = calcWeatherRadio();

            var LowMeds = context.Lek
                .Where(m =>
                    m.Operacja.Sum(o => o.Przychod - o.Rozchod)
                    <= weatherRatio * 3.0 * m.Operacja
                        .Where(o => o.Data > DateStart)
                        .Sum(o => o.Rozchod) / 5
                ).Select(i => new MedAvailability {
                    Id = i.Id_lek,
                    Nazwa = i.Nazwa,
                    Dawka = i.Dawka,
                    Postac = i.Postac,
                    Opakowanie = i.Opakowanie.HasValue ? i.Opakowanie.Value : 0,
                    Stan = i.Operacja.Sum(o => o.Przychod - o.Rozchod),
                    Zapotrzebowanie = weatherRatio * 1.0 * i.Operacja
                        .Where(o => o.Data > DateStart)
                        .Sum(o => o.Rozchod) / 5
                })
                .ToList().Select(i => {
                    i.Zapotrzebowanie = Math.Ceiling(i.Zapotrzebowanie.GetValueOrDefault(0));
                    return i;
                });


            try {
                var json = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/cennik.json"));
                var list = new JavaScriptSerializer().Deserialize<List<fileMed>>(json);

                LowMeds = LowMeds.Select(lek => {
                    lek.Hurtownie = findMatches(lek, list);
                    return lek;
                }).ToList();
            }
            catch (Exception) {
                //Not so bad;
            }

            return View(LowMeds);
        }

        private List<fileMed> findMatches(MedAvailability med, List<fileMed> list) {
            var numberRgx = new Regex("[0-9]+");
            var medName = med.Nazwa.Split(' ').First();
            int dawka;
            try {
                dawka = med.Dawka.ToLower().Split('/').First().Replace(",", ".").Replace("(", "").Replace(")", "").Split('+').Select(i => ToCommonUnit(i)).First();
            }
            catch (Exception) {
                dawka = 0;
            }
            return list.Where(i =>
                dawka == i.dawka.First() &&
                med.Opakowanie == i.ilosc &&
                ((float)MinimumEditDistance.Levenshtein.CalculateDistance(i.lek, medName, 1) / med.Nazwa.Length) <= 0.5 &&
                ((float)MinimumEditDistance.Levenshtein.CalculateDistance(i.postac, med.Postac, 1) / med.Postac.Length) <= 0.58
            ).ToList();
        }

        public JsonResult transformExcel() {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            using (var file = System.IO.File.OpenRead(Server.MapPath("~/App_Data/cenniki/Cenniki_znormalizowane.csv"))) {
                var reader = new StreamReader(file);
                List<fileMed> listA = new List<fileMed>();
                reader.ReadLine();//Skip top
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    values[1] = values[1];
                    var lek = rgx.Replace(values[1].ToLower(), "").Split(' ').First();
                    var postac = values[2].ToLower().Replace("-", "");
                    List<int> dawka;
                    int ilosc;
                    float netto;
                    try {
                        dawka = values[3].ToLower().Split('/').First().Replace(",", ".").Replace("(", "").Replace(")", "").Split('+').Select(i => ToCommonUnit(i)).ToList();
                        ilosc = int.Parse(values[4].ToLower().Split(' ').First(), CultureInfo.InvariantCulture);
                        netto = float.Parse(values[5].ToLower().Replace(",", ".").Split(' ').First(), CultureInfo.InvariantCulture);
                    }
                    catch (Exception) {
                        continue;
                    }
                    var med = new fileMed {
                        hurtownia = values[0].ToLower(),
                        lek = lek,
                        postac = postac,
                        dawka = dawka,
                        ilosc = ilosc,
                        netto = netto,
                        opis = values[0] + ": " + values[1] + "(" + values[2] + ", " + values[3] + ", " + values[4] + ") - " + values[5]
                    };
                    listA.Add(med);
                }
                string json = JsonConvert.SerializeObject(listA);
                System.IO.File.WriteAllText(Server.MapPath("~/App_Data/cennik.json"), json);
                return Json(new { result = "success", count = listA.Count }, JsonRequestBehavior.AllowGet);
            }
        }

        private int ToCommonUnit(string i) { //uq
            var rgx = new Regex("[a-zA-Z]+$");
            var nbrRgx = new Regex("[0-9]+");
            var num = float.Parse(nbrRgx.Match(i).ToString(), CultureInfo.InvariantCulture);
            var unit = rgx.Match(i.Replace(" ", "")).ToString();
            System.Diagnostics.Debug.WriteLine("Unit :" + unit + ";");
            if (unit.Equals("mg"))
                return (int)(num * 1000);
            if (unit.Equals("g"))
                return (int)(num * 1000000);
            return (int)num;
        }

        [HttpPost]
        public string UploadCSV() {
            var path = Server.MapPath("~/App_Data/cenniki");
            var file = Request.Files[0];
            file.SaveAs(Path.Combine(path, file.FileName));

            return "Added successfully contacts from " + file.FileName;
        }

        [HttpPost]
        public ActionResult Proform(ProForm pro) {
            return View(pro);
        }

        private double calcWeatherRadio() {
            var w = new WeatherHelper();
            double ratio = 0;
            var a = -0.16 * Math.Log(w.Temperatures.Average()) + 1.33;
            var b = 0.44 * Math.Exp(w.Rain.Average() * 0.0131);
            var c = 6.67 * Math.Exp(w.Pressure.Average() * -0.002);
            ratio += a;
            ratio += b;
            ratio += c;
            return ratio / 3;
        }
    }
}
