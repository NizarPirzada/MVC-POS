using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Entities
{
    public class SavePayment
    {
        public string InvoiceIDString { get; set; }
        public Int64 InvoiceID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public Double PaymentAmount { get; set; }
        public Double NetAmount { get; set; }
        public string RegisteredBy { get; set; }
        public DateTime RegisteredDate { get; set; }

    }
}