using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVCPOS.Models;
using MVCPOS.DataLayer;
namespace MVCPOS.ApiControllers
{
    public class CustomerController : ApiController
    {
        CustomerRepository repo = new CustomerRepository();

        //Register Customer
        [HttpPost]
        public CustomerModel Register([FromBody]CustomerModel model)
        {
            model.dtmRegisteredDate = DateTime.Now;
            var m = repo.Register(model);
            return model;
        }

        //get ALl Customer List
        public IEnumerable<CustomerModel> GetCustomerList()
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            customers = repo.GetCustomerList();
            return customers;
        }

        //Get Customer By ID
        [HttpPost]
        public CustomerModel GetCustomerByID([FromBody]CustomerModel model)
        {
            var getCustomer = repo.GetCustomerByID(model);
            return getCustomer;
        }

        //Get Customer by Name
        [HttpGet]
        public IEnumerable<CustomerModel>GetCustomerByName(string search)
        {
            List<CustomerModel> customersList = new List<CustomerModel>();
            customersList = repo.GetCustomerByName(search);
            return customersList;
        }



        //Update Customer by ID
        [HttpPost]
        public CustomerModel UpdateCustomerByID([FromBody] CustomerModel model)
        {
            var updateCustomer = repo.UpdateCustomerByID(model);
            return updateCustomer;
        }


        //Delete Customer by ID
        [HttpPost]
        public CustomerModel DeleteCustomer ([FromBody] CustomerModel model)
        {
            var deleteCustomer = repo.DeleteCustomer(model);
            return deleteCustomer;
        }


    }
}
