using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // Get : api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }
        // Get: api/products/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        //Post: api/products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var created = await _productService.CreateProductAsync(product);
            if (created == null) return NotFound();
            return Ok(created);
        }
        //Put: api/products/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Product ID mismatch");
            }
            var result = await _productService.UpdateProductAsync(product);
            return Ok(result);
        }
        //Delete: api/products/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result=await _productService.DeleteAsync(id);
            if(result==null)return NotFound();
            return Ok("Product Deleted");
        }
        //Put: api/products/decrement-stock/id/quantity
        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            var result=await _productService.AdjustStock(id,quantity,isAdd:false);
            return result ? Ok("Stock decremented") : NotFound();
        }
        //Put:api/products/add-to-stock/id/quantity
        [HttpPut("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> AddToStock(int id,int quantity)
        {
            var result=await _productService.AdjustStock(id, quantity,isAdd:true);
            return result ? Ok("Stock Incremented") : NotFound();
        }
    }
}
