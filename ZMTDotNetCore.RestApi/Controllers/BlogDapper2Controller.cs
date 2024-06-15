using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using ZMTDotNetCore.RestApi.Model;
using ZMTDotNetCore.Shared;

namespace ZMTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DrapperService _drapperService;

        public BlogDapper2Controller(DrapperService drapperService)
        {
            _drapperService = drapperService;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";
            var lst=_drapperService.Query<BlogModel>(query).ToList();
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
           int result= _drapperService.Excute<BlogModel>(query, blog);
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

             int result = _drapperService.Excute<BlogModel>(query, blog);
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
             int result = _drapperService.Excute<BlogModel>(query, blog);
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
            int result = _drapperService.Excute<BlogModel>(query, new BlogModel { BlogId=id});
            var message = result > 0 ? "Delete Successfully!" : "Delete Fail!";
            return Ok(message);
        }
        private BlogModel? FindById(int id)
        {
            string query = "select * from Tbl_Blog where BlogId=@BlogId;";
            var item = _drapperService.QueryFirstOrDefault<BlogModel>(query,new BlogModel { BlogId=id});
            return item;
        }
    }
}
