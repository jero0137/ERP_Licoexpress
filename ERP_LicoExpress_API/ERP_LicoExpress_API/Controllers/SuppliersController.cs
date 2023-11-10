using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Services;
using GestionTransporte_CS_API_PostgresSQL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_LicoExpress_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : Controller
    {
        private readonly SupplierService _supplierService;
        public SuppliersController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losSuppliers = await _supplierService
                .GetAllAsync();

            return Ok(losSuppliers);
        }



        [HttpGet("{supplier_id:int}")]
        public async Task<IActionResult> GetByIdAsync(int supplier_id)
        {
            try
            {
                var unSupplier = await _supplierService
                    .GetByIdAsync(supplier_id);
                return Ok(unSupplier);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }



        [HttpDelete("{supplier_id:int}")]
        public async Task<IActionResult> DeleteAsync(int supplier_id)
        {
            try
            {
                await _supplierService
                    .DeleteAsync(supplier_id);

                return Ok($"Proveedor {supplier_id} fue eliminado");

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
        public async Task<IActionResult> CreateAsync(Supplier unSupplier)
        {
            try
            {
                var supplierCreado = await _supplierService
                    .CreateAsync(unSupplier);

                return Ok(supplierCreado);
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
