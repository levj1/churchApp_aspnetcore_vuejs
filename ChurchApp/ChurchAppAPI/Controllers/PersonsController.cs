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
    [Authorize]
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
        public ActionResult GetPersons()
        {
            var persons = _personRepository.GetPeople();
            var personDtos = _mapper.Map<List<PersonDto>>(persons);

            return Ok(personDtos);
        }

        [HttpGet("{id}")]
        public ActionResult GetPerson(int id)
        {
            var person = _personRepository.GetPerson(id);
            if (person == null) { return NotFound(); }

            var personDto = _mapper.Map<PersonDto>(person);

            return Ok(person);
        }

        [HttpPost]
        public ActionResult Post([FromBody] PersonDto personDto)
        {
            if (string.IsNullOrEmpty(personDto.FirstName) || string.IsNullOrEmpty(personDto.LastName))
            {
                return StatusCode(500, "First and Last Name required");
            }

            var personEntity = _mapper.Map<Person>(personDto);

            _personRepository.Add(personEntity);
            if (!_personRepository.Save())
            {
                return StatusCode(500, "An error occured while making the change");
            }

            return CreatedAtAction("Post", new { id = personEntity.ID }, personEntity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var personInDb = _personRepository.GetPerson(id);
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
