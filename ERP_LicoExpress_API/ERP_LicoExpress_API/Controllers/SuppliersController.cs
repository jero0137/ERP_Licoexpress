using ERP_LicoExpress_API.Services;
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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losSuppliers = await _supplierService
                .GetAllAsync();

            return Ok(losSuppliers);
        }
    }
}
