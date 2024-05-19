using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMTDotNetCore.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogAPI _blogAPI = RestService.For<IBlogAPI>("https://localhost:7032");
        private IBlogAPI CallApi()
        { 
            var handler = new HttpClientHandler
            { 
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
             
            var httpClient = new HttpClient(handler)
            {
                BaseAddress=new Uri( "https://localhost:7032")
            };

            // Set up Refit RestService
            var api = RestService.For<IBlogAPI>(httpClient);
            return api;
        }

        public async void Run()
        {
            await ReadAsync();
            await EditBlog(1);
            await EditBlog(2);
            await UpdateAsync(8, "test 14", "test 000", "test 1");
            await CreateAsync("test 14", "test 12", "test 1");
            await DeleteAsyn(1);
        }
        public async Task ReadAsync()
        {
            var lst = await CallApi().GetBlogs();
            foreach (var blog in lst)
            {
                Console.WriteLine($"Id {blog.BlogId}");
                Console.WriteLine($"Title {blog.BlogTitle}");
                Console.WriteLine($"Author {blog.BlogAuthor}");
                Console.WriteLine($"Content {blog.BlogContent}");
                Console.WriteLine($"-------------------------------");

            }

        }
        public async Task EditBlog(int id)
        {
        
            try
            {
                var blog = await CallApi().GetBlog(id);
                Console.WriteLine($"Id {blog.BlogId}");
                Console.WriteLine($"Title {blog.BlogTitle}");
                Console.WriteLine($"Author {blog.BlogAuthor}");
                Console.WriteLine($"Content {blog.BlogContent}");
            }
            catch (ApiException e)
            {
                Console.WriteLine(e.StatusCode);
                Console.WriteLine(e.Content);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
        }
        private async Task UpdateAsync(int id, string bcontent, string author, string title)
        {
            BlogModel blog = new BlogModel()
            {
                BlogAuthor = author,
                BlogContent = bcontent,
                BlogTitle = title
            }; 
            try
            {
                var message = await CallApi().UpdateAsync(id, blog);
                Console.WriteLine(message);
            }
            catch(ApiException e)
            {
                Console.WriteLine(e.StatusCode);
                Console.WriteLine(e.Content);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
        }
        public async Task CreateAsync(string bcontent, string author, string title)
        {
            
            BlogModel blog = new BlogModel()
            {
                BlogAuthor = author,
                BlogContent = bcontent,
                BlogTitle = title
            };
            var message = await CallApi().CreateAsync(blog);
            Console.WriteLine(message);
        }
        public async Task DeleteAsyn(int id)
        {
            var message = await CallApi().DeleteAsync(id);
            Console.WriteLine(message);
        }
    }
}
