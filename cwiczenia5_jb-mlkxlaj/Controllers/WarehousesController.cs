using Microsoft.AspNetCore.Mvc;
using Zadanie5.DTOs;
using Zadanie5.Services;

namespace Zadanie5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehousesController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        public ActionResult AddProduct(ProductDTO product)
        {
            var result = _warehouseService.AddProduct(product);

            if (result == -1)
            {
                return NotFound("Product or warehouse not found.");
            }
            else if (result == -2)
            {
                return NotFound("Order not found.");
            }
            else if (result == -3)
            {
                return StatusCode(409, "Order has already been fulfilled.");
            }
            else
            {
                return Ok("ok");
            }
        }
    }
}
