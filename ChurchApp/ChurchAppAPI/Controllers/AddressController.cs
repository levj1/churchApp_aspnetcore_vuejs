using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Models;
using ChurchAppAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChurchAppAPI.Controllers
{

    [EnableCors("CorsPolicy")]
    [Route("api/persons/{personId}/addresses")]
    [ApiController]
    public class AddressController: ControllerBase
    {
        private IAddressRepository _addressRepository;
        private IPersonRepository _personRepository;
        private IMapper _mapper;
        public AddressController(IAddressRepository addressRepository,
            IMapper mapper,
            IPersonRepository personRepository)
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
            _personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult Get(int personId)
        {
            var addresses = _addressRepository.GetAddressesForPerson(personId);
            var addressDto = _mapper.Map<List<AddressDto>>(addresses);
            return Ok(addressDto);
        }

        [HttpGet("{addressId}")]
        public IActionResult Get(int personId, int addressId)
        {
            var address = _addressRepository.GetAddressForPerson(personId, addressId);
            if (address == null)
                return NotFound();
            return Ok(address);
        }

        [HttpPost]
        public ActionResult Post([FromBody] AddressCreateDto addressDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var addressEntity = _mapper.Map<Address>(addressDto);

            _addressRepository.Create(addressEntity);
            if (!_personRepository.Save())
            {
                return StatusCode(500, "An error occured while making the change");
            }

            return CreatedAtAction("Post", new { id = addressEntity.ID }, addressEntity);
        }

        [HttpPut]
        public IActionResult Put(int personId, [FromBody]AddressWithoutAddressTypeDto address)
        {
            var person = _personRepository.GetPerson(personId);
            if(person == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var addressEntity = _addressRepository.GetAddressForPerson(personId, address.ID);
            if (addressEntity == null) return NotFound();

            _mapper.Map(address, addressEntity);

            if (!_addressRepository.Save())
            {
                return StatusCode(500, "An error occured");
            }
            
            return NoContent(); 
        }

        [HttpPatch("{addressId}")]
        public IActionResult Patch([FromBody] JsonPatchDocument<AddressWithoutAddressTypeDto> patchDoc, int personId, int addressId)
        {
            if (patchDoc == null) return BadRequest();

            var addressEntity = _addressRepository.GetAddressForPerson(personId, addressId);

            if (addressEntity == null) return NotFound();

            var addressToPatch = _mapper.Map<AddressWithoutAddressTypeDto>(addressEntity);

            patchDoc.ApplyTo(addressToPatch, ModelState);
            TryValidateModel(addressToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(addressToPatch, addressEntity);

            if (!_addressRepository.Save())
            {
                return StatusCode(500, "Something went wrong.");
            }

            return NoContent();

        }

        [HttpDelete("{addressId}")]
        public IActionResult Delete(int personId, int addressId)
        {
            var address = _addressRepository.GetAddressForPerson(personId, addressId);
            if(address == null)
            {
                return NotFound();
            }

            _addressRepository.Delete(address);
            if (!_addressRepository.Save())
            {
                return StatusCode(500, "An error occured while processing your request");
            }

            return NoContent();
        }
      
    }
}
