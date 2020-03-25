using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Models
{
    public class CreatePersonDto
    {

        [Required(ErrorMessage = "First Name Required.")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name Required.")]
        public string LastName { get; set; }

        public List<AddressCreateDto> Addresses { get; set; }
    }
}
