using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zadanie7.DTOs;
using Zadanie7.Services;

namespace Zadanie7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {   
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }


        [HttpGet]
        public IActionResult Get()
        { 
            return Ok(_tripService.GetTripOrderBy());
        }

        [HttpPost("{idTrip}/clients")]
        public IActionResult postClient(int idTrip, ClientDTO client)
        {
            var result = _tripService.post(idTrip, client);

            if(result == 0)
            {
                return Ok("Client Assigned");
            }else if(result == 1)
            {
                return BadRequest("No trip with this id: " + client.idTrip);
            }else if (result == 2)
            {
                return BadRequest("Already assigned");
            }
            else
            {
                return BadRequest("Wrong inputs");
            }
        }
    }
}
