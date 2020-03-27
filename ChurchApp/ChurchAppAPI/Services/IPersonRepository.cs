using ChurchAppAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Services
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPeople(bool includeAddress = false, bool includeDonations = false);

        Person GetPerson(int id, bool includeAddress = false, bool includeDonations = false);

        void Add(Person person);

        void Update(Person person);

        void Patch(Person person);

        bool Save();

        void Delete(Person person);

    }
}
