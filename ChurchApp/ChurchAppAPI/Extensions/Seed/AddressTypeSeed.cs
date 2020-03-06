using ChurchAppAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Extensions.Seed
{
    public static class AddressTypeSeed
    {
        public static void EnsureSeedDataForContext(this ChurchAppContext context)
        {
            if (context.DonationTypes.Any())
            {
                return;
            }

            var addressTypes = new List<AddressType>()
            {
                new AddressType()
                {
                    Name = "Home"
                },
                new AddressType()
                {
                    Name = "Work"
                }
            };

            context.AddRange(addressTypes);
            context.SaveChanges();
        }

    }
}
