using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMTDotNetCore.ConsoleApp.Services;

internal static class ConnectionStrings
{
    public static SqlConnectionStringBuilder connectionStrings = new SqlConnectionStringBuilder()
    {
        DataSource = "(localdb)\\MSSqlLocalDb",//server name
        InitialCatalog = "DotNetTrainingBatch4",//database name
        UserID = "sa0",
        Password = "sa@12345",
        TrustServerCertificate = true

    };

}
