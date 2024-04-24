using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMTDotNetCore.ConsoleApp.Dtos;
using ZMTDotNetCore.ConsoleApp.Services;
using ZMTDotNetCore.Shared;

namespace ZMTDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {
        private readonly DrapperService drapperService = new DrapperService(ConnectionStrings.connectionStrings.ConnectionString);
        public void Run()
        {
            Read();
            Edit(1);
            Edit(11);
            Create(0, "title_1", "author_1", "content_1");
            Update(1, "title_2", "author_2", "content_2");
            Delete(3);
        }
        public void Read()
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);

            List<BlogDto> lst = drapperService.Query<BlogDto>("select * from Tbl_Blog");
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("----------------");

            }

        }
        public void Edit(int Id)
        {
            var item = drapperService.QueryFirstOrDefault<BlogDto>("select * from Tbl_Blog where BlogId=@BlogId", new BlogDto { BlogId = Id });
            if (item is null)
            {
                Console.WriteLine("No Data Found");
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
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ( 
            [BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (  
            @BlogTitle
           , @BlogAuthor
           ,@BlogContent );";
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            int result = drapperService.Excute<BlogDto>(query, item);
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            Console.WriteLine(message);
        }
        public void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content


            };
            string query = @"Update Tbl_Blog set [BlogTitle]=@BlogTitle,[BlogAuthor]=@BlogAuthor,[BlogContent]=@BlogContent where BlogId=@BlogId;";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            int result = drapperService.Excute<BlogDto>(query, item);
            string message = result > 0 ? "Updated Successfully! " : "Updated Fail!";
            Console.WriteLine(message);
        }
        public void Delete(int id)
        {
            var item = new BlogDto
            {
                BlogId = id
            };
            string query = "DELETE Tbl_Blog where BlogId=@BlogId";
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            int result = drapperService.Excute<BlogDto>(query, item);
            Console.WriteLine(result > 0 ? "Delete Successfully!" : "Delete Fail!");
        }
    }

}
