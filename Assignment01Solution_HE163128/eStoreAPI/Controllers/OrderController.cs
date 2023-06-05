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
    public class OrderController : ControllerBase
    {
        private IOrderRepository repository = new OrderRepository();

        // GET: api/Orders
        [HttpGet("GetAllOrder")]
        public ActionResult<IEnumerable<Order>> GetOrders(string? keyword) => repository.GetOrders(keyword);

        [HttpPost("AddOrder")]
        public IActionResult AddOrder(OrderRequestDto p)
        {
            repository.SaveOrder(p);
            return NoContent();
        }

        [HttpGet("Detail/{id}")]
        public ActionResult<Order> GetOrderById(int id) => repository.GetOrderById(id);

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var p = repository.GetOrderById(id);
            if (p == null)
            {
                return NotFound("Can not found Order to delete");
            }
            repository.DeleteOrder(p);
            return NoContent();
        }

        [HttpPut("Update")]
        public IActionResult UpdateOrder(OrderUpdateRequestDto p)
        {
            var pTmp = repository.GetOrderById(p.OrderId);
            if (pTmp == null)
            {
                return NotFound($"Can not find Order have id {p.OrderId}");
            }
            repository.UpdateOrder(p);
            return NoContent();
        }
    }
}
