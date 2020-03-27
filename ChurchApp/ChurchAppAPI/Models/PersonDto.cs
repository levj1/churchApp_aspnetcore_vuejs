using ChurchAppAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Models
{
    public class PersonDto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name Required.")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name Required.")]
        public string LastName { get; set; }

        public string FullName {
            get {
                return $"{FirstName} {LastName}";
            }
        }

        public List<AddressDto> Addresses { get; set; }

        public List<DonationWithoutPersonDto> Donations { get; set; }
    }
}
