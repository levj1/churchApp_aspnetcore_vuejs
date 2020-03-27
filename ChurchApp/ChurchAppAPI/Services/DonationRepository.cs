using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchAppAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChurchAppAPI.Services
{
    public class DonationRepository : IDonationRepository
    {
        private ChurchAppContext _churchAppContext;

        public DonationRepository(ChurchAppContext churchAppContext)
        {
            _churchAppContext = churchAppContext;
        }
        public void Create(Donation donation)
        {
            _churchAppContext.Add(donation);
        }

        public void Create(ICollection<Donation> donations)
        {
            _churchAppContext.AddRange(donations);
        }

        public void Delete(Donation donation)
        {
            _churchAppContext.Remove(donation);
        }

        public Donation Get(int id)
        {
            return _churchAppContext.Donations
                .Where(d => d.ID == id)
                .Include("DonationType")
                .Include("Person").FirstOrDefault();
        }

        public IEnumerable<Donation> GetAll()
        {
            return _churchAppContext.Donations
                .Include("Person")
                .Include("DonationType").ToList();
        }

        public bool Save()
        {
            return (_churchAppContext.SaveChanges() >= 0);
        }
    }
}
