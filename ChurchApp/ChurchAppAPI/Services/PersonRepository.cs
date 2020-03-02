using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchAppAPI.Entities;

namespace ChurchAppAPI.Services
{
    public class PersonRepository : IPersonRepository
    {
        private ChurchAppContext _context;
        public PersonRepository(ChurchAppContext context)
        {
            _context = context;
        }
        public IEnumerable<Person> GetPeople()
        {
            var people = _context.Persons.ToList();
            return people;
        }

        public Person GetPerson(int id)
        {
            var person = _context.Persons.Where(x => x.ID == id).FirstOrDefault();
            return person;
        }
    }
}
