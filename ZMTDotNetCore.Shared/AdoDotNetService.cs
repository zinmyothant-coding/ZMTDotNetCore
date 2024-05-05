using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZMTDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionStirng;

        public AdoDotNetService(string ConnectionString)
        {
            _connectionStirng = ConnectionString;
        }
        public List<A> Query<A>(string query,params AdoDotNetParameter[]? param)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionStirng);

            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            if(param is not null && param.Length>0)
            {
                var paraArrays=param.Select(item =>new SqlParameter (item.Name,item.Value)).ToArray();
                cmd.Parameters.AddRange(paraArrays);
            }
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();
            string json=JsonConvert.SerializeObject(dt);
            List<A> list = JsonConvert.DeserializeObject<List<A>>(json);
            return list;
        }
        public A QueryFirstOrDefault<A>(string query,params AdoDotNetParameter[]? param)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionStirng);

            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            if (param is not null && param.Length > 0)
            {
                var paraArrays = param.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(paraArrays);
            }
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();
            string json = JsonConvert.SerializeObject(dt);
           List< A> list = JsonConvert.DeserializeObject<List<A>>(json);
            return list[0];
        }
        public int Exectue(string query, params AdoDotNetParameter[]? param)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionStirng);

            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection); 
            if (param is not null && param.Length > 0)
            {
                var paraArrays = param.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(paraArrays);
            } 
            int result=cmd.ExecuteNonQuery();
            sqlConnection.Close(); 
            return result;
        }
    }

    public class AdoDotNetParameter
    {
        public AdoDotNetParameter() { }
        public AdoDotNetParameter(string Name,object Value) {
          this.  Name = Name;
            this.Value = Value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
