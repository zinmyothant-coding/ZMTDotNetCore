using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMTDotNetCore.WinFormsApp
{
    public class Queries
    {
        public static string CreateBlog { get; }= @"INSERT INTO [dbo].[Tbl_Blog]
                                                   ( 
                                                    [BlogTitle]
                                                   ,[BlogAuthor]
                                                   ,[BlogContent])
                                             VALUES
                                                   (  
                                                    @BlogTitle
                                                   , @BlogAuthor
                                                   ,@BlogContent );";
    }
}
