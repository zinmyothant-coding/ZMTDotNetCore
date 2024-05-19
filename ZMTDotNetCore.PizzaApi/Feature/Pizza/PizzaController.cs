using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZMTDotNetCore.PizzaApi.Db;
using ZMTDotNetCore.PizzaApi.Feature.Queries;
using ZMTDotNetCore.Shared;
using static ZMTDotNetCore.PizzaApi.Db.AddDbContent;

namespace ZMTDotNetCore.PizzaApi.Feature.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly DrapperService _drapperService;
        private readonly AddDbContent _addDbContent; 
        public PizzaController()
        {
            _addDbContent = new AddDbContent();
            _drapperService = new DrapperService(ConnectionStrings.connectionStrings.ConnectionString);
        }
        [HttpGet]
        public IActionResult GetData()
        {
            var lst = _addDbContent.Pizzas.ToList();
            return Ok(lst);
        }
        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtraData()
        {
            var extraLst =await _addDbContent.PizzaExtras.ToListAsync();
            return Ok(extraLst);
        }
        // [HttpGet("Order/{invoiceNo}")]
        //public IActionResult GetOrderDetail(string invoiceNo)
        //{
        //    var item=_addDbContent.PizzaOrders.FirstOrDefault(x=>x.InvoiceNo== invoiceNo);
        //    var lst=_addDbContent.PizzaOrderDetails.Where(x=>x.InvoiceNo== invoiceNo).ToList();
        //    return Ok(new
        //    {
        //        Order = item,
        //        list = lst
        //    });
        //}
        [HttpGet("Order/{invoiceNo}")]
        public IActionResult GetOrderDetail(string invoiceNo)
        {
            var getOrder = _drapperService.QueryFirstOrDefault<OrderResponse>(PizzaQueries.GetPizza, new { InvoiceNo = invoiceNo });
            var getDetail = _drapperService.Query<OrderDetailResponse>(PizzaQueries.GetPizzaDetail, new { InvoiceNo = invoiceNo }).ToList();
            return Ok(new
            {
                Order = getOrder,
                Detail = getDetail
            });
        }
        [HttpPost("Order")]
        public async Task<IActionResult> GetOrder(OrderRequestModel requestModel)
        {
            var getPizza = _addDbContent.Pizzas.FirstOrDefault(x => x.Id == requestModel.PizzaId);
            if (getPizza is null) { return NotFound(); }
            var totalPrice = getPizza.Price;
            if(requestModel.PizzaExtraId.Length>0)
            {
                var extraData = _addDbContent.PizzaExtras.Where(x => requestModel.PizzaExtraId.Contains(x.Id)).ToList();
                totalPrice += extraData.Sum(x => x.Price);
            }
            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            PizzaOrder order = new PizzaOrder()
            {
                PizzaId = requestModel.PizzaId,
                InvoiceNo = invoiceNo,
                TotalPrice = totalPrice
            };
            List<PizzaOrderDetail> orderDetail = requestModel.PizzaExtraId.Select(x => new PizzaOrderDetail
            {
                InvoiceNo = invoiceNo,
                PizzaExtraId = x
            }).ToList();
            await _addDbContent.AddAsync(order);
            await _addDbContent.AddRangeAsync(orderDetail);
            await _addDbContent.SaveChangesAsync();
            OrderResponseModel reponseModel = new OrderResponseModel()
            {
                InvoiceNo = invoiceNo,
                Status = "Thank you for your order!",
                TotalPrice = totalPrice
            };
            return Ok(reponseModel);
        } 
    }
}
