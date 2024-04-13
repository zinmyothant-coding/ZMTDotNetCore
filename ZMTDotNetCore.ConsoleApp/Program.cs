﻿
using System.Data;
using System.Data.SqlClient;


Console.WriteLine("Hello, World!");
SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = "(localdb)\\MSSqlLocalDb";//server name
stringBuilder.InitialCatalog = "DotNetTrainingBatch4";//database name
stringBuilder.UserID = "sa0";
stringBuilder.Password = "sa@12345";
SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
connection.Open();
Console.WriteLine("Connection Open"); 
string query = "select * from Tbl_Blog";
SqlCommand command = new SqlCommand(query, connection);
SqlDataAdapter adapter = new SqlDataAdapter(command);
DataTable dt=new DataTable();
adapter.Fill(dt);   
connection.Close();
Console.WriteLine("Connection Close");
//dataset ==> data table
//data table==> data row
//data row ==> data column
foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine("Blog Id =>" + dr["BlogId"]);
    Console.WriteLine("Blog Title =>" + dr["BlogTitle"]);
    Console.WriteLine("Blog Author =>" + dr["BlogAuthor"]);
    Console.WriteLine("Blog Contenct =>" + dr["BlogContent"]);
    Console.WriteLine("--------------------");
}
// ADO . Net  Read
//CRUD
Console.ReadKey();