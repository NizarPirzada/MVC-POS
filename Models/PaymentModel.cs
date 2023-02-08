using MVCPOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class PaymentModel
    {
        public Int64 InvoiceID { get; set; }
        public Int64 ClientID { get; set; }
        public Double TaxAmount { get; set; }
        public Double InvoiceAmount { get; set; }
        public Double TotalInvoiceAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public List<CustomerModel> Customers { get; set; }
        public List<Location> Locations { get; set; }
    }
}