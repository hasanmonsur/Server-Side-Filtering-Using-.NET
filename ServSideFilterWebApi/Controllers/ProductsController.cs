using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServSideFilterWebApi.DTOs;
using ServSideFilterWebApi.Services;

namespace ServSideFilterWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredData([FromBody] FilterOptions filters)
        {
            var result = await _productRepository.GetFilteredDataAsync(filters);
            return Ok(result);
        }
    }
}
