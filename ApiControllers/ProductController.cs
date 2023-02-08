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
    public class ProductController : ApiController
    {
        ProductRepository repo = new ProductRepository();

        public IEnumerable<ProductModel>GetProductByName(string search)
        {
            List<ProductModel> productsList = new List<ProductModel>();
            productsList = repo.GetProductByName(search);
            return productsList;
        }


    }
}
