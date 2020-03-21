using ChurchAppAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Services
{
    public interface IAddressRepository
    {
        IEnumerable<Address> GetAddressesForPerson(int personId);

        Address GetAddressForPerson(int personId, int addressId);

        bool Save();

        void Delete(Address address);

        void Create(Address address);

        void Update(Address address);

    }
}
