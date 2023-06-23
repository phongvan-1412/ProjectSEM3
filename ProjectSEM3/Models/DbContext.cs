using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Helpers;
using static System.Collections.Specialized.BitVector32;

namespace ProjectSEM3.Models
{
    public class DbContext
    {
        private static DbContext instance;
        public string connString = ConfigurationManager.AppSettings.Get("ConnectionString");
        private string _storeFormat = "{0} {1}";
        public static DbContext Instance
        {
            get
            {
                instance = instance ?? new DbContext();
                return instance;
            }
        }

        public T Exec<T>(string storeName)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = storeName;
                    cmd.Connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    da.Fill(dataset);
                    cmd.Connection.Close();
                    var res = JsonConvert.SerializeObject(dataset.Tables[0]);
                    return JsonConvert.DeserializeObject<T>(res);
                }
            }
        }

        public T Exec<T>(string storeName,Dictionary<string,dynamic> parameters)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = storeName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                    }
                    da.SelectCommand = cmd;
                    try
                    {
                        DataSet dataset = new DataSet();
                        da.Fill(dataset);
                        cmd.Connection.Close();
                        var res = JsonConvert.SerializeObject(dataset.Tables[0]);
                        return JsonConvert.DeserializeObject<T>(res);
                    }
                    catch (Exception ex)
                    {
                        cmd.Connection.Close();
                        return JsonConvert.DeserializeObject<T>("null");
                        throw ex;
                    }
                    
                    
                }
            }
        }

        public DataTable Exec(string storeName)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
               
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = storeName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    da.Fill(dataset);
                    cmd.Connection.Close();
                    return dataset.Tables[0];
                }
            }
        }

        public DataTable Exec(string storeName, Dictionary<string, dynamic> parameters)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
               
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = storeName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                    }
                    da.SelectCommand = cmd;
                    DataSet dataset = new DataSet();
                    da.Fill(dataset);
                    cmd.Connection.Close();
                    return dataset.Tables[0];
                }
            }
        }

        public class Result<T>
        {
            public T Data;
            public string Mes;
            public bool IsSuccess;
        }

        public class Result
        {
            public string Mes;
            public bool IsSuccess;
        }
    }

}