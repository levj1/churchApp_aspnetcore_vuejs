using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchAppAPI.Entities;

namespace ChurchAppAPI.Services
{
    public class DonationTypeRepository : IDonationTypeRepository
    {
        private ChurchAppContext _churchAppContext;

        public DonationTypeRepository(ChurchAppContext churchAppContext)
        {
            _churchAppContext = churchAppContext;
        }

        public void Create(DonationType donationType)
        {
            _churchAppContext.DonationTypes.Add(donationType);
        }

        public void Delete(DonationType donationType)
        {
            _churchAppContext.DonationTypes.Remove(donationType);
        }

        public DonationType Get(int id)
        {
            return _churchAppContext.DonationTypes.Where(d => d.ID == id).FirstOrDefault();
        }

        public IEnumerable<DonationType> GetAll()
        {
            return _churchAppContext.DonationTypes.ToList();
        }

        public bool Save()
        {
            return (_churchAppContext.SaveChanges() >= 0);
        }
    }
}
