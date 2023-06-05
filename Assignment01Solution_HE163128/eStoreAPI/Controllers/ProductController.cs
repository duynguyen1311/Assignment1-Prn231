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
    public class ProductController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();

        // GET: api/Products
        [HttpGet("GetAllProduct")]
        public ActionResult<IEnumerable<Product>> GetProducts(string? keyword, int? unitP) => repository.GetProducts(keyword,unitP);

        [HttpGet("GetAllCategory")]
        public ActionResult<IEnumerable<Category>> GetCategory() => repository.GetCategories();

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductRequestDto p)
        {
            repository.SaveProduct(p);
            return NoContent();
        }

        [HttpGet("Detail/{id}")]
        public ActionResult<Product> GetProductById(int id) => repository.GetProductById(id);

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null)
            {
                return NotFound("Can not found Product to delete");
            }
            repository.DeleteProduct(p);
            return NoContent();
        }

        [HttpPut("Update")]
        public IActionResult UpdateProduct(ProductUpdateRequestDto p)
        {
            var pTmp = repository.GetProductById(p.ProductId);
            if (pTmp == null)
            {
                return NotFound($"Can not find Product have name {p.ProductName}");
            }
            repository.UpdateProduct(p);
            return NoContent();
        }
    }
}
