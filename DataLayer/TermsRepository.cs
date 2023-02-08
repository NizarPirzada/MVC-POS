using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MVCPOS.Models;
namespace MVCPOS.DataLayer
{
    public class TermsRepository
    {
        DataAccessLayer dal = new DataAccessLayer();
        TermsModel model = new TermsModel();

        //Get Terms
        public List<TermsModel> GetTermsList()
        {
            string query = "select * from tblTerms";
            dal.Db = "Local";
            return GetObjList(query);
            
        }


        public List<TermsModel> GetObjList(string query)
        {
            List<TermsModel> mList = new List<TermsModel>();

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



        public TermsModel GetTerms(string query)
        {
            var terms = new TermsModel();
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                terms = null;
                while (reader.Read())
                {
                    terms = ReadSingleUserRow((IDataRecord)reader);

                }
                reader.Close();
            }
            return terms;
        }

        private static TermsModel ReadSingleUserRow(IDataRecord record)
        {
            var terms = new TermsModel();

            terms.lngTermsID = Convert.ToInt32(record["lngTermsID"].ToString());
            terms.strTerms = record["strTerms"].ToString();
            terms.lngSort = Convert.ToInt32(record["lngSort"].ToString());
            terms.ysnInactive = Convert.ToBoolean(record["ysnInactive"].ToString());
            terms.lngNumofDays = Convert.ToInt32(record["lngNumofDays"] == DBNull.Value? "0" : record["lngNumofDays"].ToString());
            return terms;
        }


    }
}