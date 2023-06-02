using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                    var result = cmd.ExecuteScalar().ToString();
                    cmd.Connection.Close();
                    var json = JsonConvert.DeserializeObject<T>(result);
                    return json;
                }
            }
        }

        public T Exec<T>(string storeName,Dictionary<string,dynamic> parameters)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GetStoreProduce(storeName, parameters);
                    foreach (var item in parameters)
                    {
                        var test = new SqlParameter(item.Key, item.Value);
                        cmd.Parameters.Add(test);
                    }
                    cmd.Connection.Open();
                    var result = cmd.ExecuteScalar().ToString();
                    cmd.Connection.Close();
                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
        }

        public string Exec(string storeName)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = storeName;
                    cmd.Connection.Open();
                    var result = cmd.ExecuteScalar().ToString();
                    cmd.Connection.Close();
                    return result;
                }
            }
        }

        public string Exec(string storeName, Dictionary<string, dynamic> parameters)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = GetStoreProduce(storeName, parameters);
                    foreach (var item in parameters)
                    {
                        var test = new SqlParameter(item.Key, item.Value);
                        cmd.Parameters.Add(test);
                    }
                    cmd.Connection.Open();
                    var result = cmd.ExecuteScalar().ToString();
                    cmd.Connection.Close();
                    return result;
                }
            }
        }

        private string GetStoreProduce(string storeName, Dictionary<string, dynamic> parameters)
        {
            var param = string.Join(",", parameters.Keys).ToLower();
            return string.Format(_storeFormat, storeName, param);
        }
    }
}