using ChurchAppAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Extensions.Seed
{
    public static class DonationTypeSeed
    {
        public static void EnsureSeedDataForContext(this ChurchAppContext context)
        {
            if (context.DonationTypes.Any())
            {
                return;
            }

            var donationTypes = new List<DonationType>()
            {
                new DonationType()
                {
                    Type = "Tithe"
                },
                new DonationType()
                {
                    Type = "Offering"
                },
                new DonationType()
                {
                    Type = "Pledge"
                }
            };

            context.AddRange(donationTypes);
            context.SaveChanges();
        } 
        
    }
}
