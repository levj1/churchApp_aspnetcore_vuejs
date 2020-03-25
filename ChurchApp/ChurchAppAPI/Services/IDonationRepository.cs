using ChurchAppAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Services
{
    public interface IDonationRepository
    {
        IEnumerable<Donation> GetAll();

        Donation Get(int id);

        bool Save();

        void Create(Donation donation);

        void Delete(Donation donatioon);
    }
}
