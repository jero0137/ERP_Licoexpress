using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Services;
using GestionTransporte_CS_API_PostgresSQL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ERP_LicoExpress_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : Controller
    {
        private readonly LocationService _locationService;
        public LocationsController(LocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasLocation = await _locationService
                .GetAllAsync();

            return Ok(lasLocation);
        }



        [HttpGet("{location_id:int}")]
        public async Task<IActionResult> GetByIdAsync(int location_id)
        {
            try
            {
                var unaLocation = await _locationService
                    .GetByIdAsync(location_id);
                return Ok(unaLocation);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        /* FALTAAA

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
        */
    }
}
