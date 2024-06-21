using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZMTDotNetCore.MvcApp.Db;
using ZMTDotNetCore.MvcApp.Models;

namespace ZMTDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AddDbContent db;

        public BlogController(AddDbContent db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var list = db.Blog.AsNoTracking().OrderByDescending(x=>x.BlogId).ToList();
            return View(list);
        }
        [ActionName("Create")]
        public IActionResult Create()
        {
             
            return View();
        }
        [ActionName("Create")]
        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            db.Blog.AddAsync(blog);
            db.SaveChanges();
            return Redirect("/Blog/Index");
        }
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var data= await db.Blog.AsNoTracking().FirstOrDefaultAsync(x=>x.BlogId==id);
            if(data is null)
            {
                return Redirect("/Blog  ");
            }
            return View(data);
        }
        [ActionName("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id,BlogModel blog)
        {
            var item = await db.Blog
              .AsNoTracking()
              .FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Redirect("/Blog");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            db.Entry(item).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return Redirect("/Blog"); 
        }
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await db.Blog.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if (data is null)
            {
                return Redirect("/Blog");
            }
            db.Remove(data);
            db.SaveChanges();
            return Redirect("/Blog");
        }
    }
}
