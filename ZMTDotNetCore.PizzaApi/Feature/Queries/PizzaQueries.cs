namespace ZMTDotNetCore.PizzaApi.Feature.Queries
{
    public class PizzaQueries
    {
        public static string GetPizza { get; } = @"select po.*,p.PizzaName,p.Price from Tbl_PizzaOrder po inner join Tbl_Pizza p
                                        on po.PizzaId=p.PizzaId where po.InvoiceNo=@InvoiceNo;";
        public static string GetPizzaDetail { get; } = @"select pod.*,pe.PizzaExtraName,pe.Price from Tbl_PizzaOrderDetail pod inner join Tbl_PizzaExtra pe
                                            on pod.PizzaExtraId=pe.PizzaExtraId where pod.InvoiceNo=@InvoiceNo;";
    }
}
