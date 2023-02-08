using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MVCPOS.Entities;
using MVCPOS.Models;
namespace MVCPOS.DataLayer
{
    public class CustomerRepository
    {
        DataAccessLayer dal = new DataAccessLayer();
        CustomerModel model = new CustomerModel();


        //GET Invoice by invoiceId
        public PaymentModel GetInvoice(int invoiceId)
        {
            PaymentModel paymentObject = new PaymentModel();
            string query = "select* from tblSLInv where lngNumberID='" + invoiceId + "' ";
            dal.Db = "Local";
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    paymentObject.InvoiceID = Convert.ToInt64(reader["lngNumberID"].ToString());
                    paymentObject.ClientID = Convert.ToInt64(reader["lngClientID"].ToString());
                    paymentObject.TaxAmount = Convert.ToDouble(reader["sngTaxAmt"].ToString());
                }

            }
            return paymentObject;
        }

        //GET InvoiceDetail from invoiceDetail table by invoiceId
        public List<InvoiceDetail> GetInvoiceDetail(int invoiceId)
        {
            List<InvoiceDetail> paymentList = new List<InvoiceDetail>();
            string query = "select * from tblSLInvDetail where lngInvoice='" + invoiceId + "' ";
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
                    paymentList.Add(objectInvDetail);
                }

            }
            return paymentList;
        }

        //this code is for Invoice Payment Screen
        public List<CustomerModel> GetAllCustomer()
        {
            List<CustomerModel> listCustomerModel = new List<CustomerModel>();
            string query = "select * from tblCust";
            dal.Db = "Local";
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CustomerModel objectCustomerModel = new CustomerModel();
                    objectCustomerModel.lngCustID = Convert.ToInt32(reader["lngCustID"].ToString());
                    objectCustomerModel.strCompanyName = reader["strCompanyName"].ToString();
                    listCustomerModel.Add(objectCustomerModel);
                }

            }
            return listCustomerModel;
        }

        //this code is for Invoice Payment Screen
        public List<Location> GetAllLocation()
        {
            List<Location> listLocation = new List<Location>();
            string query = "select * from tblLocations";
            dal.Db = "Local";
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Location objectLocation = new Location();
                    objectLocation.LocationID = Convert.ToInt64(reader["lngLocationID"].ToString());
                    objectLocation.LocationName = reader["strLocation"].ToString();
                    listLocation.Add(objectLocation);
                }

            }
            return listLocation;
        }

        //invoice related 
        public void SavePayment(SavePayment data)
        {
            string query = "insert into tblSLReceipts(lngInvoice,dtmPayDate,strPayMethod,curPayAmt,curNetAmount,dtmRegisteredDate,strRegisteredBy) values('" + data.InvoiceID + "' , '" + data.PaymentDate + "' , '" + data.PaymentMethod + "','" + data.PaymentAmount + "','" + data.NetAmount + "','" + data.RegisteredDate + "', '" + data.RegisteredBy + "')";
            dal.Db = "Local";
            dal.ExecuteNonQuery(query);
        }

        //Register Customer
        public CustomerModel Register(CustomerModel model)
        {
            string query = "insert into tblCust(strCompanyName,strContact,strEmail,strBillToAddress1,strBillToState,strRegisteredBy,dtmRegisteredDate,strBillToCity,strBillToZip) values('" + model.strCompanyName + "' , '" + model.strContact + "' , '" + model.strEmail + "','" + model.strBillToAddress1 + "','" + model.strBillToState + "','" + model.strRegisteredBy + "', '" + model.dtmRegisteredDate + "' ,'" + model.strBillToCity + "','" + model.strBillToZip + "')";
            dal.Db = "Local";
            dal.ExecuteNonQuery(query);
            return model;
        }

        //Get All Customer List
        public List<CustomerModel> GetCustomerList()
        {
            string query = "select * from tblCust";
            dal.Db = "Local";
            return GetObjList(query);
        }


        //search customer by name
        public List<CustomerModel> GetCustomerByName(string search)
        {
            string query = "select * from tblCust where strCompanyName like '%" + search + "%' ";
            dal.Db = "Local";
            return GetObjList(query);
        }



        //Get CustomeByID
        public CustomerModel GetCustomerByID(CustomerModel model)
        {
            string query = "select* from tblCust where lngCustId='" + model.lngCustID + "' ";
            dal.Db = "Local";
            return GetCustomer(query);
        }

        //Update Customer by ID
        public CustomerModel UpdateCustomerByID(CustomerModel model)
        {
            string query = "update tblCust set strCompanyName='" + model.strCompanyName + "' , strContact='" + model.strContact + "' , strEmail = '" + model.strEmail + "' ,strBillToAddress1='" + model.strBillToAddress1 + "' , strBillToState='" + model.strBillToState + "' ,  strBillToCity='" + model.strBillToCity + "' , strBillToZip='" + model.strBillToZip + "' where lngCustID='" + model.lngCustID + "' ";
            dal.Db = "Local";
            dal.ExecuteNonQuery(query);
            return model;

        }

        public CustomerModel DeleteCustomer(CustomerModel model)
        {
            string query = "delete from tblCust where lngCustId='" + model.lngCustID + "' ";
            dal.Db = "Local";
            dal.ExecuteNonQuery(query);
            return model;
        }


        public List<CustomerModel> GetObjList(string query)
        {
            List<CustomerModel> mList = new List<CustomerModel>();

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



        public CustomerModel GetCustomer(string query)
        {
            var customer = new CustomerModel();
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

        private static CustomerModel ReadSingleUserRow(IDataRecord record)
        {
            var customer = new CustomerModel();

            customer.lngCustID = Convert.ToInt32(record["lngCustID"].ToString());
            customer.strCompanyName = record["strCompanyName"].ToString();
            customer.strContact = record["strContact"].ToString();
            customer.strEmail = record["strEmail"].ToString();
            customer.strBillToAddress1 = record["strBillToAddress1"].ToString();
            customer.strBillToState = record["strBillToState"].ToString();
            customer.strBillToCity = record["strBillToCity"].ToString();
            customer.strRegisteredBy = record["strRegisteredBy"].ToString();
            customer.strBillToCity = record["strBillToCity"].ToString();
            customer.strBillToZip = record["strBillToZip"].ToString();
            customer.dtmRegisteredDate = record["dtmRegisteredDate"] != DBNull.Value ? Convert.ToDateTime(record["dtmRegisteredDate"]) : DateTime.Now;  //Only when null            
            return customer;
        }
    }
}