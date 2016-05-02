//using Apteka.Models;
//using ClosedXML.Excel;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace Apteka.Controllers
//{
//    public class GenerateXLSXFile
//    {
//        public byte[] Generate(List<Dictionary<string, string>> list, string[] highlightColums = null, string horizontalSeparator = null)
//        {
//            //takes as input list of dictionaries and return as excel table ready to download, as byte array
//            XLWorkbook workbook = new XLWorkbook();
//            var worksheet = workbook.Worksheets.Add("Raport-" + DateTime.Now.Date.ToString("dd-MM-yyyy"));

//            var highlighColumsIndexes = new List<int>();
//            int separatorColumnIndex = -1;
//            if (list.Count > 0)
//            {
//                //Header
//                int c = 1;
//                foreach (var element in list[0])
//                {
//                    worksheet.Cell(2, c).SetValue(element.Key);
//                    if (highlightColums != null)
//                    {
//                        if (Array.IndexOf(highlightColums, element.Key) != -1)
//                        {
//                            highlighColumsIndexes.Add(c);
//                        }
//                    }
//                    if (horizontalSeparator != null)
//                    {
//                        if (horizontalSeparator == element.Key)
//                        {
//                            separatorColumnIndex = c;
//                        }
//                    }
//                    c++;
//                }


//                //Data
//                int r = 3;
//                foreach (var l in list)
//                {
//                    c = 1;
//                    foreach (var element in l)
//                    {
//                        worksheet.Cell(r, c).SetValue(element.Value);
//                        c++;
//                    }
//                    r++;
//                }

//                styling(worksheet, list[0].Count, list.Count, highlighColumsIndexes);
//                if (separatorColumnIndex > 0)
//                    addSeparators(worksheet, list[0].Count, list.Count, separatorColumnIndex);

//                //Pre
//                worksheet.Cell(1, 1).SetValue("Raport wygenerowany " + DateTime.Now.ToString());
//            }
//            else
//            {
//                worksheet.Cell(1, 1).SetValue("Brak danych do wyswietlenia");
//            }
//            using (MemoryStream memoryStream = new MemoryStream())
//            {
//                workbook.SaveAs(memoryStream);

//                return memoryStream.ToArray();
//            }
//        }
//        private void addSeparators(IXLWorksheet ws, int width, int heigth, int separatorIndex)
//        {
//            string lastValue = "";
//            string currentValue = "";
//            for (int r = 3; r < heigth + 3; r++)
//            {
//                currentValue = ws.Cell(r, separatorIndex).Value.ToString();
//                if (lastValue != currentValue)
//                {
//                    ws.Range(r, 1, r, width).Style.Border.TopBorder = XLBorderStyleValues.Medium;
//                    ws.Range(r, 1, r, width).Style.Border.TopBorderColor = XLColor.DarkBlue;
//                }
//                lastValue = currentValue;
//            }
//        }

//        private void styling(IXLWorksheet ws, int width, int heigth, List<int> columns)
//        {
//            ws.Style.Fill.BackgroundColor = OutsideBackgroundColor;
//            ws.Range(1, 1, 1, width).Style.Fill.BackgroundColor = XLColor.White;

//            int rFirst = 2;
//            IXLRange RangeHeader = ws.Range(rFirst, 1, rFirst, width);
//            IXLRange RangeData = ws.Range(rFirst + 1, 1, heigth + rFirst, width);
//            IXLRange RangeTable = ws.Range(rFirst, 1, heigth + rFirst, width);
//            RangeHeader.Style.Fill.BackgroundColor = HeaderBackgroundColor;
//            RangeHeader.Style.Font.Bold = true;
//            RangeHeader.Style.Font.FontColor = HeaderTextColor;
//            RangeHeader.Style.Border.BottomBorder = XLBorderStyleValues.Thick;

//            for (int i = 1; i <= width; i++)
//            {
//                if (i % 2 == 0)
//                    ws.Range(rFirst + 1, i, heigth + rFirst, i).Style.Fill.BackgroundColor = TableBackgroundColor1;
//                else
//                    ws.Range(rFirst + 1, i, heigth + rFirst, i).Style.Fill.BackgroundColor = TableBackgroundColor2;
//            }

//            foreach (int c in columns)
//            {
//                ws.Range(rFirst + 1, c, heigth + rFirst, c).Style.Fill.BackgroundColor = TableHighlightColor;
//                ws.Range(rFirst + 1, c, heigth + rFirst, c).Style.Font.Bold = true;
//            }

//            RangeData.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
//            RangeData.Style.Border.InsideBorderColor = TableBorderColor;

//            ws.Columns(1, width).AdjustToContents();
//            RangeTable.CreateTable();



//        }

//        public XLColor OutsideBackgroundColor { get; set; } = XLColor.FromHtml("#cccccc");
//        public XLColor HeaderBackgroundColor { get; set; } = XLColor.FromHtml("#9DA1B2");
//        public XLColor HeaderTextColor { get; set; } = XLColor.FromHtml("#222222");
//        public int HeaderBorderWidth { get; set; } = 1;
//        public XLColor TableBackgroundColorFirstCol { get; set; } = XLColor.FromHtml("#BFCCB3");
//        public XLColor TableBackgroundColor1 { get; set; } = XLColor.FromHtml("#FAFeFF");
//        public XLColor TableBackgroundColor2 { get; set; } = XLColor.FromHtml("#FFf7E3");
//        public XLColor TableHighlightColor { get; set; } = XLColor.FromHtml("#aFE3c7");
//        public XLColor TableBorderColor { get; set; } = XLColor.FromHtml("#aDc1d2");
//    }

//    [Authorize]
//    public class ReportController : Controller
//    {
//        // GET: Report
//        public ActionResult Index()
//        {

//            return View();
//        }


//        //Raport stan magazynu
//        public ActionResult Store()
//        {
//            var data = new List<Stan_magazynu>();

//            aptekaEntities1 context = new aptekaEntities1();

//            var output = from p in context.Stan_magazynu select p;
//            data = output.ToList();

//            var dataDict = new List<Dictionary<string, string>>();
//            foreach (Stan_magazynu sm in data)
//            {
//                var d = new Dictionary<string, string>();
//                d["Id_lek"] = sm.Id_lek.ToString();
//                d["Nazwa"] = sm.Nazwa;
//                d["Opakowanie"] = sm.Opakowanie.ToString();
//                d["Dawka"] = sm.Dawka;
//                d["Postać"] = sm.Postac;
//                d["Obecny stan magazynu"] = sm.Obecny_Stan_Magazynu.ToString();
//                dataDict.Add(d);
//            }
//            var generator = new GenerateXLSXFile();

//            return File(generator.Generate(dataDict, new[] { "Obecny stan magazynu" }), "application/xlsx", "Raport-Stan-Magazynu_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
//        }

//        public ActionResult Medicine()
//        {
//            var data = new List<Lek>();

//            aptekaEntities1 context = new aptekaEntities1();
//            data = context.Lek.ToList();

//            var dataDict = new List<Dictionary<string, string>>();
//            foreach (Lek l in data)
//            {
//                var d = new Dictionary<string, string>();
//                d["Id_lek"] = l.Id_lek.ToString();
//                d["Nazwa"] = l.Nazwa;
//                d["Opakowanie"] = l.Opakowanie.ToString();
//                d["Dawka"] = l.Dawka;
//                d["Postać"] = l.Postac;

//                dataDict.Add(d);
//            }
//            var generator = new GenerateXLSXFile();

//            return File(generator.Generate(dataDict), "application/xlsx", "Raport-Leki_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
//        }

//        //Raport sprzedazy lekow
//        public ActionResult SellHistory(DateTime dateFrom, DateTime dateTo)
//        {
//            aptekaEntities1 context = new aptekaEntities1();

//            var data = context.Operacja.Where(o => o.Rozchod != 0)
//            .Join(context.Lek, o => o.Id_lek, l => l.Id_lek, (o, l) => new
//            {
//                Data = o.Data,
//                Nazwa = l.Nazwa,
//                Postac = l.Postac,
//                Dawka = l.Dawka,
//                Opakowanie = l.Opakowanie,
//                Rozchod = o.Rozchod,
//                Netto = o.Netto,
//                Brutto = o.Brutto
//            }).ToList()
//            .Where(o => (o.Data >= dateFrom) && (o.Data <= dateTo));

//            var dataDict = new List<Dictionary<string, string>>();
//            foreach (var sell in data)
//            {
//                var d = new Dictionary<string, string>();
//                d["Data"] = sell.Data.Value.ToString("dd.MM.yyyy");
//                d["Nazwa"] = sell.Nazwa;
//                d["Postać"] = sell.Postac;
//                d["Dawka"] = sell.Dawka;
//                d["Opakowanie"] = sell.Opakowanie.ToString();
//                d["Sprzedanych"] = sell.Rozchod.ToString();
//                d["Cena netto"] = sell.Netto.ToString();
//                d["Cena brutto"] = sell.Brutto.ToString();
//                dataDict.Add(d);
//            }
//            var generator = new GenerateXLSXFile();

//            return File(generator.Generate(dataDict, new[] { "Sprzedanych", "Cena netto", "Cena brutto" }, "Data"), "application/xlsx", "Raport-Sprzedaz-Lekow_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
//        }

//      //Raport sprzedazy
//        public ActionResult SellSummary(DateTime dateFrom, DateTime dateTo)
//        {
//            aptekaEntities1 context = new aptekaEntities1();

//            var data = context.Operacja.Where(o => o.Rozchod != 0)
//            .GroupBy(o => o.Data)
//            .Select(o => new {
//                Data = o.FirstOrDefault().Data,
//                NettoDzienne = o.Sum(s => s.Netto),
//                BruttoDzienne = o.Sum(s => s.Brutto)
//            })
//            .ToList()
//            .Where(o => (o.Data >= dateFrom) && (o.Data <= dateTo));

//            var dataDict = new List<Dictionary<string, string>>();
//            foreach (var sell in data)
//            {
//                var d = new Dictionary<string, string>();
//                d["Data"] = sell.Data.Value.ToString("dd.MM.yyyy");
//                d["Kwota Netto"] = sell.NettoDzienne.ToString();
//                d["Kwota Brutto"] = sell.BruttoDzienne.ToString();

//                dataDict.Add(d);
//            }
//            var generator = new GenerateXLSXFile();

//            return File(generator.Generate(dataDict), "application/xlsx", "Raport-Sprzedaz_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
//        }


//        //Raport kupna lekow
//        public ActionResult BuyHistory(DateTime dateFrom, DateTime dateTo)
//        {
//            aptekaEntities1 context = new aptekaEntities1();

//            var data = context.Operacja.Where(o => o.Przychod != 0)
//            .Join(context.Lek, o => o.Id_lek, l => l.Id_lek, (o, l) => new
//            {
//                Id_operacja = o.Id_operacja,
//                Data = o.Data,
//                Nazwa = l.Nazwa,
//                Postac = l.Postac,
//                Dawka = l.Dawka,
//                Opakowanie = l.Opakowanie,
//                Przychod = o.Przychod,
//                Netto = o.Netto,
//                Brutto = o.Brutto,
//                Faktura = o.Faktura
//            })
//            .ToList()
//            .Where(o => (o.Data >= dateFrom) && (o.Data <= dateTo));

//            var dataDict = new List<Dictionary<string, string>>();
//            foreach (var buy in data)
//            {
//                var d = new Dictionary<string, string>();
//                d["Data"] = buy.Data.Value.ToString("dd.MM.yyyy");
//                d["Nr faktury"] = buy.Faktura.ToArray()[0].Numer;
//                d["Hurtownia"] = buy.Faktura.ToArray()[0].Hurtownia.Nazwa;
//                d["NIP"] = buy.Faktura.ToArray()[0].Hurtownia.NIP.ToString();
//                d["Nazwa"] = buy.Nazwa;
//                d["Postać"] = buy.Postac;
//                d["Dawka"] = buy.Dawka;
//                d["Opakowanie"] = buy.Opakowanie.ToString();
//                d["Kupionych"] = buy.Przychod.ToString();
//                d["Cena netto"] = buy.Netto.ToString();
//                d["Cena brutto"] = buy.Brutto.ToString();
//                dataDict.Add(d);
//            }
//            var generator = new GenerateXLSXFile();

//            return File(generator.Generate(dataDict, new[] { "Kupionych", "Cena netto", "Cena brutto" }, "Nr faktury"), "application/xlsx", "Raport-Kupno-lekow_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
//        }

//        //Raport kupna
//        public ActionResult BuySummary(DateTime dateFrom, DateTime dateTo)
//        {
//            aptekaEntities1 context = new aptekaEntities1();

//            var data = context.Operacja.Where(o => o.Przychod != 0)
//            .Select(
//            o => new
//            {
//                Data = o.Data,
//                Id_faktura = o.Faktura.FirstOrDefault().Id_faktura,
//                Hurtownia=o.Faktura.FirstOrDefault().Hurtownia,
//                Netto=o.Netto,
//                Brutto=o.Brutto
//            })
//            .GroupBy(o => o.Id_faktura)
//            .Select(o => new {
//                Data = o.FirstOrDefault().Data,
//                Hurtownia = o.FirstOrDefault().Hurtownia.Nazwa,
//                NettoDzienne = o.Sum(s => s.Netto),
//                BruttoDzienne = o.Sum(s => s.Brutto)
//            })
//            .ToList()
//            .Where(o => (o.Data >= dateFrom) && (o.Data <= dateTo));

//            var dataDict = new List<Dictionary<string, string>>();
//            foreach (var sell in data)
//            {
//                var d = new Dictionary<string, string>();
//                d["Data"] = sell.Data.Value.ToString("dd.MM.yyyy");
//                d["Hurtownia"] = sell.Hurtownia;
//                d["Kwota Netto"] = sell.NettoDzienne.ToString();
//                d["Kwota Brutto"] = sell.BruttoDzienne.ToString();
//                dataDict.Add(d);
//            }
//            var generator = new GenerateXLSXFile();

//            return File(generator.Generate(dataDict,horizontalSeparator:"Data"), "application/xlsx", "Raport-Kupno_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
//        }

        
//    }
//}
