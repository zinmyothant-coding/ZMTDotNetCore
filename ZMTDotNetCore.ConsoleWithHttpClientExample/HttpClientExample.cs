using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ZMTDotNetCore.ConsoleAppHttpClientExamples
{
    internal class HttpClientExample
    {
        private readonly string _endPoint = "api/Blog";
        private HttpClient GetHttpClient()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
            var client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:7032") };
            return client;
        }
        public async void Run()
        {
            // await  ReadAsync();
            await EditAsync(2);
            await DeleteAsync(2);
            await CreateAsync("test0", "test", "test");
        }
        public async Task ReadAsync()
        {
            var response = await GetHttpClient().GetAsync(_endPoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                List<BlogModel> list = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
                foreach (var blog in list)
                {
                    Console.WriteLine($"Title =>{blog.BlogTitle}");
                    Console.WriteLine($"Author =>{blog.BlogAuthor}");
                    Console.WriteLine($"Content =>{blog.BlogContent}");
                }
            }
        }

        public async Task EditAsync(int id)
        {
            var response = await GetHttpClient().GetAsync($"{_endPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogModel blog = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;

                Console.WriteLine($"Title =>{blog.BlogTitle}");
                Console.WriteLine($"Author =>{blog.BlogAuthor}");
                Console.WriteLine($"Content =>{blog.BlogContent}");

            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var response = await GetHttpClient().DeleteAsync($"{_endPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
        public async Task CreateAsync(string bcontent, string author, string title)
        {
            BlogModel blog = new()
            {
                BlogAuthor = author,
                BlogContent = bcontent,
                BlogTitle = title
            };
            var json = JsonConvert.SerializeObject(blog);
            HttpContent content = new StringContent(json, Encoding.UTF8, Application.Json);
            var response = await GetHttpClient().PostAsync(_endPoint, content);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
    }
}
