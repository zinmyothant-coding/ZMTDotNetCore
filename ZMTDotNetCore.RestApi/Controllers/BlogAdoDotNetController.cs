using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using ZMTDotNetCore.RestApi.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZMTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog;";
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);

            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();
            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"])

            }).ToList();

            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Tbl_Blog where BlogId=@id;";
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);

            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            sqlConnection.Close();
            if (dt.Rows.Count == 0)
            {
                return NotFound();
            }
            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"])
            };
            return Ok(item);
        }
        [HttpPost]
        public IActionResult PostBlog(BlogModel model)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
              ( 
               [BlogTitle]
              ,[BlogAuthor]
              ,[BlogContent])
        VALUES
              (  
               @BlogTitle
              , @BlogAuthor
              ,@BlogContent );";
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle); 
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            int result = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel model)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound();
            }
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            string query = @"update Tbl_Blog set BlogTitle=@BlogTitle,
               BlogAuthor=@BlogAuthor,
               BlogContent=@BlogContent where BlogId=@id;";
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            cmd.Parameters.AddWithValue("@id", id);
            int result = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult Edit(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound();
            }
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition = "BlogTitle=@BlogTitle, "; 
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition = "BlogAuthor=@BlogAuthor, "; 
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition = "BlogContent=@BlogContent, "; 
            }
            if (condition.Length == 0)
            {
                return NotFound();
            }
            condition = condition.TrimEnd(',', ' ');

            string query = $@"Update Tbl_Blog set  {condition} where BlogId=@BlogId;";
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogTitle", GetNull(blog.BlogTitle));
            cmd.Parameters.AddWithValue("@BlogAuthor", GetNull(blog.BlogAuthor));
            cmd.Parameters.AddWithValue("@BlogContent", GetNull( blog.BlogContent));
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            string message = result > 0 ? "Updated Successfully! " : "Updated Fail!";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound();
            }
            SqlConnection connection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            string query = "delete Tbl_BlogModel where BlogId=@id;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
           int result= cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            return Ok(message);
        }
        private BlogModel? FindById(int id)
        {
            string query = "select * from Tbl_Blog where BlogId=@id;";
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);

            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            sqlConnection.Close();
            DataRow dr = dt.Rows[0];
            sqlConnection.Close();
            var item = new BlogModel
            {
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"])
            };
            return item;

        }
        protected virtual object GetNull(object obj)
        {
            if (obj == null)
            {
                obj = DBNull.Value;
                return obj;

            }
            // Check that object is "string" and object's value is empty string
            if ((obj is string) && (obj.ToString() == string.Empty))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            } 
            return obj;
        }
    }
}
