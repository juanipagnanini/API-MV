using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Getting all the data");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int Id)
        {
            if (Id == 0)
            {
                _logger.LogError("Error fetching data with id" + Id);
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.Id == Id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Villas.FirstOrDefault(v => v.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("NameExist", "Name is in use");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa model = new()
            {
                Name = villaDto.Name,
                Description = villaDto.Description,
                Rate = villaDto.Rate,
                Occupants = villaDto.Occupants,
                SquareMeters = villaDto.SquareMeters,
                ImagenUrl = villaDto.ImagenUrl,
                Services = villaDto.Services
            };

            _db.Villas.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public IActionResult VillaDelete (int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult VillaUpdate (int id, [FromBody] VillaDto villaDto)
        {
            if (id == null || id != villaDto.Id)
            {
                return BadRequest();
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Description = villaDto.Description,
                Rate = villaDto.Rate,
                Occupants = villaDto.Occupants,
                SquareMeters = villaDto.SquareMeters,
                ImagenUrl = villaDto.ImagenUrl,
                Services = villaDto.Services,
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();
            
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult VillaPartialUpdate (int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if(patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            VillaDto villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Description = villa.Description,
                Rate = villa.Rate,
                Occupants = villa.Occupants,
                SquareMeters= villa.SquareMeters,
                ImagenUrl= villa.ImagenUrl,
                Services = villa.Services,
            };

            if (villa == null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Description = villaDto.Description,
                Rate = villaDto.Rate,
                Occupants = villaDto.Occupants,
                SquareMeters = villaDto.SquareMeters,
                ImagenUrl = villaDto.ImagenUrl,
                Services = villaDto.Services,
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
