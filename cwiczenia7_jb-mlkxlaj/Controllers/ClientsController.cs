using Microsoft.AspNetCore.Mvc;
using Zadanie7.Services;

namespace Zadanie7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpDelete]
        public IActionResult delete(int id)
        {
            var result = _clientsService.deleteClients(id);

            if(result == 1) { 
            
                return NotFound("No clients with that id");

            }else if(result == 2)
            {
                return NoContent();
            }
            else
            {
                return Ok("Client with id:" + id + " deleted");
            }
        }
    }
}
