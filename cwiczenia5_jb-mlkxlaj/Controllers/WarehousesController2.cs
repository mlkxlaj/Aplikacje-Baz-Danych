using Microsoft.AspNetCore.Mvc;
using Zadanie5.DTOs;
using Zadanie5.Services;


namespace Zadanie5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController2 : ControllerBase
    {

        private readonly IWarehouseService _warehouseService;

        public WarehousesController2(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }
        [HttpPost]
        public ActionResult AddProduct(ProductDTO product)
        {
            var result = _warehouseService.AddProductProcedure(product);

            if (result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

   