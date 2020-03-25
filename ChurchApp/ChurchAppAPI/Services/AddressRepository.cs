using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchAppAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChurchAppAPI.Services
{
    public class AddressRepository : IAddressRepository
    {
        private ChurchAppContext _churchAppContext;

        public AddressRepository(ChurchAppContext churchAppContext)
        {
            _churchAppContext = churchAppContext;
        }

        public void Create(Address address)
        {
            _churchAppContext.Add(address);
        }

        public void Delete(Address address)
        {
            _churchAppContext.Remove(address);
        }

        public Address GetAddressForPerson(int personId, int addressId)
        {
            return _churchAppContext.Addresses
                .Where(a => a.ID == addressId && a.PersonID == personId)
                .Include(x => x.AddressType).FirstOrDefault();
        }

        public IEnumerable<Address> GetAddressesForPerson(int personId)
        {
            List<Address> addresses = _churchAppContext.Addresses
                .Where(a => a.PersonID == personId)
                .Include(x => x.AddressType).ToList();
            return addresses;
        }

        public bool Save()
        {
            return _churchAppContext.SaveChanges() >= 0;      
        }

        public void Update(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
