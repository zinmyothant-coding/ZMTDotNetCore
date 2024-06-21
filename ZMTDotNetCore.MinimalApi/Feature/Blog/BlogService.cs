using Microsoft.EntityFrameworkCore;
using ZMTDotNetCore.MinimalApi.Db;
using ZMTDotNetCore.MinimalApi.Model;

namespace ZMTDotNetCore.MinimalApi.Feature.Blog
{
    public static class BlogService
    {
        public static IEndpointRouteBuilder AddBlogsFeature(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/Blog", async (AddDbContent db) =>
            {
                var lst = await db.Blog.AsNoTracking().ToListAsync();
                return Results.Ok(lst);
            });

            app.MapPost("api/Blog", async (AddDbContent db, BlogModel blog) =>
            {
                await db.Blog.AddAsync(blog);
                var result = await db.SaveChangesAsync();

                string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                return Results.Ok(message);
            });

            app.MapPut("api/Blog/{id}", async (AddDbContent db, int id, BlogModel blog) =>
            {
                var item = await db.Blog.FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No data found.");
                }

                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;
                var result = await db.SaveChangesAsync();

                string message = result > 0 ? "Updating Successful." : "Updating Failed.";
                return Results.Ok(message);
            });

            app.MapDelete("api/Blog/{id}", async (AddDbContent db, int id) =>
            {
                var item = await db.Blog.FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No data found.");
                }

                db.Blog.Remove(item);
                var result = await db.SaveChangesAsync();

                string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
                return Results.Ok(message);
            });

            return app;

        }
    }
}
