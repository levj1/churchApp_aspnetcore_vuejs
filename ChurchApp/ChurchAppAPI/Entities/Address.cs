using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Entities
{
    public class Address
    {
        public int ID { get; set; }

        public AddressType Type { get; set; }

        public string StreetLine1 { get; set; }

        public string StreetLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }

        public int PersonID { get; set; }

        public Person Person { get; set; }

    }
}
