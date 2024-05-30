
using Microsoft.Data.SqlClient;

namespace ZMTDotNetCore.RestApiWithNLayer;

internal static class ConnectionStrings
{
    public static SqlConnectionStringBuilder connectionStrings = new SqlConnectionStringBuilder()
    {
        DataSource = "(localdb)\\MSSqlLocalDb",//server name
        InitialCatalog = "DotNetTrainingBatch4",//database name
        UserID = "sa0",
        Password = "sa@12345",
        TrustServerCertificate = true
        //DataSource = ".",// "DESKTOP-SSEBD4Q",//server name
        //InitialCatalog = "DotNetTrainingBatch4",//database name
        //UserID = "sa",
        //Password = "sa@123",
        //TrustServerCertificate = true
    };

}
