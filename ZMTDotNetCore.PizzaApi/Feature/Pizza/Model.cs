namespace ZMTDotNetCore.PizzaApi.Feature.Pizza
{
    public class Model
    {
    }
    public class OrderRequestModel
    {
        public int PizzaId { get; set; }
        public int[] PizzaExtraId { get; set; }
    }
    public class OrderResponseModel
    {
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string InvoiceNo { get; set; }
    }
    public class  OrderResponse
    {
        public int OrderId { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalPrice { get; set; }
        public string PizzaName { get; set; }
        public decimal Price { get; set; }
    }
    public class OrderDetailResponse
    {
        public int OrderDetailId { get; set; }
        public string InvoiceNo { get; set; }
        public int PizzaExtraId { get; set; }
        public string PizzaName { get; set; }
        public decimal Price { get; set; }
    }
}
