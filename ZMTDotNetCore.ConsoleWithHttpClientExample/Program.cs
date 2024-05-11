// See https://aka.ms/new-console-template for more information
using System.Net;
using ZMTDotNetCore.ConsoleAppHttpClientExamples;

Console.WriteLine("Hello, World!");
 HttpClientExample httpClientExample = new HttpClientExample();
httpClientExample.Run();

Console.ReadKey();