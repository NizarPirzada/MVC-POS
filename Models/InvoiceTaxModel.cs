using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class InvoiceTaxModel
    {
        public int lngID { get; set; }
        public int lngInvoice { get; set; }
        public string lngTax { get; set; }
        public string strTaxCode { get; set; }
        public int sngInvAmt { get; set; }
        public DateTime sngTaxableAmt { get; set; }
        public int sngExptAmt { get; set; }
        public string sngCalcTax { get; set; }
        public string dtmDateCalc { get; set; }
        public double sngRate { get; set; }
        
    }
}