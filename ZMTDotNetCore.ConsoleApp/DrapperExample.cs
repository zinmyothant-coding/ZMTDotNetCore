using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMTDotNetCore.ConsoleApp
{
    internal class DrapperExample
    {
        public void Run() 
        {
            Read();
            Edit(1);
            Edit(11);
            Create(0,"title_1","author_1","content_1");
            Update(1, "title_2", "author_2", "content_2");
            Delete(3);
        }
        public void Read()
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
          
          List<BlogDto> lst=  dbConnection.Query<BlogDto>("select * from Tbl_Blog").ToList();
          foreach(BlogDto item in lst)
            {
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("----------------");

            }

        }
        public void Edit(int Id)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            var item = dbConnection.Query("select * from Tbl_Blog where BlogId=@BlogId", new BlogDto { BlogId = Id }).FirstOrDefault();
            if(item is null)
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
           int result= dbConnection.Execute(query,item);
            string message = result > 0 ? "Save Successfully! " : "Save Fail!";
            Console.WriteLine(message);
        }
        public void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId=id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content


            };
            string query = @"Update Tbl_Blog set [BlogTitle]=@BlogTitle,[BlogAuthor]=@BlogAuthor,[BlogContent]=@BlogContent where BlogId=@BlogId;";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.connectionStrings.ConnectionString);
            int result = dbConnection.Execute(query, item);
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
            int result = dbConnection.Execute(query, item);
            Console.WriteLine(result > 0 ? "Delete Successfully!" : "Delete Fail!");
        }
    }
   
}
