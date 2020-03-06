using ChurchAppAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Models
{
    public class PersonDto
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName {
            get {
                return $"{FirstName} {LastName}";
            }
        }

        public List<Address> Addresses { get; set; }
    }
}
