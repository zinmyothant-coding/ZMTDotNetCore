using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ZMTDotNetCore.ConsoleAppRestClientExamples
{
    internal class HttpClientExample
    {
        private readonly string _endPoint = "api/Blog";
        //private readonly string _endPoint = "api/Blog/Create";
        private readonly RestClient _client=new RestClient(new Uri("https://localhost:7032"));
        private RestClient GetHttpClient()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
            var client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:7032") };
           RestClient restClient = new RestClient(client);
            return restClient;
        }
        public async void Run()
        {
            //     await  ReadAsync();
            //    await EditAsync(2);
            await DeleteAsync(3);
            //await CreateAsync("test0", "test", "test");
        }
        public async Task ReadAsync()
        {
            RestRequest request = new RestRequest(_endPoint,Method.Get);
            var response = await GetHttpClient().ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
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
            RestRequest request = new RestRequest($"{_endPoint}/{id}", Method.Get);
            var response = await GetHttpClient().ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =   response.Content!;
                BlogModel blog = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;

                Console.WriteLine($"Title =>{blog.BlogTitle}");
                Console.WriteLine($"Author =>{blog.BlogAuthor}");
                Console.WriteLine($"Content =>{blog.BlogContent}");

            }
            else
            {
                string message =   response.Content!;
                Console.WriteLine(message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            RestRequest request = new RestRequest($"{_endPoint}/{id}", Method.Delete);
            var response = await GetHttpClient().ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
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
            RestRequest request = new RestRequest(_endPoint, Method.Post);
            request.AddBody(blog);
            var response = await GetHttpClient().ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
    }
}
