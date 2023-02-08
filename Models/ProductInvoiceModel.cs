using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class ProductInvoiceMode
    {
        public string StrProdNumber { get; set; }
        public string StrProdDescription { get; set; }
        public int Qty { get; set; }
        public double Rate { get; set; }
        public double TotalAmount { get; set; }
        public bool IsTax { get; set; }
        public bool RegisteredBy { get; set; }
    }
}