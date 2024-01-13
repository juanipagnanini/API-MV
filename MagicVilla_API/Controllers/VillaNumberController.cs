using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly ILogger<VillaNumberController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly IVillaNumberRepository _numberRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        
        public VillaNumberController(ILogger<VillaNumberController> logger, IVillaRepository villaRepo, IVillaNumberRepository numberRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numberRepo = numberRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillasNumbers()
        {
            try
            {
                _logger.LogInformation("Getting all villa numbers");
                IEnumerable<VillaNumber> villaNumberList = await _numberRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<VillaNumberDto>>(villaNumberList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMenssages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("id:int", Name = "GetVillaNumbers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    _logger.LogError("Error fetching villa number with id" + Id);

                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }

                var villaNumber = await _numberRepo.Get(v => v.VillaNum == Id);

                if (villaNumber == null)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.NotFound;

                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMenssages = new List<string>() { ex.ToString() };

                
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numberRepo.Get(v => v.VillaNum == createDto.VillaNum) != null)
                {
                    ModelState.AddModelError("NumberExist", "Number is in use");
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Get(v => v.Id == createDto.VillaId) == null)
                {
                    ModelState.AddModelError("ForeignKey", "The villa Id does not exist!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                VillaNumber model = _mapper.Map<VillaNumber>(createDto);

                model.DateCreated = DateTime.Now;
                model.DateUpdated = DateTime.Now;

                await _numberRepo.Create(model);

                _response.Result = model; 
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = model.VillaNum }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMenssages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<IActionResult> VillaNumberDelete (int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }

                var villaNumber = await _numberRepo.Get(v => v.VillaNum == id);

                if (villaNumber == null)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.NotFound;

                    return NotFound(_response);
                }

                await _numberRepo.Remove(villaNumber);

                _response.statusCode = HttpStatusCode.NoContent;
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMenssages = new List<string>() { ex.ToString() };

            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> VillaNumberUpdate (int id, [FromBody] VillaNumberUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.VillaNum)
            {
                _response.IsSuccessful = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _villaRepo.Get(v => v.Id == updateDto.VillaId) == null)
            {
                ModelState.AddModelError("ForeignKey", "The villa Id does not exist!");
                return BadRequest(ModelState);
            }

            VillaNumber model = _mapper.Map<VillaNumber>(updateDto);

            await _numberRepo.Update(model);

            _response.statusCode = HttpStatusCode.NoContent;
            
            return Ok(_response);
            
        }
    }
}
