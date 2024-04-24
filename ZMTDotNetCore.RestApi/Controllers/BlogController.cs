using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using ZMTDotNetCore.RestApi.Db;
using ZMTDotNetCore.RestApi.Model;

namespace ZMTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AddDbContent _context;
        public BlogController()
        {
            _context = new AddDbContent();
        }
        [HttpGet]
        public IActionResult Read()
        {
            var lst = _context.Blog.ToList();
            return Ok(lst);
        }

        [HttpGet("{Id}")]
        public IActionResult Edit(int Id)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == Id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _context.Blog.Add(blog);
            int result = _context.SaveChanges();
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            return Ok(message);
        }
        [HttpPut("{Id}")]
        public IActionResult Update(int Id,BlogModel blog)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == Id);
            if (item is null)
            {
                Console.WriteLine("not found ");
                return NotFound();
            }
            item.BlogContent = blog.BlogContent ;
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            int result = _context.SaveChanges();
            string message = result > 0 ? "Updated Successfully! " : "Updated Fail!";

            return Ok(message);
        }
        [HttpPatch("{Id}")]
        public IActionResult Patch(int Id, BlogModel blog)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == Id);
            if (item is null)
            {
                Console.WriteLine("not found ");
                return NotFound();
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent=blog.BlogContent;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            } 
            int result = _context.SaveChanges();
            string message = result > 0 ? "Updated Successfully! " : "Updated Fail!";

            return Ok(message); 
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("not found ");
                return NotFound();
            }
            _context.Blog.Remove(item); 
            int result = _context.SaveChanges();
            string message = result > 0 ? "Delete Successfully! " : "Delete Fail!";

            return Ok(message); 
        }
    }
}
