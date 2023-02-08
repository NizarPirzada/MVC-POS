using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class SaleInvoiceModel
    {
        public int lngNumberID { get; set; }
        public int lngClientID { get; set; }
        public DateTime dtmDate { get; set; }
        public DateTime dtmDueDate { get; set; }
        public DateTime dtmShipDate { get; set; }
        public string strContact { get; set; }
        public string strBillToAddress1 { get; set; }
        public string strBillToAddress2 { get; set; }
        public string strBillToCity { get; set; }
        public string strBillToState { get; set; }
        public string strBillToZip { get; set; }
        public string strShipToID { get; set; }
        public string strShipToAddress1 { get; set; }
        public string strShipToAddress2 { get; set; }
        public string strShipToCity { get; set; }
        public string strShipToState { get; set; }
        public string strShipToZip { get; set; }
        public string strPhone { get; set; }
        public string strFax { get; set; }
        public string strPONumber { get; set; }
        public int lngTerms { get; set; }
        public int lngShipVia { get; set; }
        public int lngSalesRep { get; set; }
        public string memInvIntMemo { get; set; }
        public string memInvMemo { get; set; }
        public bool ysnPrinted { get; set; }
        public DateTime dtmRegisteredDate { get; set; }
        public string strRegisteredBy { get; set; }
        public int lngOriginalEstimate { get; set; }
        public string strAR { get; set; }
        public int lngTax { get; set; }
        public int sngTaxAmt { get; set; }
        public bool ysnPOS { get; set; }
        public int lngPOSID { get; set; }
        public bool ysnToBeShipped { get; set; }
        public bool ysnShipped { get; set; }

        public string strCompanyName { get; set; }
        public double TotalAmount { get; set; }

    }
}