using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Entities
{
    public class InvoiceDetail
    {
        public Int64 InvoiceID { get; set; }
        public Int64 InvoiceDetailID { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public int QuantityShipped { get; set; }
        public Double PriceEach { get; set; }
        public Double Discount { get; set; }
        public Double LineTotal { get; set; }
    }
}