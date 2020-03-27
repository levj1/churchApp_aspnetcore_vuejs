using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Models;
using ChurchAppAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("api/donationTypes")]
    public class DonationTypesController : ControllerBase
    {
        private IDonationTypeRepository _donationTypeRepository;
        private IMapper _mapper;
        public DonationTypesController(IDonationTypeRepository donationTypeRepository,
            IMapper mapper)
        {
            _donationTypeRepository = donationTypeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var types = _donationTypeRepository.GetAll();
            var typesDto = _mapper.Map<List<DonationTypeDto>>(types);
            return Ok(typesDto);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var type = _donationTypeRepository.Get(id);
            if (type == null) return NotFound();

            var typeDto = _mapper.Map<DonationTypeDto>(type);

            return Ok(typeDto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DonationTypeCreateDto type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var typeEntity = _mapper.Map<DonationType>(type);
            _donationTypeRepository.Create(typeEntity);
            if (!_donationTypeRepository.Save())
            {
                return StatusCode(500, "An error occured while saving your change");
            }
            var typeDto = _mapper.Map<DonationTypeDto>(typeEntity);

            return CreatedAtAction("Post", new { id = typeEntity.ID }, typeDto);
        }

        [HttpPut]
        public IActionResult Put(DonationTypeDto type)
        {
            var typeInDb = _donationTypeRepository.Get(type.Id);

            if (typeInDb == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _mapper.Map(type, typeInDb);
            if (!_donationTypeRepository.Save())
            {
                return StatusCode(500, "An error occured while saving your change");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody] JsonPatchDocument<DonationTypeDto> patchDoc, int id)
        {
            if (patchDoc == null) return BadRequest();

            var typeEntity = _donationTypeRepository.Get(id);
            if (typeEntity == null) return NotFound();

            var typeToPatch = _mapper.Map<DonationTypeDto>(typeEntity);
            patchDoc.ApplyTo(typeToPatch, ModelState);
            TryValidateModel(typeToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _mapper.Map(typeToPatch, typeEntity);

            if (!_donationTypeRepository.Save())
            {
                return StatusCode(500, "Something went wrong");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var donationtType = _donationTypeRepository.Get(id);
            if (donationtType == null) return NotFound();

            _donationTypeRepository.Delete(donationtType);
            if (!_donationTypeRepository.Save())
            {
                return StatusCode(500, "An error occured while saving your change");
            }

            return NoContent();
        }
    }
}
