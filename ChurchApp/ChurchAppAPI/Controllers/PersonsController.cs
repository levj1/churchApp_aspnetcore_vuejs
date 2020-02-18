using ChurchAppAPI.Entities;
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

        public PersonsController(ChurchAppContext churchContext)
        {
            _churchContext = churchContext;
        }

        [HttpGet]
        public ActionResult GetPersons()
        {
            return Ok(_churchContext.Persons.ToList());
        }
    }
}
