using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using ZMTDotNetCore.RestApi.Model;
using ZMTDotNetCore.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZMTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService adoDotNetService = new AdoDotNetService(ConnectionStrings.connectionStrings.ConnectionString);
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog;";
           var lst= adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Tbl_Blog where BlogId=@blogId;";
            var item = adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@blogId", id));
            return Ok(item);
        }
        [HttpPost]
        public IActionResult PostBlog(BlogModel model)
        {
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
           int result= adoDotNetService.Exectue(query, new AdoDotNetParameter("@BlogTitle", model.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", model.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", model.BlogContent));
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
            string query = @"update Tbl_Blog set BlogTitle=@BlogTitle,
               BlogAuthor=@BlogAuthor,
               BlogContent=@BlogContent where BlogId=@id;";
            
            int result = adoDotNetService.Exectue(query, new AdoDotNetParameter("@BlogTitle", model.BlogTitle),
                 new AdoDotNetParameter("@BlogAuthor", model.BlogAuthor),
                 new AdoDotNetParameter("@BlogContent", model.BlogContent),
                 new AdoDotNetParameter("@id", id)); 
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
            
            int result = adoDotNetService.Exectue(query, new AdoDotNetParameter("@BlogTitle", GetNull(blog.BlogTitle)),
                 new AdoDotNetParameter("@BlogAuthor", GetNull(blog.BlogAuthor)),
                 new AdoDotNetParameter("@BlogContent", GetNull(blog.BlogContent)),
                 new AdoDotNetParameter("@BlogId", id));
           
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
            string query = "delete Tbl_Blog where BlogId=@id;";
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            int result = adoDotNetService.Exectue(query, new AdoDotNetParameter("@id", id));
              connection.Close();
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            return Ok(message);
        }
        private BlogModel? FindById(int id)
        {
            string query = "select * from Tbl_Blog where BlogId=@id;";
            BlogModel result = adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@id", id));

            return result;

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
