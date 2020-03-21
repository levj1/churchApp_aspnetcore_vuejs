using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Entities
{
    public class Person
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Collection<Address> Addresses { get; set; }

        public Collection<Donation> Donations { get; set; }
    }
}
