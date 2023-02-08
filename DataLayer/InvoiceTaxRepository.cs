using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCPOS.Models;
using MVCPOS.DataLayer;
using System.Data.SqlClient;
using System.Data;
using MVCPOS.Entities;

namespace MVCPOS.DataLayer
{
    public class InvoiceTaxRepository
    {
        InvoiceListModel model = new InvoiceListModel();
        DataAccessLayer dal = new DataAccessLayer();
        InvoiceDetailModel inmodel = new InvoiceDetailModel();
        

        public InvoiceModel GetInvForTax(int invoiceId)
        {
            string query = "select * from tblSLInv where lngNumberID= '" + invoiceId + "' ";
            dal.Db = "Local";
            return GetInvoiceForTaxData(query);
        }


        //SELECT * FROM tblSLInvTax where lngInvoice=3

        //getinvdetail
        public List<InvoiceDetailModel>GetInvDetail(int id)
        {
            string query = "select tblSLInvDetail.strDescription, tblSLInvDetail.dblQtyShipped, tblSLInvDetail.dblPriceEach, tblSLInvDetail.dblLineTotal, tblSLInvDetail.ysnTaxable, tblProducts.strProdName from tblSLInvDetail inner join tblProducts on tblSLInvDetail.strProduct = tblProducts.strProdNumber where lngInvoice = '"+id+"'  ";
            dal.Db = "Local";
            return GetInvoieDetailList(query);
        }


  


        public List<InvoiceListModel> GetReceiptInvoice(string startdate, string enddate)
        {
            var query = "SELECT tblSLInv.lngNumberID, tblSLInv.dtmDate, tblSLInv.dtmDueDate, tblSLInv.strPONumber, tblCust.strCompanyName, qryInvDue.TotalInv, qryInvDue.TotalPayments, qryInvDue.TotalDue FROM tblCust INNER JOIN(qryInvDue RIGHT JOIN tblSLInv ON qryInvDue.lngNumberID = tblSLInv.lngNumberID) ON tblCust.lngCustID = tblSLInv.lngClientID WHERE tblSLInv.dtmDate >= '" + startdate + "' And tblSLInv.dtmDate <= '" + enddate + "'";
            dal.Db = "Local";
            return GetObjList(query);
        }
        public InvoiceModel SaveInvoice(InvoiceModel model)
        {

            var max = "SELECT MAX(lngNumberID) AS lngNumberID from tblSLInv";

            var a = GetMaxCOl(max);
            int col = Convert.ToInt32(a.lngNumberID);
            col = col + 1;
            var query = "insert into tblSLInv(lngNumberID,lngClientID,dtmDate,strContact,strBillToAddress1,strRegisteredBy,dtmRegisteredDate,lngTerms,memInvMemo)values('" + col+ "','" + model.lngClientID + "','" + model.dtmDate+ "', '"+model.strContact+ "','"+model.strBillToAddress1+ "','" + model.strRegisteredBy + "','" + model.dtmRegisteredDate + "','" + model.lngTerms + "','" + model.memInvMemo + "')  ";
           
            dal.Db = "Local";
            dal.ExecuteNonQuery(query);

            for(int i=0; i<model.Products.Count(); i++)
            {
                var q = "insert into tblSLInvDetail(lngInvoice,strProduct,strDescription,dblQtyShipped,dblPriceEach,dblLineTotal,dtmRegisteredDate,strRegisteredBy,strComment)values('" + col + "','" + model.Products[i].strProduct + "','" + model.Products[i].strDescription + "','" + model.Products[i].dblQtyShipped + "','" + model.Products[i].dblPriceEach+ "','" + model.Products[i].dblLineTotal + "','" + model.dtmRegisteredDate+ "','" + model.strRegisteredBy + "','" + model.memInvMemo + "')";
                dal.Db = "Local";
                dal.ExecuteNonQuery(q);
            }

                return model;
            }
          
        //GET InvoiceDetail from invoiceDetail table by invoiceId
            public List<InvoiceDetail> GetInvoiceDetail(int invoiceId)
        {
            List<InvoiceDetail> paymentList = new List<InvoiceDetail>();
            string query = "select tsid.lngInvoice,tsid.lngDetailID,tsid.dblLineTotal,tsid.dblQtyShipped,tsid.dblPriceEach, tp.strProdName from tblSLInvDetail as tsid inner join tblProducts as tp on tsid.strProduct = tp.strProdNumber where tsid.lngInvoice = '" + invoiceId + "' ";
            dal.Db = "Local";
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InvoiceDetail objectInvDetail = new InvoiceDetail();
                    objectInvDetail.InvoiceID = Convert.ToInt64(reader["lngInvoice"].ToString());
                    objectInvDetail.InvoiceDetailID = Convert.ToInt64(reader["lngDetailID"].ToString());
                    objectInvDetail.LineTotal = Convert.ToDouble(reader["dblLineTotal"].ToString());
                    objectInvDetail.QuantityShipped = Convert.ToInt32(reader["dblQtyShipped"].ToString());
                    objectInvDetail.PriceEach = Convert.ToDouble(reader["dblPriceEach"].ToString());
                    objectInvDetail.Product = reader["strProdName"].ToString();
                    paymentList.Add(objectInvDetail);
                }

            }
            return paymentList;
        }

        public List<InvoiceListModel> GetObjList(string query)
        {
            List<InvoiceListModel> mList = new List<InvoiceListModel>();

            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    model = ReadSingleUserRow((IDataRecord)reader);
                    mList.Add(model);
                }
                reader.Close();
            }
            return mList;

        }



        private InvoiceListModel GetInvoice(string query)
        {
            var customer = new InvoiceListModel();
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                customer = null;
                while (reader.Read())
                {
                    customer = ReadSingleUserRow((IDataRecord)reader);

                }
                reader.Close();
            }
            return customer;
        }

        public InvoiceModel GetInvoiceForTaxData(string query)
        {
            var model = new InvoiceModel();
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                model = null;
                while (reader.Read())
                {
                    model = ReadGetInvoiceForTaxData((IDataRecord)reader);

                }
                reader.Close();
            }
            return model;
        }

        //Read single invoice data
        private static InvoiceModel ReadGetInvoiceForTaxData(IDataRecord record)
        {
            var invData = new InvoiceModel();
            invData.lngNumberID = Convert.ToInt32(record["lngNumberID"].ToString());         
            invData.strBillToAddress1 = record["strBillToAddress1"].ToString();          
            invData.dtmDate = record["dtmDate"] != DBNull.Value ? Convert.ToDateTime(record["dtmDate"]) : DateTime.Now;            
            invData.lngTax = ToNullableInt(record["lngTax"].ToString());
            invData.sngTaxAmt = Convert.ToDouble(record["sngTaxAmt"]);            
            return invData;
        }

        public static int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        //Get  single Invoice data
        public InvoiceModel GetSingleInvoiceData(string query)
        {
            var model = new InvoiceModel();
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                model= null;
                while (reader.Read())
                {
                    model = ReadInvoiceData((IDataRecord)reader);

                }
                reader.Close();
            }
            return model;
        }

        //Read single invoice data
        private static InvoiceModel ReadInvoiceData(IDataRecord record)
        {
            var invData = new InvoiceModel();

            invData.lngNumberID = Convert.ToInt32(record["lngNumberID"].ToString());
            invData.strCompanyName = record["strCompanyName"].ToString();
            invData.strBillToAddress1 = record["strBillToAddress1"].ToString();
            invData.strEmail = record["strEmail"].ToString();
            invData.dtmDate = record["dtmDate"] != DBNull.Value ? Convert.ToDateTime(record["dtmDate"]) : DateTime.Now;
          //  invData.lngTerms = record["lngTerms"] !=DBNull.Value? Convert.ToInt32(record["lngTerms"].ToString()): 0;
            invData.memInvMemo = record["memInvMemo"].ToString();
            //invoiceList.TotalInv = record["TotalInv"] != DBNull.Value ? Convert.ToDecimal(record["TotalInv"]) : 0;
            //invoiceList.TotalPayments = record["TotalPayments"] != DBNull.Value ? Convert.ToDecimal(record["TotalInv"]) : 0;
            //invoiceList.TotalDue = record["TotalDue"] != DBNull.Value ? Convert.ToDecimal(record["TotalInv"]) : 0;
            return invData;
        }

        public List<InvoiceDetailModel> GetInvoieDetailList(string query)
        {
            List<InvoiceDetailModel> mList = new List<InvoiceDetailModel>();

            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    inmodel = ReadInvDetailList((IDataRecord)reader);
                    mList.Add(inmodel);
                }
                reader.Close();
            }
            return mList;

        }


        private static InvoiceDetailModel ReadInvDetailList(IDataRecord record)
        {
            var invData = new InvoiceDetailModel();
            invData.strProduct = record["strProdName"].ToString();
            invData.strDescription = record["strDescription"].ToString();
            invData.dblQtyShipped = Convert.ToDouble(record["dblQtyShipped"].ToString());
            invData.dblPriceEach = Convert.ToDouble(record["dblPriceEach"].ToString());
            invData.dblLineTotal = Convert.ToDouble(record["dblLineTotal"].ToString());

            

            return invData;
        }




        public InvoiceListModel GetMaxCOl(string query)
        {
            var customer = new InvoiceListModel();
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                customer = null;
                while (reader.Read())
                {
                    customer = ReadMax((IDataRecord)reader);

                }
                reader.Close();
            }
            return customer;
        }



        private static InvoiceListModel ReadMax(IDataRecord record)
        {
            var invoiceList = new InvoiceListModel();

            invoiceList.lngNumberID = Convert.ToInt32(record["lngNumberID"].ToString());

            return invoiceList;
        }



            private static InvoiceListModel ReadSingleUserRow(IDataRecord record)
        {
            var invoiceList = new InvoiceListModel();

            invoiceList.lngNumberID = Convert.ToInt32(record["lngNumberID"].ToString());
            invoiceList.strCompanyName = record["strCompanyName"].ToString();

            invoiceList.dtmDate = record["dtmDate"] != DBNull.Value ? Convert.ToDateTime(record["dtmDate"]) : DateTime.Now;
            invoiceList .dtmDueDate= record["dtmDueDate"] != DBNull.Value ? Convert.ToDateTime(record["dtmDueDate"]) : DateTime.Now;

            //           customer.dtmRegisteredDate = record["dtmRegisteredDate"] != DBNull.Value ? Convert.ToDateTime(record["dtmRegisteredDate"]) : DateTime.Now;  //Only when null  



           invoiceList.strPONumber = record["strPONumber"].ToString();

            invoiceList.TotalInv = record["TotalInv"] != DBNull.Value ? Convert.ToDecimal(record["TotalInv"]) : 0 ;
            invoiceList.TotalPayments = record["TotalPayments"] != DBNull.Value ? Convert.ToDecimal(record["TotalInv"]) : 0 ;
            invoiceList.TotalDue = record["TotalDue"] != DBNull.Value ? Convert.ToDecimal(record["TotalInv"]) : 0 ;



            return invoiceList;
        }
        
    }



}