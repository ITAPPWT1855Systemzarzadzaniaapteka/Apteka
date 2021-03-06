﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apteka.Models {
    public class InvoiceProductModel {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; } //netto per each
        public string Vat { get; set; }
    }

    public class CreateInvoiceModel {
        [Required]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Hurtownia")]
        public int WarehouseId { get; set; } //Warehouse id

        [Required]
        [Display(Name = "Numer faktury")]
        public String InvoiceId { get; set; }
        //This id is not always a number, most likely it looks like "01/2016" but it depends
        public List<InvoiceProductModel> Products { get { return _products; } }
        private List<InvoiceProductModel> _products = new List<InvoiceProductModel>();
    }
}