using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchAppAPI.Entities;
using Microsoft.EntityFrameworkCore;

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
            var person = _context.Persons.Where(x => x.ID == id).Include(x => x.Addresses).FirstOrDefault();
            return person;
        }

        public void Add(Person person)
        {
            _context.Add(person);            
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Update(Person person)
        {
            throw new NotImplementedException();
        }

        public void Patch(Person person)
        {
            throw new NotImplementedException();
        }

        public void Delete(Person person)
        {
            _context.Remove(person);
        }
    }
}
