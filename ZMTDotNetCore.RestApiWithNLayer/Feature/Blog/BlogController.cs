

namespace ZMTDotNetCore.RestApiWithNLayer.Feature.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog blog;
        public BlogController(BL_Blog _blog) 
        {
            blog = _blog;
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = blog.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var item = blog.GetBlog(Id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel requestModel)
        { 
            int result = blog.CreateBlog(requestModel);
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            return Ok(message);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int Id, BlogModel requestModel)
        {
            var item = blog.GetBlog(Id);
                
            if (item is null)
            {
                Console.WriteLine("not found ");
                return NotFound();
            }
            int result = blog.UpdateBlog(Id, requestModel);
            string message = result > 0 ? "Updated Successfully! " : "Updated Fail!";

            return Ok(message);
        }

        [HttpPatch("{Id}")]
        public IActionResult Patch(int Id, BlogModel requestModel)
        {
            var item =blog.GetBlog(Id);
            if (item is null)
            {
                Console.WriteLine("not found ");
                return NotFound();
            }
            if (!string.IsNullOrEmpty(requestModel.BlogContent))
            {
                item.BlogContent = requestModel.BlogContent;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                item.BlogAuthor = requestModel.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                item.BlogTitle = requestModel.BlogTitle;
            }
            int result = blog.UpdateBlog(item.BlogId, item);
            string message = result > 0 ? "Updated Successfully! " : "Updated Fail!";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = blog.GetBlog(id);
            if (item is null)
            {
                Console.WriteLine("not found ");
                return NotFound();
            } 
            int result = blog.DeleteBlog(id);
          string message = result > 0 ? "Delete Successfully! " : "Delete Fail!";

            return Ok(message);
        }
    }
}
