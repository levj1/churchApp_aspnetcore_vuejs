using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Entities
{
    public class Donation
    {
        public int ID { get; set; }

        public decimal Amount { get; set; }

        public DateTime DonationDate { get; set; }

        public int PersonID { get; set; }

        public Person Person { get; set; }
    }
}
