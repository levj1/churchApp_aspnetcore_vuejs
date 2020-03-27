using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Models;
using ChurchAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/persons")]
    [ApiController]
    //[Authorize]
    public class PersonsController : ControllerBase
    {
        private IPersonRepository _personRepository;
        private IAddressRepository _addressRepository;
        private IMapper _mapper;

        public PersonsController(IPersonRepository personRepository,
            IAddressRepository addressRepository,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetPersons([FromQuery] bool includeAddress = false, bool includeDonations = false)
        {
            var persons = _personRepository.GetPeople(includeAddress, includeDonations);
            var personDtos = _mapper.Map<List<PersonDto>>(persons);

            return Ok(personDtos);
        }

        [HttpGet("{id}")]
        public ActionResult GetPerson(int id, [FromQuery] bool includeAddress = false, bool includeDonations = false)
        {
            var person = _personRepository.GetPerson(id, includeAddress, includeDonations);
            if (person == null) { return NotFound(); }

            var personDto = _mapper.Map<PersonDto>(person);

            return Ok(personDto);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreatePersonDto personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }        

            
            var personWithoutAddress = _mapper.Map<PersonWithoutAddressDto>(personDto);
            var personEntity = _mapper.Map<Person>(personWithoutAddress);

            _personRepository.Add(personEntity);
            if (!_personRepository.Save())
            {
                return StatusCode(500, "An error occured while making the change");
            }

            foreach (var address in personDto.Addresses)
            {
                address.PersonID = personEntity.ID;
                var addressEntity = _mapper.Map<Address>(address);
                _addressRepository.Create(addressEntity);
                if (!_addressRepository.Save())
                {
                    return StatusCode(500, "An error occured while making the change");
                }
            }

            return CreatedAtAction("Post", new { id = personEntity.ID }, personEntity);
        }

        [HttpPut]
        public IActionResult Update([FromBody]PersonWithoutAddressDto person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }   

            var personInDb = _personRepository.GetPerson(person.ID, false, false);
            if(personInDb == null)
            {
                return NotFound();
            }

            _mapper.Map(person, personInDb);

            if (!_personRepository.Save())
            {
                return StatusCode(500, "An error occured while processing request.");
            }                     

            return NoContent();
        }

        [HttpPatch("{personId}")]
        public IActionResult Patch([FromBody] JsonPatchDocument<PersonWithoutAddressDto> patchDoc, int personId)
        {
            if (patchDoc == null) return BadRequest();

            var personEntity = _personRepository.GetPerson(personId, false, false);

            if (personEntity == null) return NotFound();

            var personToPatch = _mapper.Map<PersonWithoutAddressDto>(personEntity);

            patchDoc.ApplyTo(personToPatch, ModelState);
            TryValidateModel(personToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(personToPatch, personEntity);

            if (!_personRepository.Save())
            {
                return StatusCode(500, "Something went wrong.");
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var personInDb = _personRepository.GetPerson(id, false, false);
            if(personInDb == null) { return NotFound(); }

            _personRepository.Delete(personInDb);

            if (!_personRepository.Save())
            {
                return StatusCode(500, "An error occured while processing your request");
            }

            return NoContent();
        }
    }
}
