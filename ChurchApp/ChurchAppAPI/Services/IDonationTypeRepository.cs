using ChurchAppAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Services
{
    public interface IDonationTypeRepository
    {
        IEnumerable<DonationType> GetAll();

        DonationType Get(int id);

        void Create(DonationType donationType);

        void Delete(DonationType donationType);

        bool Save();

    }
}
