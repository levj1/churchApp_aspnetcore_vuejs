using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController: ControllerBase
    {
        private ChurchAppContext _churchContext;
        private IMapper _mapper;

        public PersonsController(ChurchAppContext churchContext,
            IMapper mapper)
        {
            _churchContext = churchContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetPersons()
        {
            var persons = _churchContext.Persons.ToList();
            var personDtos = _mapper.Map<List<PersonDto>>(persons);

            return Ok(personDtos);
        }
    }
}
