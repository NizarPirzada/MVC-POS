using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPOS.Models
{
    public class InvoiceModel
    {
        public int lngNumberID { get; set; }
        public int lngClientID { get; set; }
        public string strContact { get; set; }
        public string strBillToAddress1 { get; set; }
        public int lngTerms { get; set; }
        public DateTime dtmDate { get; set; }
        public int lngInvoice { get; set; }
        public string strProduct { get; set; }
        public string strDescription { get; set; }
        public double dblQtyShipped { get; set; }
        public double dblPriceEach { get; set; }
        public double dblLineTotal { get; set; }
        public bool ysnTaxable { get; set; }
        public int? lngTax { get; set; }
        public double sngTaxAmt { get; set; }
        public string memInvMemo { get; set; }
        public string strCompanyName { get; set; }
        public string strEmail { get; set; }
        public DateTime dtmRegisteredDate { get; set; }
        public string   strRegisteredBy { get; set; }


        public List<ProductModel> Products { get; set; }
        public List<InvoiceDetailModel> InvoiceDetail { get; set; }
        //   public ProductModel Products {get;set;}

    }
}