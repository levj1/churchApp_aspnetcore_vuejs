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
        public IEnumerable<Person> GetPeople(bool includeAddress)
        {
            List<Person> people;
            if (includeAddress)
            {
                return _context.Persons
                    .Include("Addresses.AddressType")
                    .ToList();
            }
            people = _context.Persons.ToList();
            
            return people;
        }

        public Person GetPerson(int id, bool includeAddress)
        {
            Person person = _context.Persons
                .Where(x => x.ID == id)
                .FirstOrDefault();
            if (includeAddress)
            {
                person = _context.Persons
                .Where(x => x.ID == id)
                .Include("Addresses.AddressType")
                .FirstOrDefault();
            }
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
