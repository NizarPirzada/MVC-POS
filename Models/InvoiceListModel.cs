using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class InvoiceListModel
    {
        public int lngNumberID { get; set; }
        public DateTime dtmDate { get; set; }
        public DateTime dtmDueDate { get; set; }
        public string strPONumber { get; set; }
        public string strCompanyName { get; set; }
        public decimal TotalInv { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal TotalDue { get; set; }
      
    }
}