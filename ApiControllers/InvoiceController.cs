using MVCPOS.DataLayer;
using MVCPOS.Entities;
using MVCPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCPOS.ApiControllers
{
    public class InvoiceController : ApiController
    {

        InvoiceRepository irepo = new InvoiceRepository();
        [HttpGet]
        public IEnumerable<InvoiceListModel>GetInvoices(string startdate, string enddate)
        {
            List<InvoiceListModel> list = new List<InvoiceListModel>();

            list = irepo.GetInvoiceList(startdate, enddate);

            return list;
        }


        [HttpGet]
        public InvoiceModel GetInvData(int id)
        {
            InvoiceModel objectInvoiceModel = new InvoiceModel();
            InvoiceModel data = irepo.GetInvData(id);

            objectInvoiceModel.strCompanyName = data.strCompanyName;
            objectInvoiceModel.strEmail = data.strEmail;
            objectInvoiceModel.strBillToAddress1 = data.strBillToAddress1;
            objectInvoiceModel.lngTerms = data.lngTerms;
            objectInvoiceModel.dtmDate = data.dtmDate;
            objectInvoiceModel.lngClientID = data.lngClientID;
            objectInvoiceModel.memInvMemo = data.memInvMemo;

            List<InvoiceDetailModel> list = new List<InvoiceDetailModel>();

            list = irepo.GetInvDetail(id);
            objectInvoiceModel.InvoiceDetail = list ;


            return objectInvoiceModel;
        }



        [HttpPost]
        public InvoiceModel SaveInvoice(InvoiceModel model)
        {

            model.dtmDate = DateTime.Now;
            model.dtmRegisteredDate = DateTime.Now;
            var save = irepo.SaveInvoice(model);
            return save;
        }

        CustomerRepository repo = new CustomerRepository();

        [HttpGet]
        public PaymentModel GetPaymentInvoice()
        {
            PaymentModel paymentModel = new PaymentModel();
            paymentModel = repo.GetInvoice(3);

            List<InvoiceDetail> listInvoiceDetail = repo.GetInvoiceDetail(Convert.ToInt32(paymentModel.InvoiceID));
            paymentModel.InvoiceAmount = GetTotalInvoiceAmount(listInvoiceDetail);
            paymentModel.TotalInvoiceAmount = paymentModel.TaxAmount + paymentModel.InvoiceAmount;
            paymentModel.Customers = repo.GetAllCustomer();
            paymentModel.Locations = repo.GetAllLocation();
            return paymentModel;
        }

        [HttpPost]
        public void SavePayment([FromBody]SavePayment data)
        {
            data.RegisteredDate = DateTime.Now;
            string[] invoiceSplit = data.InvoiceIDString.Split(' ');
            data.InvoiceID = Convert.ToInt64(invoiceSplit[1].Trim());
            repo.SavePayment(data);
        }
        public Double GetTotalInvoiceAmount(List<InvoiceDetail> list)
        {
            Double totalAmount = 0;
            if (list?.Count > 0)
            {
                foreach (var item in list)
                {
                    totalAmount += item.LineTotal;
                }
            }
            return totalAmount;
        }

    }
}
