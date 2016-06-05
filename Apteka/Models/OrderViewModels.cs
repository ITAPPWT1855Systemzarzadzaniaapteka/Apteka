using System.Collections.Generic;

namespace Apteka.Models {
    public class MedAvailability {
        public int Id;
        public string Nazwa;
        public string Dawka;
        public int Opakowanie;
        public string Postac;
        public int? Stan;
        public double? Zapotrzebowanie;
        public double DoZamowienia;
        public List<fileMed> Hurtownie;
    }

    public class fileMed {
        public string hurtownia;
        public string lek;
        public string postac;
        public List<int> dawka;
        public int ilosc;
        public float netto;
        public string opis;
    }

    public class ProFormProdukty {
        public string hurtownia { get; set; }
        public string lek { get; set; }
        public string postac { get; set; }
        public string dawka { get; set; }
        public string opakowanie { get; set; }
        public string cena { get; set; }
        public string opis { get; set; }
        public string ilosc { get; set; }
    }

    public class ProForm {
        public string name { get; set; }
        public List<ProFormProdukty> produkty { get; set; }
        public string suma { get; set; }
    }
}