using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ZMTDotNetCore.RestApi.Model;

namespace ZMTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";
            using IDbConnection connection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            List<BlogModel> lst = connection.Query<BlogModel>(query).ToList();
            
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound();

            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlogs(BlogModel blog)
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
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            int result = dbConnection.Execute(query, blog);
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id,BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound();

            }
            blog.BlogId = id;
            string query = @"Update Tbl_Blog set [BlogTitle]=@BlogTitle,[BlogAuthor]=@BlogAuthor,[BlogContent]=@BlogContent where BlogId=@BlogId;";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            int result = dbConnection.Execute(query, blog);
            string message = result > 0 ? "Updated Successfully! " : "Updated Fail!";
            return Ok(message);
        }
       [HttpPatch("{id}")]
        public IActionResult EditBlogs(int id,BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound();

            }
            blog.BlogId = id;
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition = "BlogTitle=@BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition = "BlogAuthor=@BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition = "BlogAuthor=@BlogAuthor, ";
            }
            if (condition.Length == 0)
            {
                return NotFound();
            }
            condition = condition.Substring(0, condition.Length - 2);
            string query = $@"Update Tbl_Blog set  {condition} where BlogId=@BlogId;";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            int result = dbConnection.Execute(query, blog);
            string message = result > 0 ? "Updated Successfully! " : "Updated Fail!";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound();

            }
            string query = "DELETE Tbl_Blog where BlogId=@BlogId";
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            int result = dbConnection.Execute(query, item);
            var message = result > 0 ? "Delete Successfully!" : "Delete Fail!";
            return Ok(message);
        }
        private BlogModel? FindById(int id)
        {
            using IDbConnection connection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            BlogModel item = connection.Query<BlogModel>("select * from Tbl_Blog where BlogId=@BlogId", new BlogModel { BlogId = id }).FirstOrDefault();
            return item;
        }
    }
}
