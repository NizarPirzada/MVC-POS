using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class InvoiceDetailModel
    {
        public string StrProdNumber { get; set; }
        public string StrProdName { get; set; }
        public string strProduct { get; set; }
        public string strDescription { get; set; }
        public double dblQtyShipped { get; set; }
        public double dblPriceEach { get; set; }
        public double dblLineTotal { get; set; }
        public string strComment { get; set; }
        public bool ysnTaxable { get; set; }
    }
}