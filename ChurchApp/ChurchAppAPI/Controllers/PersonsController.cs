using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Models;
using ChurchAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        private IMapper _mapper;

        public PersonsController(IPersonRepository personRepository,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetPersons([FromQuery] bool includeAddress = false)
        {
            var persons = _personRepository.GetPeople(includeAddress);
            var personDtos = _mapper.Map<List<PersonDto>>(persons);

            return Ok(personDtos);
        }

        [HttpGet("{id}")]
        public ActionResult GetPerson(int id, [FromQuery] bool includeAddress = false)
        {
            var person = _personRepository.GetPerson(id, includeAddress);
            if (person == null) { return NotFound(); }

            var personDto = _mapper.Map<PersonDto>(person);

            return Ok(personDto);
        }

        [HttpPost]
        public ActionResult Post([FromBody] PersonDto personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }        

            var personEntity = _mapper.Map<Person>(personDto);

            _personRepository.Add(personEntity);
            if (!_personRepository.Save())
            {
                return StatusCode(500, "An error occured while making the change");
            }

            return CreatedAtAction("Post", new { id = personEntity.ID }, personEntity);
        }

        [HttpPut]
        public IActionResult Update([FromBody]PersonDto person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var personInDb = _personRepository.GetPerson(person.ID, true);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var personInDb = _personRepository.GetPerson(id, false);
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
