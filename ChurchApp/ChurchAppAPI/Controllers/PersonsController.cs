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
    public class PersonsController: ControllerBase
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

            return Ok(person);
        }
    }
}
