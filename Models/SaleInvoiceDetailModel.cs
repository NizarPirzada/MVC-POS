using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class SaleInvoiceDetailModel
    {
        public int lngDetailID { get; set; }
        public int lngInvoice { get; set; }
        public string strProduct { get; set; }
        public string strDescription { get; set; }
        public double dblQtyShipped { get; set; }
        public double dblPriceEach { get; set; }
        public double dblDiscount { get; set; }
        public double dblLineTotal { get; set; }
        public DateTime dtmRegisteredDate { get; set; }
        public string strRegisteredBy { get; set; }
        public string strComment { get; set; }
        public string strSerialNumber { get; set; }
        public DateTime dtmServiceDate { get; set; }
        public string strGL { get; set; }
        public bool ysnTaxable { get; set; }
        public string strInvAcct { get; set; }
        public string strCGSAcct { get; set; }
        public int lngPOS { get; set; }
        public int lngPOSDetail { get; set; }
        public DateTime dtmDateEntered { get; set; }

    }
}