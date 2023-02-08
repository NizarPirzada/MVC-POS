using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class CustomerModel
    {
        public int lngCustID { get; set; }
        public string strCompanyName { get; set; }
        public string strContact { get; set; }
        public string strBillToAddress1 { get; set; }
        public string strBillToAddress2 { get; set; }
        public string strBillToCity { get; set; }
        public string strBillToZip { get; set; }
        public string strBillToState { get; set; }
        public string strShipToAddress1 { get; set; }
        public string strShipToAddress2 { get; set; }
        public string strShipToCity { get; set; }
        public string strShipToState { get; set; }
        public string strShipToZip { get; set; }
        public string strEmail { get; set; }
        public int lngDefSalesRep { get; set; }
        public int lngDefShipVia { get; set; }
        public int lngDefTerms { get; set; }
        public bool ysnInactive { get; set; }
        public DateTime? dtmRegisteredDate { get; set; }
        public string strRegisteredBy { get; set; }
        public string strWebAddress{ get; set; }
        public bool ysnCreditLimit { get; set; }
        public decimal curCreditLimit { get; set; }
        public string strDefGLAcct { get; set; }
        public string strPOSMessage { get; set; }
        public DateTime? dtmTimeStamp { get; set; }

    }
}