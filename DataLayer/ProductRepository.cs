using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCPOS.Models;
using MVCPOS.DataLayer;
using System.Data.SqlClient;
using System.Data;

namespace MVCPOS.DataLayer
{
   
    public class ProductRepository
    {
        DataAccessLayer dal = new DataAccessLayer();
        ProductModel model = new ProductModel();

        public List<ProductModel> GetProducts()
        {
            string query = "select * from tblProducts where lngLocation=1";
            return GetObjList(query);
        }

        public List<ProductModel> GetProductByName(string search)
        {
            string query = "select * from tblProducts where strProdName like '%" + search + "%'";
            dal.Db = "Local";
            return GetObjList(query);
        }




        public List<ProductModel> GetObjList(string query)
        {
            List<ProductModel> mList = new List<ProductModel>();

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

        private ProductModel GetProducts(string query)
        {
            var dataObject = new ProductModel();
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dataObject = null;
                while (reader.Read())
                {
                    dataObject = ReadSingleUserRow((IDataRecord)reader);
                }
                reader.Close();
            }
            return dataObject;
        }

        private static ProductModel ReadSingleUserRow(IDataRecord record)
        {
            var user = new ProductModel();
            user.StrProdNumber = record["strProdNumber"].ToString();
            user.StrProdName = record["strProdName"].ToString();
            user.strSaleDescription = record["strSaleDescription"].ToString();
            user.dblSalesPrice = Convert.ToDouble(record["dblSalesPrice"].ToString());
            return user;
        }
    }
}