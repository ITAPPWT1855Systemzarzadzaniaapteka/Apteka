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
//        public byte[] Generate(List<Dictionary<string, string>> list)
//        {
//            //takes as input list of dictionaries and return as excel table ready to download, as byte array
//            XLWorkbook workbook = new XLWorkbook();
//            var worksheet = workbook.Worksheets.Add("Raport-" + DateTime.Now.Date.ToString("dd-MM-yyyy"));

//            if (list.Count > 1)
//            {
//                //Header
//                int c = 1;
//                foreach (var element in list[0])
//                {
//                    worksheet.Cell(2, c).SetValue(element.Key);
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

//                styling(worksheet, list[0].Count, list.Count);

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

//        private void styling(IXLWorksheet ws, int width, int heigth)
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
//        public XLColor TableBorderColor { get; set; } = XLColor.FromHtml("#aDc1d2");
//    }

//    public class ReportController : Controller
//    {
//        // GET: Report
//        public ActionResult Index()
//        {
//            List<Faktura> lista;

//            return View();
//        }

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
//                d["ID_lek"] = sm.ID_lek.ToString();
//                d["Nazwa"] = sm.Nazwa;
//                d["Opakowanie"] = sm.Opakowanie.ToString();
//                d["Dawka"] = sm.Dawka;
//                d["Postać"] = sm.Postac;
//                d["Obecny stan magazynu"] = sm.Obecny_Stan_Magazynu.ToString();
//                dataDict.Add(d);
//            }
//            var generator = new GenerateXLSXFile();

//            return File(generator.Generate(dataDict), "application/xlsx", "Raport-Stan-Magazynu_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
//        }

//        public ActionResult CheckInvoices()
//        {
//            var data = new List<Sprawdz_faktury>();

//            aptekaEntities1 context = new aptekaEntities1();
                
//            var output = from p in context.Sprawdz_faktury select p;
//            data = output.ToList();
              
//            var dataDict = new List<Dictionary<string, string>>();
//            foreach (Sprawdz_faktury inv in data)
//            {
//                var d = new Dictionary<string, string>();
//                d["ID_Faktura"] = inv.ID_faktura.ToString();
//                d["ID_Lek"] = inv.Netto.ToString();
//                d["Nazwa leku"] = inv.Nazwa;
                
//                dataDict.Add(d);
//            }
//            var generator = new GenerateXLSXFile();

//            return File(generator.Generate(dataDict), "application/xlsx", "Raport-Faktury_" + DateTime.Now.Date.ToString("dd - MM - yyyy") + ".xlsx");
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
//                d["ID_lek"] = l.Id_lek.ToString();
//                d["Nazwa"] = l.Nazwa;
//                d["Opakowanie"] = l.Opakowanie.ToString();
//                d["Dawka"] = l.Dawka;
//                d["Postać"] = l.Postac;

//                dataDict.Add(d);
//            }
//            var generator = new GenerateXLSXFile();

//            return File(generator.Generate(dataDict), "application/xlsx", "Raport-Leki_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
//        }

//        public ActionResult SellHistory(string dateFrom, string dateTo)
//        {
//            DateTime _dateFrom = DateTime.ParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture);
//            DateTime _dateTo = DateTime.ParseExact(dateTo, "dd.MM.yyyy", CultureInfo.InvariantCulture);
//            aptekaEntities1 context = new aptekaEntities1();

//            var data = context.Operacjas.Where(o => o.Rozchod != 0)
//            .Join(context.Lek, o => o.ID_lek, l => l.Id_lek, (o, l) => new
//            {
//                Data = o.Data,
//                Nazwa = l.Nazwa,
//                Postac = l.Postac,
//                Dawka = l.Dawka,
//                Opakowanie = l.Opakowanie,
//                Rozchod = o.Rozchod,
//                Przychod = o.Przychod,

//            }).ToList()
//            .Where(o => (DateTime.ParseExact(o.Data, "dd.MM.yyyy", CultureInfo.InvariantCulture) > _dateFrom) && (DateTime.ParseExact(o.Data, "dd.MM.yyyy", CultureInfo.InvariantCulture) < _dateTo));

//            var dataDict = new List<Dictionary<string, string>>();
//            foreach (var sell in data)
//            {
//                var d = new Dictionary<string, string>();
//                d["Data"] = sell.Data.ToString();
//                d["Nazwa"] = sell.Nazwa;
//                d["Postać"] = sell.Postac;
//                d["Dawka"] = sell.Dawka;
//                d["Opakowanie"] = sell.Opakowanie.ToString();
//                d["Sprzedanych"] = sell.Rozchod.ToString();
//                d["aa"] = sell.Przychod.ToString();
//                dataDict.Add(d);
//            }
//            var generator = new GenerateXLSXFile();

//            return File(generator.Generate(dataDict), "application/xlsx", "Raport-Sprzedaz_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx");
//        }

//    }
//}
