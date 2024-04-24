using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ZMTDotNetCore.Shared
{
    public class DrapperService
    {
        private  readonly string _connectionString;
        public DrapperService(String ConnectionString) 
        {
            _connectionString= ConnectionString;
        }
        public List<T> Query<T>(string query,object?  param=null) 
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var lst=db.Query<T>(query, param).ToList();
            return lst;
        }
        public T QueryFirstOrDefault<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var lst = db.Query<T>(query, param).FirstOrDefault();
            return lst;
        }
        public int Excute<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var result = db.Execute(query, param);
            return result;
        }
    }
}
