using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMTDotNetCore.ConsoleApp
{
    internal class EFCoreExample
    {
       private readonly AddDbContent db = new AddDbContent();
        public void Run() {
            Read();
            Edit(2);
            Edit(10);
            Create(0, "title_2", "author_2", "content_2");
            Update(1002, "title_4", "author_4", "content_4");
            Delete(1002);
        }
        public void Read() 
        {
          
            var  lst=db.Blog.ToList();
            foreach(var item in lst)
            {
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("----------------");

            }
        }
        public void Edit(int Id)
        {
           var item= db.Blog.FirstOrDefault(x=>x.BlogId==Id);
           if(item is null)
            {
                Console.WriteLine("not found ");
                return;
            }
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("----------------");

        }
        public void Create(int id, string title, string author, string content)
        {

            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content


            };
            db.Blog.Add(item);
           int result= db.SaveChanges();
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            Console.WriteLine(message);
        }
        public void Update(int id, string title, string author, string content)
        {
            var item = db.Blog.FirstOrDefault(x=>x.BlogId==id);
            if (item is null)
            {
                Console.WriteLine("not found ");
                return;
            }
           item.BlogContent=content;    
            item.BlogTitle=title;
            item.BlogAuthor=author;
           int result= db.SaveChanges();
            string message = result > 0 ? "Update Successfully! " : "Update Fail!";
            Console.WriteLine(message);
        }
        public void Delete(int id) 
        {
            var item = db.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("not found ");
                return;
            }
            db.Blog.Remove(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Delete Successfully! " : "Delete Fail!";
            Console.WriteLine(message);
        }
    }
}
