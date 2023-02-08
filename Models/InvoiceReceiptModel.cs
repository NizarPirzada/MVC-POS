using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class InvoiceReceiptModel
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyEamil { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyFooter { get; set; }
        public List<ReceiptItem> ReceiptItems { get; set; }
        public string InvoiceId { get; set; }
        public string InvoiceDate { get; set; }
        public List<ReceiptTax> Taxes { get; set; }
        public double SubTotalAmount { get; set; }
        public double TotalTaxAmount { get; set; }
        public double TotalAmount { get; set; }
    }

    public class ReceiptItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public class ReceiptTax
    {
        public string Name { get; set; }        
        public double Value { get; set; }
    }
}