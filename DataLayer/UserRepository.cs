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
   
    public class UserRepository
    {
        DataAccessLayer dal = new DataAccessLayer();

        public UserModel Login(UserModel model)
        {
            string query = "Select * from tblUsers where strUserID='" + model.strUserID + "' and strUserPwd = '"+model.strUserPwd+"' ";
            return GetUser(query);
        }


        


      


        public UserModel GetUser(string query)
        {
            var user = new UserModel();
            using (SqlConnection connection = dal.Connect())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                user = null;
                while (reader.Read())
                {
                    user = ReadSingleUserRow((IDataRecord)reader);

                }
                reader.Close();
            }
            return user;
        }

        private static UserModel ReadSingleUserRow(IDataRecord record)
        {
            var user = new UserModel();
            user.strUserID = record["strUserID"].ToString();
            user.strUserPwd = record["strUserPwd"].ToString();
            user.strUserEmail = record["strUserEmail"].ToString();
            user.lngUserID = Convert.ToInt32(record["lngUserID"].ToString());
            user.ysnAdmin = Convert.ToBoolean(record["ysnAdmin"].ToString());
            return user;
        }


    }

}