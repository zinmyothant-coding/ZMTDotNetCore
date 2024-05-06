using ZMTDotNetCore.RestApiWithNLayer.Db;
using ZMTDotNetCore.RestApiWithNLayer.Model;

namespace ZMTDotNetCore.RestApiWithNLayer.Feature.Blog
{
    public class DA_Blog
    {
        private readonly AddDbContent _context;
        public DA_Blog()
        {
            _context = new AddDbContent();
        }
        public List<BlogModel> GetBlogs()
        {
            var lst=_context.Blog.ToList();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            return _context.Blog.FirstOrDefault(x => x.BlogId == id);
        }
        public int CreateBlog(BlogModel blog)
        {
            _context.Blog.Add(blog);
            var result = _context.SaveChanges();
            return result;
        }
        public int UpdateBlog(int id,BlogModel blog)
        {
            var item= _context.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item is null) return 0;

            item.BlogAuthor = blog.BlogAuthor;
            item.BlogTitle = blog.BlogTitle;
            item.BlogContent = blog.BlogContent;
            var result = _context.SaveChanges();
            return result;
        }
        public int DeleteBlog(int id) 
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item is null) return 0;
            _context.Blog.Remove(item);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
