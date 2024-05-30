// See https://aka.ms/new-console-template for more information
using ZMTDotNetCore.NLayer.DataAccess;
using ZMTDotNetCore.RestApiWithNLayer.Feature.Blog;

Console.WriteLine("Hello, World!");

BL_Blog blog = new BL_Blog();
BlogModel model = new BlogModel
{
    BlogAuthor="test0",
    BlogContent="test",
    BlogTitle="test"
};
blog.CreateBlog(model);
Console.ReadKey();
