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
        public IEnumerable<Person> GetPeople(bool includeAddress, bool includeDonations)
        {
            if (includeAddress && includeDonations)
            {
                return _context.Persons
                    .Include("Addresses.AddressType")
                    .Include("Donations.DonationType")
                    .ToList();
            }
            if (includeAddress)
            {
                return _context.Persons.Include("Addresses.AddressType").ToList();
            }
            if (includeDonations)
            {
                return _context.Persons.Include("Donations.DonationType").ToList();
            }

            return _context.Persons.ToList();
        }

        public Person GetPerson(int id, bool includeAddress = false, bool includeDonations = false)
        {
            if (includeAddress && includeDonations)
            {
                return _context.Persons
                .Where(x => x.ID == id)
                .Include("Addresses.AddressType")
                .Include("Donations.DonationType").FirstOrDefault();
            }
            if (includeDonations)
            {
                return _context.Persons
                .Where(x => x.ID == id)
                .Include("Donations.DonationType").FirstOrDefault();
            }
            if (includeAddress)
            {
                return _context.Persons
                .Where(x => x.ID == id)
                .Include("Addresses.AddressType").FirstOrDefault();
            }

            return _context.Persons
               .Where(x => x.ID == id).FirstOrDefault();
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
            _context.Update(person);
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
