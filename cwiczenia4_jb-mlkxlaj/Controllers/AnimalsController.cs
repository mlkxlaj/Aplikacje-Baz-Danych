using Microsoft.AspNetCore.Mvc;
using Zadanie4.Model;
using Zadanie4.Service;

namespace Zadanie4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalsService _animalsService = new AnimalsService();
        [HttpGet]
        public ActionResult Get([FromQuery] string param)
        {
            if (_animalsService.GetAnimals(param).Equals(null))
            {
                return BadRequest("wrong parameter");
            }
            else
            {
                var animals = _animalsService.GetAnimals(param);
                return Ok(animals);
            }
        }
        
        [HttpPost]
        public ActionResult Post([FromQuery]InsertAnimal param)
        {
            _animalsService.PutAnimal(param);
            return Ok("Animal has been added");
        }
        
        [HttpPut("{id:int}")]
        public ActionResult UpdateAnimal(int id, InsertAnimal animal)
        {
            var result = _animalsService.PutAnimal(id, animal);
            if (!result)
            {
                return NotFound();
            }
            else
            {
                return Ok("Animal has been updated");
            }
            
        }

        
        [HttpDelete("{id:int}")]
        public ActionResult DeleteAnimal(int id)
        {
            var animalToDelete = _animalsService.DeleteAnimal(id);

            if (!animalToDelete)
            {
                return NotFound();
            }
            else
            {
                return Ok("Animal has been deleted");
            }
        }
    }
}