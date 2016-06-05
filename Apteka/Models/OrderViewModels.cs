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
}