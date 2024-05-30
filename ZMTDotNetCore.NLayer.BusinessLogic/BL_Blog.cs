 
using ZMTDotNetCore.NLayer.DataAccess; 
namespace ZMTDotNetCore.RestApiWithNLayer.Feature.Blog
{
    public class BL_Blog
    {
        private readonly DA_Blog _blog;

        public BL_Blog()
        {
            _blog = new DA_Blog();
        }
        public List<BlogModel> GetBlogs()
        {
            var lst = _blog.GetBlogs();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            return _blog.GetBlog(id);
        }
        public int CreateBlog(BlogModel blog)
        {
           int result=_blog.CreateBlog(blog);
            return result;
        }
        public int UpdateBlog(int id, BlogModel blog)
        {
            var item = _blog.GetBlog(id);
            if (item is null) return 0;

            
            var result = _blog.UpdateBlog(id, blog);
            return result;
        }
        public int DeleteBlog(int id)
        {
            var item = _blog.GetBlog(id);
            if (item is null) return 0;
            
            var result = _blog.DeleteBlog(id);
            return result;
        }
    }
}
