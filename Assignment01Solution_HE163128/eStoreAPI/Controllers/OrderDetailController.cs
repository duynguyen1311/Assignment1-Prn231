using BusinessObjects.Models;
using DataAccess.Dto;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private IOrderDetailRepository repository = new OrderDetailRepository();
        // GET: api/OrderDetails
        [HttpGet("Detail/{id}")]
        public ActionResult<OrderDetail> GetOrderDetails(int Id) => repository.FindOrderDetailById(Id);

        [HttpPut("Update")]
        public IActionResult UpdateOrderDetail(OrderDetailUpdateRequestDto p)
        {
            var pTmp = repository.FindOrderDetailById(p.OrderId);
            if (pTmp == null)
            {
                return NotFound($"Can not find OrderDetail have id {p.OrderId}");
            }
            repository.UpdateOrderDetail(p);
            return NoContent();
        }

        [HttpGet("Report")]
        public ActionResult<List<OrderDetail>> ReportOrder(DateTime? fromDate, DateTime? toDate) => repository.ReportOrder(fromDate, toDate);
    }
}
