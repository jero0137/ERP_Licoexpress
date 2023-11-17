using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Services;
using ERP_LicoExpress_API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ERP_LicoExpress_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losProductos = await _productService
                .GetAllAsync();

            return Ok(losProductos);
        }



        [HttpGet("{product_id:int}")]
        public async Task<IActionResult> GetByIdAsync(int product_id)
        {
            try
            {
                var unProduct = await _productService
                    .GetByIdAsync(product_id);
                return Ok(unProduct);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }


        [HttpDelete("{product_id:int}")]
        public async Task<IActionResult> DeleteAsync(int product_id)
        {
            try
            {
                await _productService
                    .DeleteAsync(product_id);

                return Ok($"Producto {product_id} fue eliminado");

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Product unProduct)
        {
            try
            {
                var productCreado = await _productService
                    .CreateAsync(unProduct);

                return Ok(productCreado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }


        [HttpPut("{producto_id:int}")]
        public async Task<IActionResult> UpdateAsync(int producto_id, Product unProducto)
        {
            try
            {
                var productActualizado = await _productService
                    .UpdateAsync(producto_id, unProducto);

                return Ok(productActualizado);

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

    }
    
}
