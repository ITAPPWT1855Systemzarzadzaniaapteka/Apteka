using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apteka.Models {
    public class InvoiceProductModel {
        public int lp { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public string unit { get; set; }
        public float price { get; set; }
        public string vat { get; set; }
    }

    public class CreateInvoiceModel {
        [Required]
        [Display(Name = "Data")]
        public DateTime date { get; set; }

        [Required]
        [Display(Name = "Hurtownia")]
        public id warehouse { get; set; }

        [Required]
        [Display(Name = "Numer faktury")]
        public String invoiceId { get; set; }
        
        public List<InvoiceProductModel> Products { get { return _products; } }
        private List<InvoiceProductModel> _products = new List<InvoiceProductModel>();
    }
}