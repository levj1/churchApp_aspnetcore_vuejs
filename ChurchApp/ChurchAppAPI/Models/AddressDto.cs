using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Models
{
    public class AddressDto
    {
        public int ID { get; set; }

        public AddressTypeDto AddressType { get; set; }

        [Required(ErrorMessage = "Address street is required.")]
        public string StreetLine1 { get; set; }

        public string StreetLine2 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }


        [Required(ErrorMessage = "State is required.")]

        public string State { get; set; }


        [Required(ErrorMessage = "Zipcode street is required.")]
        public string Zipcode { get; set; }
    }
}
