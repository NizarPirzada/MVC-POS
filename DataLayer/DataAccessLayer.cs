using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCPOS.DataLayer
{
    public class DataAccessLayer
    {
        static string ConnectionString = "";
        public string Db = "Local";
        SqlConnection _connect;

        public SqlConnection Connect()
        {
            if (Db == "Local")
            {
                ConnectionString = System.Configuration.ConfigurationManager.
                ConnectionStrings["LocalDb"].ConnectionString;
            }
           
            _connect = new SqlConnection(ConnectionString);
            Db = "Local";
            return _connect;

        }

        public void ConnectionClose()
        {
            if (ConnectionState.Open == _connect.State)
            {
                _connect.Close();
            }
        }

        public SqlDataReader GetDataReader(string query)
        {
            Connect();
            if (ConnectionState.Open == _connect.State)
            {
            }
            else
            {
                _connect.Open();
            }

            SqlCommand comm = new SqlCommand(query, _connect);
            return comm.ExecuteReader();
        }

        public SqlDataReader GetDataReader(string query, List<SqlParameter> myParamArray)
        {
            Connect();
            if (ConnectionState.Open == _connect.State)
            {
            }
            else
            {
                _connect.Open();
            }

            SqlCommand comm = new SqlCommand(query, _connect);
            for (int i = 0; i < myParamArray.Count; i++)
            {
                comm.Parameters.Add(new SqlParameter { ParameterName = myParamArray[i].ParameterName, Value = myParamArray[i].Value });
            }

            return comm.ExecuteReader();
        }

        public SqlDataReader GetDataReader(SqlCommand comm)
        {
            Connect();
            if (ConnectionState.Open == _connect.State)
            {
            }
            else
            {
                _connect.Open();
            }
            comm.Connection = _connect;
            return comm.ExecuteReader();
        }


        public Int32 ExecuteNonQuery(string query)
        {
            Int32 Id = 0;
            try
            {
                query = query.Replace(" or ", "");
                query = query.Replace("'or ", "'");
                query = query.Replace("--", "");
                query = query.Replace("/*", "");
                query = query.Replace("*/", "");
                query = query.Replace("#", "");


                Connect();
                if (ConnectionState.Open == _connect.State)
                {

                }
                else
                {
                    _connect.Open();
                }

                SqlCommand comm = new SqlCommand(query, _connect);

                object tem = comm.ExecuteScalar();

                Id = Convert.ToInt32(tem);

            }
            catch (Exception er)
            {
                Id = -1;
            }

            finally
            {
                ConnectionClose();
            }

            return Id;
        }


        public Int32 ExecuteScalar(string query)
        {
            Int32 Id = 0;
            try
            {
                Connect();
                if (ConnectionState.Open == _connect.State)
                {

                }
                else
                {
                    _connect.Open();
                }

                SqlCommand comm = new SqlCommand(query, _connect);

                Id = int.Parse(comm.ExecuteScalar().ToString());

            }
            catch (Exception er)
            {
                Id = -1;
            }

            finally
            {
                ConnectionClose();
            }

            return Id;
        }


        public Int32 ExecuteNonQuery(string query, List<SqlParameter> myParamArray)
        {
            Int32 Id = 0;
            try
            {
                Connect();
                if (ConnectionState.Open == _connect.State)
                {

                }
                else
                {
                    _connect.Open();
                }

                SqlCommand comm = new SqlCommand(query, _connect);
                for (int i = 0; i < myParamArray.Count; i++)
                {
                    comm.Parameters.Add(new SqlParameter { ParameterName = myParamArray[i].ParameterName, Value = myParamArray[i].Value });
                }

                object tem = comm.ExecuteScalar();
                Id = Convert.ToInt32(tem);

            }
            catch (Exception er)
            {
                Id = -1;
            }

            finally
            {
                ConnectionClose();
            }

            return Id;
        }
        public Int32 ExecuteNonQuery(SqlCommand comm)
        {
            Int32 Id = 0;
            try
            {
                Connect();
                if (ConnectionState.Open == _connect.State)
                {

                }
                else
                {
                    _connect.Open();
                }
                comm.Connection = _connect;
                object tem = comm.ExecuteScalar();
                Id = Convert.ToInt32(tem);

            }
            catch (Exception er)
            {
                Id = -1;
            }

            finally
            {
                ConnectionClose();
            }

            return Id;
        }

    }
}