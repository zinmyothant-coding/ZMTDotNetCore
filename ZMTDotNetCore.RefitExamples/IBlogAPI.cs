using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMTDotNetCore.RefitExamples;

namespace ZMTDotNetCore.RefitExamples
{
    public interface IBlogAPI
    {
        [Get("/api/blog")]
        Task<List<BlogModel>> GetBlogs();
        [Get("/api/blog/{Id}")]
        Task<BlogModel> GetBlog(int id);
        [Put("/api/blog/{Id}")]
        Task<string> UpdateAsync(int id,BlogModel blog);
        [Post("/api/blog")]
        Task<string> CreateAsync(BlogModel blog);
        [Delete("/api/blog/{id}")]
        Task<string> DeleteAsync(int id);
    }
    public class BlogModel
    {
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
    }
}
